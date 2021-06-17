using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyButton : MonoBehaviour
{
  public string DifficultyName = "easy";
  public Core Core;
  
  public void pushButton()
  {
    Core.pushStartAsAnyDifficulty(DifficultyName);
  }
}
