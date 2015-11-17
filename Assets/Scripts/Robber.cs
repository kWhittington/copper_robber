using UnityEngine;
using System;
using System.Collections;

public class Robber : MovingObject
{

  // Use this for initialization
  protected override void Start()
  {
    base.Start();
  }

  // Update is called once per frame
  void Update()
  {
    UpdatePosition();
  }

  void UpdatePosition()
  {
    if (MoveLeftInputDetected())
    {
      AttemptMove<Wall>(-1, 0);
    } else if (MoveRightInputDetected()) {
      AttemptMove<Wall>(1, 0);
    } else if (MoveUpInputDetected()) {
      AttemptMove<Wall>(0, 1);
    } else if (MoveDownInputDetected()) {
      AttemptMove<Wall>(0, -1);
    }
  }

  public bool MoveLeftInputDetected()
  {
    return Input.GetKeyUp(KeyCode.LeftArrow);
  }

  public bool MoveRightInputDetected()
  {
    return Input.GetKeyUp(KeyCode.RightArrow);
  }

  public bool MoveUpInputDetected()
  {
    return Input.GetKeyUp(KeyCode.UpArrow);
  }

  public bool MoveDownInputDetected()
  {
    return Input.GetKeyUp(KeyCode.DownArrow);
  }

  protected override void AttemptMove<T> (int xDir, int yDir)
  {
    base.AttemptMove<T> (xDir, yDir);
    RaycastHit2D hit;
    if (Move(xDir, yDir, out hit))
    {
    }
  }

  protected override void OnCantMove<T> (T component)
  {
    Debug.Log("Cannot Move Robber");
  }
}
