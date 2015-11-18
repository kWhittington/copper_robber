using UnityEngine;
using System.Collections;

public class Enemy : Movable
{
  public int playerDamage;
  private Animator animator;
  private Transform target;
  private bool skipMove;
  public AudioClip enemyAttack1;
  public AudioClip enemyAttack2;

  protected void Start()
  {
    GameManager.instance.AddEnemyToList(this);
    this.animator = GetComponent<Animator> ();
    this.target = GameObject.FindGameObjectWithTag("Player").transform;
  }

  protected override void AttemptMove<T> (int xDir, int yDir)
  {
    if (this.skipMove)
    {
      this.skipMove = false;

      return;
    }

    base.AttemptMove<T> (xDir, yDir);

    this.skipMove = true;
  }

  public void MoveEnemy()
  {
    int xDir = 0;
    int yDir = 0;

    if (Mathf.Abs(this.target.position.x - this.transform.position.x) <
        float.Epsilon)
    {
      yDir = this.target.position.y > this.transform.position.y ? 1 : -1;
    } else {
      xDir = this.target.position.x > this.transform.position.x ? 1 : -1;
    }

    this.AttemptMove<Player> (xDir, yDir);
  }

  protected override void OnCollision()
  {

  }

  protected override void OnCollisionWith <T> (T component)
  {
    Player hitPlayer = component as Player;

    this.animator.SetTrigger("enemyAttack");
    hitPlayer.LooseFood(this.playerDamage);
    SoundManager.instance.RandomizeSfx(enemyAttack1, enemyAttack2);
  }

  protected override void OnNoCollision()
  {

  }
}
