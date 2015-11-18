using UnityEngine;
using System.Collections;

public abstract class Movable : MonoBehaviour
{
  public LayerMask blockingLayer;

  private bool collided;
  private RaycastHit2D collision;
  private RaycastHit2D projectedCollision;
  private Vector2 direction;

  public BoxCollider2D BoxCollider
  {
    get { return GetComponent<BoxCollider2D>(); }
  }

  public bool Collided
  {
    get { return collided; }
  }

  public Vector2 Direction
  {
    get { return direction; }
  }

  public Vector2 Position
  {
    get { return transform.position; }
  }

  public Rigidbody2D Rigidbody
  {
    get { return GetComponent<Rigidbody2D>(); }
  }

  public RaycastHit2D ProjectedCollision {
    get { return projectedCollision; }
  }

  public Vector2 ProjectedPosition
  {
    get { return Position + Direction; }
  }

  public bool ProjectedToCollide
  {
    get { return ProjectedCollision.transform != null; }
  }

  public void CalculateTrajectory()
  {
    BoxCollider.enabled = false;
    projectedCollision = Physics2D.Linecast(
      Position, ProjectedPosition, blockingLayer);
    BoxCollider.enabled = true;
  }

  protected void Move(int xDirection, int yDirection)
  {
    direction = new Vector2(xDirection, yDirection);
    CalculateTrajectory();

    if (!ProjectedToCollide)
    {
      Rigidbody.MovePosition(ProjectedPosition);
      collided = false;
    } else {
      collision = ProjectedCollision;
      collided = true;
    }
  }

  protected virtual void AttemptMove <T> (int xDir,
                                          int yDir) where T : Component
  {
    Move(xDir, yDir);

    if (!Collided)
    {
      OnNoCollision();

      return;
    } else {
      T hitComponent = ProjectedCollision.transform.GetComponent<T> ();
      OnCollision();

      if (Collided && hitComponent != null)
      {
        OnCollisionWith(hitComponent);
      }
    }
  }

  protected abstract void OnCollision();
  protected abstract void OnCollisionWith <T> (T component) where T : Component;
  protected abstract void OnNoCollision();
}
