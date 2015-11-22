using UnityEngine;
using System.Collections;

namespace CopperRobber
{
  public class Levels : MonoBehaviour
  {
    public string[] levelProgression;

    private int currentLevelIndex;

    public int CurrentLevelIndex
    {
      get { return currentLevelIndex; }
    }

    public string CurrentLevelName
    {
      get { return levelProgression[CurrentLevelIndex]; }
    }

    public int NextLevelIndex
    {
      get { return CurrentLevelIndex + 1; }
    }

    public string NextLevelName
    {
      get { return levelProgression[NextLevelIndex]; }
    }

    public int PreviousLevelIndex
    {
      get { return CurrentLevelIndex - 1; }
    }

    public string PreviousLevelName
    {
      get { return levelProgression[PreviousLevelIndex]; }
    }

    public void AdvanceLevel()
    {
      if (!OnLastLevel())
      {
        currentLevelIndex += 1;
      }
    }

    public bool OnLastLevel()
    {
      return CurrentLevelIndex >= levelProgression.Length - 1;
    }

    public void Reset()
    {
      currentLevelIndex = 0;
    }

    public bool RestartInputDetected()
    {
      return Input.GetKeyUp(KeyCode.R);
    }

    public void RestartCurrentLevel()
    {
      Game.instance.LoadLevel(CurrentLevelName);
    }

    public override string ToString()
    {
      string result = "< Levels: ";

      foreach (string levelName in levelProgression)
      {
        result += levelName + ", ";
      }

      result += ">";

      return result;
    }

    void Update()
    {
      if (RestartInputDetected())
      {
        if (OnLastLevel())
        {
          Game.instance.StartLevels();
        } else {
          RestartCurrentLevel();
        }
      }
    }
  }
}
