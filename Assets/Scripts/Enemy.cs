using UnityEngine;
using System.Collections;

public class Enemy : MovingObject
{
	public int playerDamage;
	private Animator animator;
	private Transform target;
	private bool skipMove;

	protected override void Start ()
	{
		GameManager.instance.AddEnemyToList (this);
		this.animator = GetComponent<Animator> ();
		this.target = GameObject.FindGameObjectWithTag ("Player").transform;
		base.Start ();
	}

	protected override void AttemptMove<T> (int xDir, int yDir)
	{
		if (this.skipMove) {
			this.skipMove = false;
			return;
		}

		base.AttemptMove<T> (xDir, yDir);

		this.skipMove = true;
	}

	public void MoveEnemy ()
	{
		int xDir = 0;
		int yDir = 0;

		if (Mathf.Abs (this.target.position.x - this.transform.position.x) < float.Epsilon) {
			yDir = this.target.position.y > this.transform.position.y ? 1 : -1;
		} else {
			xDir = this.target.position.x > this.transform.position.x ? 1 : -1;
		}

		this.AttemptMove<Player> (xDir, yDir);
	}

	protected override void OnCantMove <T> (T component)
	{
		Player hitPlayer = component as Player;
		this.animator.SetTrigger ("enemyAttack");
		hitPlayer.LooseFood (this.playerDamage);
	}
}
