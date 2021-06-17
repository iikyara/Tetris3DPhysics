using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Microno : MonoBehaviour
{
  public int Segment;
  public Mino ParentMino;

  public static GameSettings gs;

  public bool OutSide;

  public bool EffectScore;

  private void Start()
  {
    gameObject.layer = LayerMask.NameToLayer("Mino");
    OutSide = false;
    EffectScore = true;
    GetComponent<Collider>().material = gs.MinoPhysicMat;
  }

  /// <summary>
  /// このMicronoを消す
  /// </summary>
  public void Delete()
  {
    MonoBehaviour.Destroy(this.gameObject);

    ParentMino.Micronos.Remove(this);
    if(EffectScore) gs.ScoreManager.DeleteMicrono();
  }

  // Update is called once per frame
  void Update()
  {
    CheckSegment();
  }

  private void CheckSegment()
  {
    //このマイクロノの位置を取得
    var p = transform.position;
    //範囲内で段数を計算
    if (gs.StageBounds.Contains(p))
    {
      Segment = Mathf.FloorToInt(p.y / gs.MicronoSize);
      //Debug.Log($"p.y:{p.y}, segment:{Segment}");
      if (Segment >= gs.BoardSize.y) Segment = gs.BoardSize.y - 1;
      else if (Segment < 0) Segment = 0;
    }
    OutSide = gs.GameoverZone.Contains(p);
  }
}
