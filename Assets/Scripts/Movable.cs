using UnityEngine;
using System.Collections;

public abstract class Movable : MonoBehaviour
{
  public LayerMask blockingLayer;

  private bool collided;
  private RaycastHit2D projectedCollision;
  private Vector2 currentDirection;

  public BoxCollider2D BoxCollider
  {
    get { return GetComponent<BoxCollider2D>(); }
  }

  public bool Collided
  {
    get { return collided; }
  }

  public Vector2 CurrentDirection
  {
    get { return currentDirection; }
  }

  public Vector2 CurrentPosition
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
    get { return CurrentPosition + CurrentDirection; }
  }

  public bool ProjectedToCollide
  {
    get { return ProjectedCollision.transform != null; }
  }

  public void CalculateTrajectory()
  {
    BoxCollider.enabled = false;
    projectedCollision = Physics2D.Linecast(
      CurrentPosition, ProjectedPosition, blockingLayer);
    BoxCollider.enabled = true;
  }

  protected void Move(int xDirection, int yDirection)
  {
    currentDirection = new Vector2(xDirection, yDirection);
    CalculateTrajectory();

    if (!ProjectedToCollide)
    {
      Rigidbody.MovePosition(ProjectedPosition);
      collided = false;
    } else {
      collided = true;
    }
  }

  protected virtual void AttemptMove <T> (int xDir,
                                          int yDir) where T : Component
  {
    Move(xDir, yDir);

    if (!Collided)
    {
      return;
    }

    T hitComponent = ProjectedCollision.transform.GetComponent<T> ();

    if (Collided && hitComponent != null)
    {
      OnCantMove(hitComponent);
    }
  }

  protected abstract void OnCantMove <T> (T component) where T : Component;
}
