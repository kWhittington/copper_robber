using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace CopperRobber
{
  public class Game : MonoBehaviour
  {
    public static Game instance = null;
    public Text scoreText;
    public Levels levels;
    public Tutorial tutorial;

    private Text levelText;
    private GameObject levelImage;
    private int score;

    public static void AdvanceInstanceLevel()
    {
      instance.AdvanceLevel();
    }

    public static bool Started()
    {
      return instance != null;
    }

    public string CurrentLevelName
    {
      get { return levels.CurrentLevelName; }
    }

    public int LoadedLevelIndex
    {
      get { return Application.loadedLevel; }
    }

    public string LoadedLevelName
    {
      get { return Application.loadedLevelName; }
    }

    public string NextLevelName
    {
      get { return levels.NextLevelName; }
    }

    public string PreviousLevelName
    {
      get { return levels.PreviousLevelName; }
    }

    void Awake()
    {
      InitStaticReference();
      InitGame();
    }

    public void AdvanceLevel()
    {
      if (!OnLastLevel())
      {
        LoadLevel(NextLevelName);
        levels.AdvanceLevel();
      }
    }

    public void ContinueTutorial()
    {
      if (OnLastScreen())
      {
        EndTutorial();
      } else {
        LoadLevel(tutorial.NextTutorialName);
        tutorial.AdvanceTutorial();
      }
    }

    public void EndTutorial()
    {
      tutorial.enabled = false;
      StartLevels();
    }

    public void IncreaseScoreBy(int amount)
    {
      score += amount;
    }

    void InitGame()
    {
      Debug.Log("Init Game");
      levels = GetComponent<Levels>();
      tutorial = GetComponent<Tutorial>();
      Debug.Log("Levels: " + levels.ToString());
      score = 0;
    }

    void InitStaticReference()
    {
      if (instance == null)
      {
        instance = this;
      } else if (instance != this) {
        Destroy(gameObject);
      }
      DontDestroyOnLoad(gameObject);
    }

    public void LoadLevel(string levelName)
    {
      Application.LoadLevel(levelName);
    }

    public bool OnLastLevel()
    {
      return levels.OnLastLevel();
    }

    public bool OnLastScreen()
    {
      return tutorial.OnLastScreen();
    }

    public void StartLevels()
    {
      levels.Reset();
      LoadLevel(levels.CurrentLevelName);
    }
  }
}
