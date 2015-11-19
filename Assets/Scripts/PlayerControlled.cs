using UnityEngine;
using System;
using System.Collections;

public class PlayerControlled : Movable
{
  public SpriteRenderer Renderer
  {
    get { return GetComponent<SpriteRenderer>(); }
  }

  // Use this for initialization
  protected void Start()
  {
  }

  public void MoveLeft()
  {
    AttemptMove<Pushable>(-1, 0);
  }

  public bool MoveLeftInputDetected()
  {
    return Input.GetKeyUp(KeyCode.LeftArrow);
  }

  public void MoveRight()
  {
    AttemptMove<Pushable>(1, 0);
  }

  public bool MoveRightInputDetected()
  {
    return Input.GetKeyUp(KeyCode.RightArrow);
  }

  public void MoveUp()
  {
    AttemptMove<Pushable>(0, 1);
  }

  public bool MoveUpInputDetected()
  {
    return Input.GetKeyUp(KeyCode.UpArrow);
  }

  public void MoveDown()
  {
    AttemptMove<Pushable>(0, -1);
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
      MoveLeft();
    } else if (MoveRightInputDetected()) {
      MoveRight();
    } else if (MoveUpInputDetected()) {
      MoveUp();
    } else if (MoveDownInputDetected()) {
      MoveDown();
    }
  }
}
