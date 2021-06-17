using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinoOperater : MonoBehaviour
{
  public float MoveSpeed;
  public float UpDownSpeed;
  public float RotateSpeed;

  [Header("Camera Settings")]
  public float CameraRotateSpeed;
  public float CameraScaleSpeed;

  public Core Core;

  public Mino Target;
  public MinoState PreState;

  public static GameSettings gs;

  public GameObject Tilt;
  public GameObject Pan;
  public GameObject Camera_go;

  public bool IsInverseAtTilt;
  public bool IsInverseAtPan;

  public float MAX_PAN_ROTATION;
  public float MIN_PAN_ROTATION;

  public float MAX_CAMERA_DIST;
  public float MIN_CAMERA_DIST;

  private RaycastHit hit; //レイキャストが当たったものを取得する入れ物

  // Start is called before the first frame update
  void Start()
  {

  }

  public void Initialize()
  {
    Target = null;
  }

  // Update is called once per frame
  void Update()
  {
    selectMino();
    operateHold();
    if (Target != null) operateTarget();
    else operateCamera();
  }

  private void selectMino()
  {
    var selectDown = Input.GetButtonDown("Select");
    var selectUp = Input.GetButtonUp("Select");
    if (selectDown)
    {
      Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //マウスのポジションを取得してRayに代入

      if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Mino")))  //マウスのポジションからRayを投げて何かに当たったらhitに入れる
      {
        string objectName = hit.collider.gameObject.name; //オブジェクト名を取得して変数に入れる
        Debug.Log(objectName); //オブジェクト名をコンソールに表示

        var go = hit.collider.gameObject;
        Microno ms;
        if(go.TryGetComponent<Microno>(out ms))
        {
          setTarget(ms.ParentMino);
        }
        Mino m;
        if(go.TryGetComponent<Mino>(out m))
        {
          setTarget(m);
        }
      }
    }
    if (selectUp && Target != null)
    {
      Target.State = PreState;
      Target = null;
    }
  }

  private void setTarget(Mino m)
  {
    Target = m;
    PreState = Target.State;
    Target.TargetPosition = Target.transform.position;
    Target.TargetRotation = Target.transform.rotation;
    Target.TargetScale = Target.transform.localScale;
    if (Target.State == MinoState.physics) Target.State = MinoState.operate;
  }

  private void operateTarget()
  {
    var x = MoveSpeed * Input.GetAxisRaw("Mouse X");
    var z = MoveSpeed * Input.GetAxisRaw("Mouse Y");
    var y = UpDownSpeed * Input.GetAxisRaw("Mouse ScrollWheel");
    var r_x = RotateSpeed * Input.GetAxisRaw("Rotate X");
    var r_y = RotateSpeed * Input.GetAxisRaw("Rotate Y");
    var r_z = RotateSpeed * Input.GetAxisRaw("Rotate Z");

    //fallモードの時は上に上げれないようにする．
    if (Target.State == MinoState.fall && y > 0) y = 0;

    //x,yの移動方向をカメラ方向に修正する．
    var dict = Quaternion.Inverse(Camera_go.transform.rotation) * new Vector3(0, 0, 1);
    var dict2 = new Vector2(dict.x, dict.z).normalized;
    var sin = dict2.x;
    var cos = dict2.y;
    var move = new Vector2(
      x * cos - z * sin,
      x * sin + z * cos
    );

    x = move.x;
    z = move.y;

    Target.TargetPosition.x += x;
    Target.TargetPosition.y += y;
    Target.TargetPosition.z += z;

    //位置制限
    if (Target.TargetPosition.x < gs.StageBounds.min.x)
      Target.TargetPosition.x = gs.StageBounds.min.x;
    else if (Target.TargetPosition.x > gs.StageBounds.max.x)
      Target.TargetPosition.x = gs.StageBounds.max.x;
    if (Target.TargetPosition.y < gs.StageBounds.min.y)
      Target.TargetPosition.y = gs.StageBounds.min.y;
    else if (Target.TargetPosition.y > gs.StageBounds.max.y)
      Target.TargetPosition.y = gs.StageBounds.max.y;
    if (Target.TargetPosition.z < gs.StageBounds.min.z)
      Target.TargetPosition.z = gs.StageBounds.min.z;
    else if (Target.TargetPosition.z > gs.StageBounds.max.z)
      Target.TargetPosition.z = gs.StageBounds.max.z;

    Target.TargetRotation = Quaternion.Euler(r_x, r_y, r_z) * Target.TargetRotation;
  }

  private void operateHold()
  {
    var hold = Input.GetButton("Hold");
    if (hold)
    {
      if (Target != null && Target.State == MinoState.fall)
      {
        Target.State = PreState;
        Target = null;
      }
      Core.HoldMino();
      return;
    }
  }

  private void operateCamera()
  {
    var clicked = Input.GetButton("Select");
    var x = MoveSpeed * Input.GetAxisRaw("Mouse X");
    var z = MoveSpeed * Input.GetAxisRaw("Mouse Y");
    var y = UpDownSpeed * Input.GetAxisRaw("Mouse ScrollWheel");

    if (IsInverseAtTilt) x *= -1;
    if (IsInverseAtPan) z *= -1;

    if (clicked)
    {
      Tilt.transform.localRotation *= Quaternion.Euler(0, x, 0);
      Pan.transform.localRotation *= Quaternion.Euler(z, 0, 0);

      //各値の幅を決める
      if (
        MAX_PAN_ROTATION < Pan.transform.localRotation.eulerAngles.x
        && Pan.transform.localRotation.eulerAngles.x < 180
      )
        Pan.transform.localRotation = Quaternion.Euler(MAX_PAN_ROTATION, 0, 0);
      if (
        180 < Pan.transform.localRotation.eulerAngles.x
        && Pan.transform.localRotation.eulerAngles.x < 360 + MIN_PAN_ROTATION
      )
        Pan.transform.localRotation = Quaternion.Euler(360 + MIN_PAN_ROTATION, 0, 0);
    }

    Camera_go.transform.localPosition += new Vector3(0, 0, y);
  }
}
