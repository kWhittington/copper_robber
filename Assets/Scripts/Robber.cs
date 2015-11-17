using UnityEngine;
using System.Collections;

public class Robber : MovingObject
{

	// Use this for initialization
	void Start ()
	{
		base.Start ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		int horizontal = 0;
		int vertical = 0;

		horizontal = (int)Input.GetAxisRaw ("Horizontal");
		vertical = (int)Input.GetAxisRaw ("Vertical");

		if (horizontal != 0) {
			vertical = 0;
			System.Console.WriteLine ("This Also Ran");
		}

		if (horizontal != 0 || vertical != 0) {
			AttemptMove<Wall> (horizontal, vertical);
		}
	}

	protected override void AttemptMove<T> (int xDir, int yDir)
	{
		System.Console.WriteLine ("This Ran");
		base.AttemptMove<T> (xDir, yDir);
		RaycastHit2D hit;
		if (Move (xDir, yDir, out hit)) {
		}
	}

	protected override void OnCantMove<T> (T component)
	{
	}
}
