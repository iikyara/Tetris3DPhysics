using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Difficulty", menuName ="ScriptableObjects/CreateDifficultySetting")]
public class DifficultySetting : ScriptableObject
{
  public string Name = "default";

  /// <summary>
  /// ˆê’i‚ð–„‚ß‚Â‚­‚·
  /// </summary>
  public float ClearPercentage;

  /// <summary>
  /// ŽŸ‚Ìƒ~ƒm‚ª—ˆ‚é‚Ü‚Å‚ÌŽžŠÔŠÔŠu
  /// </summary>
  public float NextInterval;

  [Header("Mino Settings")]

  public float fallSpeed;

  public float RBDrag;
  public float RBAngularDrag;
}
