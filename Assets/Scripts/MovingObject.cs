using UnityEngine;
using System.Collections;

public abstract class MovingObject : MonoBehaviour
{
  public LayerMask blockingLayer;

  public BoxCollider2D BoxCollider
  {
    get { return GetComponent<BoxCollider2D>(); }
  }

  public Rigidbody2D Rigidbody
  {
    get { return GetComponent<Rigidbody2D>(); }
  }

  protected bool Move(int xDir, int yDir, out RaycastHit2D hit)
  {
    Vector2 start = transform.position;
    Vector2 end = start + new Vector2(xDir, yDir);

    BoxCollider.enabled = false;
    hit = Physics2D.Linecast(start, end, blockingLayer);
    BoxCollider.enabled = true;

    if (hit.transform == null)
    {
      Rigidbody.MovePosition(end);

      return true;
    }

    return false;
  }

  protected virtual void AttemptMove <T> (int xDir,
                                          int yDir) where T : Component
  {
    RaycastHit2D hit;
    bool canMove = Move(xDir, yDir, out hit);

    if (hit.transform == null)
    {
      return;
    }

    T hitComponent = hit.transform.GetComponent<T> ();

    if (!canMove && hitComponent != null)
    {
      OnCantMove(hitComponent);
    }
  }

  protected abstract void OnCantMove <T> (T component) where T : Component;
}
