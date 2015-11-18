using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : Movable
{
  public int wallDamage = 1;
  public int pointsPerFood = 10;
  public int pointsPerSoda = 20;
  public float restartLevelDelay = 1f;
  public Text foodText;
  public AudioClip moveSound1;
  public AudioClip moveSound2;
  public AudioClip eatSound1;
  public AudioClip eatSound2;
  public AudioClip drinkSound1;
  public AudioClip drinkSound2;
  public AudioClip gameOverSound;

  private Animator animator;
  private int food;

  // Use this for initialization
  protected void Start()
  {
    this.animator = GetComponent<Animator> ();
    this.food = GameManager.instance.playerFoodPoints;
    this.foodText.text = "Food: " + this.food;
  }

  private void OnDisable()
  {
    GameManager.instance.playerFoodPoints = this.food;
  }

  // Update is called once per frame
  void Update()
  {
    if (!GameManager.instance.playersTurn)
    {
      return;
    }

    int horizontal = 0;
    int vertical = 0;

    horizontal = (int)Input.GetAxisRaw("Horizontal");
    vertical = (int)Input.GetAxisRaw("Vertical");

    if (horizontal != 0)
    {
      vertical = 0;
    }

    if (horizontal != 0 || vertical != 0)
    {
      this.AttemptMove<Wall> (horizontal, vertical);
    }
  }

  protected override void AttemptMove<T> (int xDir, int yDir)
  {
    this.food--;
    this.foodText.text = "Food: " + this.food;
    base.AttemptMove<T> (xDir, yDir);
    Move(xDir, yDir);
    if (!Collided)
    {
      SoundManager.instance.RandomizeSfx(moveSound1, moveSound2);
    }
    this.CheckIfGameOver();

    GameManager.instance.playersTurn = false;
  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.tag == "Exit")
    {
      this.Invoke("Restart", this.restartLevelDelay);
      this.enabled = false;
    } else if (other.tag == "Food") {
      this.food += this.pointsPerFood;
      this.foodText.text = "+" + this.pointsPerFood + " Food: " + this.food;
      SoundManager.instance.RandomizeSfx(eatSound1, eatSound2);
      other.gameObject.SetActive(false);
    } else if (other.tag == "Soda") {
      this.food += this.pointsPerSoda;
      this.foodText.text = "+" + this.pointsPerSoda + " Food: " + this.food;
      SoundManager.instance.RandomizeSfx(drinkSound1, drinkSound2);
      other.gameObject.SetActive(false);
    }
  }

  protected override void OnCantMove<T> (T component)
  {
    Wall hitWall = component as Wall;

    hitWall.DamageWall(this.wallDamage);
    this.animator.SetTrigger("playerChop");
  }

  private void Restart()
  {
    Application.LoadLevel(Application.loadedLevel);
  }

  public void LooseFood(int loss)
  {
    this.animator.SetTrigger("playerHit");
    this.food -= loss;
    this.foodText.text = "-" + loss + " Food: " + this.food;
    this.CheckIfGameOver();

  }

  private void CheckIfGameOver()
  {
    if (this.food <= 0)
    {
      SoundManager.instance.PlaySingle(gameOverSound);
      SoundManager.instance.musicSource.Stop();
      GameManager.instance.GameOver();
    }
  }
}
