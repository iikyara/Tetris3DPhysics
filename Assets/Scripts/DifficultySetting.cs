using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Difficulty", menuName ="ScriptableObjects/CreateDifficultySetting")]
public class DifficultySetting : ScriptableObject
{
  public string Name = "default";

  /// <summary>
  /// 一段を埋めつくす
  /// </summary>
  public float ClearPercentage;

  /// <summary>
  /// 次のミノが来るまでの時間間隔
  /// </summary>
  public float NextInterval;

  [Header("Mino Settings")]

  public float fallSpeed;

  public float RBDrag;
  public float RBAngularDrag;
}
