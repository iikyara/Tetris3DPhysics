using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Core : MonoBehaviour
{
  public Board Board;
  public NextManager NextManager;
  public HoldManager HoldManager;
  public MinoOperater MinoOperater;

  [Header("GUI Settings")]
  public GameObject TitleGUI;
  public GameObject GameoverGUI;
  public GameObject GameclearGUI;

  public Text ScoreText1;
  public Text ScoreText2;

  public Mino CurrentMino;

  public static GameSettings gs;

  public bool Running;

  private bool ready;

  public int StartInterval;

  private bool isHold;

  public string difficulty = "easy";

  // Start is called before the first frame update
  void Start()
  {
    //Initilize();
    changeGUIScene(GUIScene.title);
  }

  public void Initilize()
  {
    Board.Inilialize();
    NextManager.Initialize();
    HoldManager.Initialize();
    MinoOperater.Initialize();
    gs.ScoreManager.Inilialize();

    Running = false;
    ready = true;
    StartInterval = 0;
    CurrentMino?.Discard();
    CurrentMino = null;

    gs.SetDifficulty(difficulty);
  }

  // Update is called once per frame
  void Update()
  {
    if(ready && StartInterval++ > 10)
    {
      ready = false;
      Running = true;
    }

    if (!Running) return;

    if (CurrentMino == null)
    {
      CurrentMino = NextManager.CreateAndPushMino();
      if (CurrentMino != null)
      {
        CurrentMino.State = MinoState.set;
      }
    }
    else if(CurrentMino.State != MinoState.fall && CurrentMino.State != MinoState.set)
    {
      Board.AddMino(CurrentMino);
      CurrentMino = null;
      isHold = false;
    }
  }

  /// <summary>
  /// 現在のミノをホールドする．
  /// </summary>
  public void HoldMino()
  {
    if (isHold) return;
    CurrentMino = HoldManager.Hold(CurrentMino);
    isHold = true;
  }

  public void GameOver()
  {
    Running = false;
    Debug.Log("GameOver");
    ScoreText1.text = $"SCORE : {gs.ScoreManager.Score}";
    changeGUIScene(GUIScene.gameover);
  }

  public void GameClear()
  {
    Running = false;
    Debug.Log("GameClear");
    ScoreText2.text = $"SCORE : {gs.ScoreManager.Score}";
    changeGUIScene(GUIScene.gameclear);
  }

  public void pushStart()
  {
    Debug.Log("game start");
    changeGUIScene(GUIScene.none);
    Initilize();
  }

  public void pushStartAsAnyDifficulty(string diff_name)
  {
    Debug.Log("game start as difficulty is very hard");
    changeGUIScene(GUIScene.none);
    difficulty = diff_name;
    Initilize();
  }

  public void pushToTitle()
  {
    Debug.Log("to title");
    changeGUIScene(GUIScene.title);
  }

  public void pushEnd()
  {
#if UNITY_EDITOR
    UnityEditor.EditorApplication.isPlaying = false;
#else
		Application.Quit();
#endif
  }

  enum GUIScene
  {
    title,
    gameover,
    gameclear,
    none
  }

  private void changeGUIScene(GUIScene s)
  {
    TitleGUI.SetActive(false);
    GameoverGUI.SetActive(false);
    GameclearGUI.SetActive(false);
    if (s == GUIScene.title) TitleGUI.SetActive(true);
    else if (s == GUIScene.gameover) GameoverGUI.SetActive(true);
    else if (s == GUIScene.gameclear) GameclearGUI.SetActive(true);
  }
}
