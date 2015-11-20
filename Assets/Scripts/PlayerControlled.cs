using UnityEngine;
using System;
using System.Collections;

public class PlayerControlled : Movable
{
  private bool isPushing;
  private Animator animator;

  public bool Pushing
  {
    get { return isPushing; }
  }

  public SpriteRenderer Renderer
  {
    get { return GetComponent<SpriteRenderer>(); }
  }

  // Use this for initialization
  protected void Start()
  {
    isPushing = false;
    animator = GetComponent<Animator>();
    animator.SetBool("IdlingLeft", true);
  }

  private void BraceForPush()
  {
    if (MovingDown())
    {
      BraceForPushDown();
    } else if (MovingLeft()) {
      BraceForPushLeft();
    } else if (MovingRight()) {
      BraceForPushRight();
    } else if (MovingUp()) {
      BraceForPushUp();
    }
    isPushing = true;
  }

  private void BraceForPushDown()
  {
    animator.SetBool("PushingDown", true);
  }

  private void BraceForPushLeft()
  {
    animator.SetBool("PushingLeft", true);
  }

  private void BraceForPushRight()
  {
    animator.SetBool("PushingRight", true);
  }

  private void BraceForPushUp()
  {
    animator.SetBool("PushingUp", true);
  }

  public void Idle()
  {
    isPushing = false;
    animator.SetBool("PushingDown", false);
    animator.SetBool("PushingLeft", false);
    animator.SetBool("PushingRight", false);
    animator.SetBool("PushingUp", false);
  }

  public void IdleIfPushing()
  {
    if (Pushing)
    {
      Idle();
    }
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

    if (pushable != null)
    {
      OnCollisionWithPushable(pushable);
    }
  }

  private void OnCollisionWithPushable(Pushable pushable)
  {
    if (!Pushing)
    {
      BraceForPush();
    } else {
      Push(pushable);
    }
  }

  protected override void OnNoCollision()
  {
    SwitchIdleSidesIfIdling();
    IdleIfPushing();
  }

  public void Push(Pushable pushable)
  {
    pushable.Push(Direction);
    Idle();
  }

  public void SwitchIdleSidesIfIdling()
  {
    if (!Pushing)
    {
      animator.SetBool("IdlingLeft", !animator.GetBool("IdlingLeft"));
      animator.SetBool("IdlingRight", !animator.GetBool("IdlingRight"));
    }
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
