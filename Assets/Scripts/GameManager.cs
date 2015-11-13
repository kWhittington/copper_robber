using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

	public float levelStartDelay = 2f;
	public float turnDelay = .1f;
	public static GameManager instance = null;
	public BoardManager boardScript;
	public int playerFoodPoints = 100;
	[HideInInspector]
	public bool
		playersTurn = true;

	private Text levelText;
	private GameObject levelImage;
	private int level = 1;
	private List<Enemy> enemies;
	private bool enemiesMoving;
	private bool doingSetup;


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

	private void OnLevelWasLoaded (int index)
	{
		this.level++;
		this.InitGame ();
	}

	void InitGame ()
	{
		this.doingSetup = true;

		this.levelImage = GameObject.Find ("LevelImage");
		this.levelText = GameObject.Find ("LevelText").GetComponent<Text> ();
		this.levelText.text = "Day " + this.level;
		this.levelImage.SetActive (true);
		this.Invoke ("HideLevelImage", this.levelStartDelay);

		this.enemies.Clear ();
		this.boardScript.SetupScene (level);
	}

	private void HideLevelImage ()
	{
		this.levelImage.SetActive (false);
		this.doingSetup = false;
	}

	public void GameOver ()
	{
		this.levelText.text = "After " + this.level + " days, you starved.";
		this.levelImage.SetActive (true);
		this.enabled = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (this.playersTurn || this.enemiesMoving || this.doingSetup) {
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
