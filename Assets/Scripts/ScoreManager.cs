using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
  public TMPro.TextMeshPro ScoreBoard;

  public int OneLineScore;
  public int DeleteMicronoScore;

  public int Score;


  // Start is called before the first frame update
  void Start()
  {
    
  }

  public void Inilialize()
  {
    Score = 0;
    ScoreBoard.text = "0";
  }

  // Update is called once per frame
  void Update()
  {
    ScoreBoard.text = Score.ToString();
  }

  public void OneLine()
  {
    Score += OneLineScore;
  }

  public void DeleteMicrono()
  {
    Score += DeleteMicronoScore;
  }
}
