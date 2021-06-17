using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour
{
  [Header("instanses")]
  public ScoreManager ScoreManager;

  [Header("Board Setting")]

  /// <summary>
  /// Micronoのサイズ
  /// </summary>
  public float MicronoSize;

  /// <summary>
  /// ステージの範囲を設定
  /// </summary>
  public Bounds StageBounds {
    get
    {
      var center = MicronoSize * new Vector3(BoardSize.x, BoardSize.y, BoardSize.z) / 2;
      center.x = 0f;
      center.z = 0f;
      var size = MicronoSize * new Vector3(BoardSize.x, BoardSize.y, BoardSize.z);
      return new Bounds(center, size);
    }
  }

  public Bounds GameoverZone
  {
    get
    {
      var height = 5f;
      var center = new Vector3(0f, StageBounds.max.y + height / 2, 0f);
      var size = new Vector3(StageBounds.size.x, height, StageBounds.size.z);
      return new Bounds(center, size);
    }
  }

  /// <summary>
  /// ステージの大きさ（マス目）を設定
  /// </summary>
  public Vector3Int BoardSize;

  [Header("Mino Settings")]

  public List<MinoForm> MinoForms;

  public DifficultySetting CurrentDifficultySetting;

  public List<DifficultySetting> Difficulties;

  /// <summary>
  /// 一段を埋めつくす
  /// </summary>
  public float ClearPercentage { get { return CurrentDifficultySetting.ClearPercentage; } }

  public float NextInterval { get { return CurrentDifficultySetting.NextInterval; } }

  public float fallSpeed { get { return CurrentDifficultySetting.fallSpeed; } }

  public float RBDrag { get { return CurrentDifficultySetting.RBDrag; } }

  public float RBAngularDrag { get { return CurrentDifficultySetting.RBAngularDrag; } }

  [Header("Other Settings")]

  public GameObject FallSetPosition;

  public PhysicMaterial MinoPhysicMat;

  public float LimitOfPositionError;
  public float LimitOfRotationError;

  private void Awake()
  {
    Core.gs = this;
    Board.gs = this;
    HoldManager.gs = this;
    //NextManager.gs = this;
    Mino.gs = this;
    Microno.gs = this;
    MinoOperater.gs = this;
    StageGenerater.gs = this;
  }

  public void SetDifficulty(string diff_name)
  {
    foreach(var diff_data in Difficulties)
    {
      if(diff_data.Name == diff_name)
      {
        Debug.Log("Difficulty: " + CurrentDifficultySetting.Name + " -> " + diff_data.Name);
        CurrentDifficultySetting = diff_data;
        break;
      }
    }
  }
}
