using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MinoState
{
  position,
  physics,
  fall,
  set,
  operate
}

[RequireComponent(typeof(Rigidbody))]
public class Mino : MonoBehaviour
{
  public MinoForm InputForm;
  [HideInInspector]
  public List<Microno> Micronos;
  private MinoState _state;
  public MinoState State {
    get
    {
      return _state;
    }
    set
    {
      _state = value;
      if (value == MinoState.position)
      {
        ActivateRBGrabity(false);
        SetMicronosCollision(false);
      }
      else if(value == MinoState.operate)
      {
        ActivateRBGrabity(false);
        SetMicronosCollision(true);
      }
      else if(value == MinoState.physics)
      {
        ActivateRBGrabity(true);
        SetMicronosCollision(true);
      }
      else if(value == MinoState.fall)
      {
        ActivateRBGrabity(false);
        SetMicronosCollision(true);
      }
      else if(value == MinoState.set)
      {
        ActivateRBGrabity(false);
        SetMicronosCollision(false);
      }
      Debug.Log(value);
    }
  }
  public Board Board;

  public Vector3 TargetPosition;
  public Quaternion TargetRotation;
  public Vector3 TargetScale;

  private Rigidbody rb;

  public bool CollisionOther;

  public static GameSettings gs;

  // Start is called before the first frame update
  void Start()
  {
    //CreateMino(this.InputForm);
    Micronos = new List<Microno>();
    //rb = gameObject.AddComponent<Rigidbody>();
    rb = gameObject.GetComponent<Rigidbody>();
    CreateMino(this.InputForm);
    State = MinoState.position;
    gameObject.layer = LayerMask.NameToLayer("Mino");
  }

  private void ActivateRBGrabity(bool use)
  {
    if (use)
    {
      rb.useGravity = true;
      rb.drag = 0f;
      rb.angularDrag = 0.05f;
    }
    else
    {
      rb.useGravity = false;
      rb.drag = gs.RBDrag;
      rb.angularDrag = gs.RBAngularDrag;
    }
  }

  public void CreateMino(MinoForm mf)
  {
    InputForm = mf;
    var ms = gs.MicronoSize;
    var form = mf.GetForm();

    for (int i = 0; i < form.GetLength(0); i++)
    {
      for (int j = 0; j < form.GetLength(1); j++)
      {
        for (int k = 0; k < form.GetLength(2); k++)
        {
          if(form[i, j, k])
          {
            var x = k * ms;
            var y = -j * ms;
            var z = i * ms;
            var point = new Vector3(x, y, z)
              + ms / 2 * new Vector3(1, -1, 1)
              - Vector3.Scale(mf.Center, new Vector3(1, -1, 1));
            Micronos.Add(CreateMicrono(point, mf));
          }
        }
      }
    }
  }

  private Microno CreateMicrono(Vector3 point, MinoForm mf)
  {
    var microno_go = GameObject.CreatePrimitive(PrimitiveType.Cube);
    microno_go.GetComponent<MeshRenderer>().material = mf.Mat;
    Utils.SetParent(this.gameObject, microno_go);
    microno_go.transform.localPosition = point;
    microno_go.transform.localScale = gs.MicronoSize * Vector3.one;
    var microno = microno_go.AddComponent<Microno>();
    microno.ParentMino = this;
    return microno;
  }

  // Update is called once per frame
  void Update()
  {
    checkDelete();
  }

  /// <summary>
  /// 全てのミクロノスが消えたらミノを削除する．
  /// </summary>
  private void checkDelete()
  {
    if (Micronos.Count == 0)
    {
      Board?.Minos.Remove(this);
      MonoBehaviour.Destroy(this.gameObject, 1f);
    }
  }

  public void Discard()
  {
    var ms = Micronos.ToArray();
    for(int i =0; i < ms.Length; i++)
    {
      ms[i].EffectScore = false;
      ms[i].Delete();
    }
  }

  private void FixedUpdate()
  {
    if (State == MinoState.fall) updateThenFallMode();
    else if (State == MinoState.physics) updateThenPhysicsMode();
    else if (State == MinoState.operate) updateThenOperateMode();
    else if (State == MinoState.position) updateThenPositionMode();
    else if (State == MinoState.set) updateThenSetMode();
  }

  private void updateThenFallMode()
  {
    updateThenPositionMode();
    TargetPosition += gs.fallSpeed * Time.deltaTime * new Vector3(0, -1f, 0);

    if (CollisionOther) State = MinoState.physics;
  }

  private void updateThenPhysicsMode()
  {

  }

  private void updateThenOperateMode()
  {
    updateThenPositionMode();
  }

  private void updateThenPositionMode()
  {
    //position
    if(TargetPosition != transform.position)
    {
      var dir = TargetPosition - transform.position;
      rb.AddForce(dir, ForceMode.Impulse);
    }
    
    //rotation
    if(TargetRotation != transform.rotation)
    {
      /*var diff = TargetRotation * Quaternion.Inverse(transform.rotation);
      if(diff.w < 0f)
      {
        diff.x = -diff.x;
        diff.y = -diff.y;
        diff.z = -diff.z;
        diff.w = -diff.w;
      }*/
      transform.rotation = Quaternion.Slerp(transform.rotation, TargetRotation, 0.1f);
      //rb.AddTorque(new Vector3(diff.x, diff.y, diff.z) * 100f, ForceMode.Impulse);
      //Debug.Log($"add torque: TR:{TargetRotation}, tr:{transform.rotation}, diff:{diff}");
    }

    //scale
    if(TargetScale != transform.localScale)
    {
      var s_diff = TargetScale - transform.localScale;
      transform.localScale += s_diff;
    }
  }

  private void updateThenSetMode()
  {
    TargetPosition = gs.FallSetPosition.transform.position;
    TargetRotation = gs.FallSetPosition.transform.rotation;
    TargetScale = 1f * Vector3.one;

    updateThenPositionMode();

    if (
      (transform.position - gs.FallSetPosition.transform.position).magnitude < gs.LimitOfPositionError &&
      Quaternion.Angle(transform.rotation, gs.FallSetPosition.transform.rotation) < gs.LimitOfRotationError &&
      transform.localScale == 1f * Vector3.one
    )
    {
      State = MinoState.fall;
    }

    Debug.Log((transform.position - gs.FallSetPosition.transform.position).magnitude + ", "
      + Quaternion.Angle(transform.rotation, gs.FallSetPosition.transform.rotation));

  }

  private void SetMicronosCollision(bool activation)
  {
    foreach(var mc in Micronos)
    {
      mc.GetComponent<Collider>().enabled = activation;
    }
  }

  private void OnCollisionStay(Collision collision)
  {
    CollisionOther = collision.gameObject.TryGetComponent<Mino>(out _) || collision.gameObject.name=="bottom";
  }

  private void OnCollisionExit(Collision collision)
  {
    CollisionOther = false;
  }

  /// <summary>
  /// ランダムな形状でミノを生成する．
  /// </summary>
  /// <returns></returns>
  public static Mino Create(Vector3 firstPosition = new Vector3())
  {
    //ランダムに形状を選ぶ
    MinoForm mf = Utils.RandomList(gs.MinoForms);

    var go = new GameObject(mf.Name);
    var mino = go.AddComponent<Mino>();
    mino.InputForm = mf;

    mino.transform.position = firstPosition;

    return mino;
  }
}