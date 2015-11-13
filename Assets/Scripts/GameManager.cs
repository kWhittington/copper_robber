using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{

	public float turnDelay = .1f;
	public static GameManager instance = null;
	public BoardManager boardScript;
	public int playerFoodPoints = 100;
	[HideInInspector]
	public bool
		playersTurn = true;

	private int level = 3;
	private List<Enemy> enemies;
	private bool enemiesMoving;


	// Use this for initialization
	void Awake ()
	{
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy (gameObject);
		}
		DontDestroyOnLoad (gameObject);
		this.enemies = new List<Enemy> ();
		this.boardScript = GetComponent<BoardManager> ();
		InitGame ();
	}

	void InitGame ()
	{
		this.enemies.Clear ();
		this.boardScript.SetupScene (level);
	}

	public void GameOver ()
	{
		this.enabled = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (this.playersTurn || this.enemiesMoving) {
			return;
		}

		StartCoroutine (MoveEnemies ());
	}

	public void AddEnemyToList (Enemy script)
	{
		this.enemies.Add (script);
	}

	IEnumerator MoveEnemies ()
	{
		this.enemiesMoving = true;
		yield return new WaitForSeconds (this.turnDelay);
		if (enemies.Count == 0) {
			yield return new WaitForSeconds (turnDelay);
		}

		for (int i = 0; i < this.enemies.Count; i++) {
			this.enemies [i].MoveEnemy ();
			yield return new WaitForSeconds (enemies [i].moveTime);
		}

		this.playersTurn = true;
		this.enemiesMoving = false;
	}
}
