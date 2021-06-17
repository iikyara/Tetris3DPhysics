using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldManager : MonoBehaviour
{
  public Mino HoldMino;

  public GameObject HoldPosition;

  public float HoldSize;

  public static GameSettings gs;

  // Start is called before the first frame update
  void Start()
  {
    HoldMino = null;
  }

  public void Initialize()
  {
    HoldMino?.Discard();
    HoldMino = null;
  }

  public Mino Hold(Mino newHold)
  {
    if(newHold == null)
    {
      Debug.Log("Invalid hold");
      return null;
    }

    var oldHold = HoldMino;
    HoldMino = newHold;

    //transformÇêßå‰
    if(oldHold != null)
    {
      oldHold.State = MinoState.set;
      oldHold.TargetPosition = gs.FallSetPosition.transform.position;
      oldHold.TargetRotation = gs.FallSetPosition.transform.rotation;
      oldHold.TargetScale = 1f * Vector3.one;
    }

    newHold.State = MinoState.position;
    newHold.TargetPosition = HoldPosition.transform.position;
    newHold.TargetRotation = HoldPosition.transform.rotation;
    newHold.TargetScale = HoldSize * Vector3.one;

    return oldHold;
  }
}
