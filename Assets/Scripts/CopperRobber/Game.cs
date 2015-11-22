using UnityEngine;
using System.Collections;

namespace CopperRobber
{
  public class Game : MonoBehaviour
  {
    public static Game instance = null;

    private Levels levels;

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
      get { return Application.LoadedLevelName; }
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

    void InitGame()
    {
      levels = GetComponent<CopperRobber.Levels>();
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

    public void AdvanceLevel()
    {
      if (!OnLastLevel())
      {
        LoadLevel(NextLevelName);
      }
    }

    public void LoadLevel(string levelName)
    {
      Application.LoadLevel(levelName);
    }

    public bool OnLastLevel()
    {
      return levels.OnLastLevel();
    }
  }
}
