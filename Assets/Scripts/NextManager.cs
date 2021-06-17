using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextManager : MonoBehaviour
{
  public int NextNum;
  public float NextMinoSize;
  public Queue<Mino> MinoQueue;

  public List<GameObject> NextPositions;

  public GameObject FirstPosition;

  // Start is called before the first frame update
  void Start()
  {
    MinoQueue = new Queue<Mino>(NextNum);
  }

  public void Initialize()
  {
    var ms = new List<Mino>(MinoQueue).ToArray();
    for(int i = 0; i < ms.Length; i++)
    {
      ms[i].Discard();
    }
    
    MinoQueue.Clear();
    Setup();
  }

  private void Setup()
  {
    for(int i = 0; i < NextNum; i++)
    {
      CreateMino();
    }
  }

  public Mino CreateAndPushMino()
  {
    Mino mino;
    try
    {
      mino = MinoQueue.Dequeue();
    }
    catch(System.InvalidOperationException e)
    {
      mino = null;
    }
    CreateMino();
    return mino;
  }

  private Mino CreateMino()
  {
    var mino = Mino.Create(FirstPosition.transform.position);
    MinoQueue.Enqueue(mino);
    return mino;
  }

  private void Update()
  {
    updateMinoPositions();
  }

  private void updateMinoPositions()
  {
    var p1 = NextPositions[0].transform.position;
    var p2 = NextPositions[1].transform.position;

    var minos = new List<Mino>(MinoQueue);

    for(int i = 0; i < minos.Count; i++)
    {
      var mino = minos[i];
      mino.TargetPosition = (p2 - p1) * ((float)i / minos.Count) + p1;
      mino.TargetPosition.y += NextMinoSize / 2;
      mino.TargetRotation = Quaternion.Euler(0, 0, 0);
      mino.TargetScale = NextMinoSize * Vector3.one;
    }
  }
}
