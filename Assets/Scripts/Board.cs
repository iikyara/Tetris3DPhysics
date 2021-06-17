using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
  public Core Core;

  public List<Mino> Minos;
  public List<Microno> Micronos;

  private List<Microno>[] seg_count;

  public static GameSettings gs;

  // Start is called before the first frame update
  void Start()
  {
    Minos = new List<Mino>();
    Micronos = new List<Microno>();
    seg_count = new List<Microno>[gs.BoardSize.y];
    for (int i = 0; i < seg_count.Length; i++) seg_count[i] = new List<Microno>();
  }

  public void Inilialize()
  {
    //checkMinoList();
    foreach (var m in Minos) m.Discard();
    Minos.Clear();
    Micronos.Clear();
  }

  public void AddMino(Mino m)
  {
    foreach (var mic in m.Micronos)
    {
      this.Micronos.Add(mic);
    }
    Minos.Add(m);
    m.Board = this;
  }

  // Update is called once per frame
  void Update()
  {
    checkGameover();
    checkSegments();
    //checkMinoList();
  }

  private void checkSegments()
  {
    var maxMino = gs.BoardSize.x * gs.BoardSize.z;
    var clearTerm = maxMino * gs.ClearPercentage;

    for (int i = 0; i < seg_count.Length; i++) seg_count[i].Clear();
    foreach (var mic in Micronos) seg_count[mic.Segment].Add(mic);

    for (int i = 0; i < seg_count.Length; i++)
    {
      //条件に達していたらその段のミクロノスを消す．
      if(seg_count[i].Count >= clearTerm)
      {
        foreach(var mic in seg_count[i])
        {
          mic.Delete();
          Micronos.Remove(mic);
        }
        gs.ScoreManager.OneLine();
      }
    }
  }

  private void checkGameover()
  {
    foreach(var mic in Micronos)
    {
      if (mic.OutSide)
      {
        Core.GameOver();
        return;
      }
    }
  }

  private void checkMinoList()
  {
    foreach(var m in Minos)
    {
      if (m == null) Minos.Remove(m);
    }
  }
}
