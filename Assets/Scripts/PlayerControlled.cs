using UnityEngine;
using System;
using System.Collections;

public class PlayerControlled : Movable
{

  // Use this for initialization
  protected void Start()
  {
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

  protected override void OnCollision()
  {
  }

  protected override void OnCollisionWith<T> (T component)
  {
    Pushable pushable = component as Pushable;

    pushable.Push(Direction);
  }

  protected override void OnNoCollision()
  {
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
      AttemptMove<Pushable>(-1, 0);
    } else if (MoveRightInputDetected()) {
      AttemptMove<Pushable>(1, 0);
    } else if (MoveUpInputDetected()) {
      AttemptMove<Pushable>(0, 1);
    } else if (MoveDownInputDetected()) {
      AttemptMove<Pushable>(0, -1);
    }
  }
}
