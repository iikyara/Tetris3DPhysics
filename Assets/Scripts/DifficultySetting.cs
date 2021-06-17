using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Difficulty", menuName ="ScriptableObjects/CreateDifficultySetting")]
public class DifficultySetting : ScriptableObject
{
  public string Name = "default";

  /// <summary>
  /// ��i�𖄂߂���
  /// </summary>
  public float ClearPercentage;

  /// <summary>
  /// ���̃~�m������܂ł̎��ԊԊu
  /// </summary>
  public float NextInterval;

  [Header("Mino Settings")]

  public float fallSpeed;

  public float RBDrag;
  public float RBAngularDrag;
}
