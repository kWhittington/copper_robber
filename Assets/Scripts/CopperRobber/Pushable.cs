using UnityEngine;
using System.Collections;

namespace CopperRobber
{
  public class Pushable : Movable {

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Push(int xDirection, int yDirection)
    {
      AttemptMove<Pushable>(xDirection, yDirection);
    }

    public void Push(Vector2 vector)
    {
      Push((int)vector.x, (int)vector.y);
    }

    protected override void OnCollision()
    {
    }

    protected override void OnCollisionWith <T> (T component)
    {
      Pushable pushable = component as Pushable;

      if (pushable)
      {
        pushable.Push(Direction);
      }
    }

    protected override void OnNoCollision()
    {
    }
  }
}
