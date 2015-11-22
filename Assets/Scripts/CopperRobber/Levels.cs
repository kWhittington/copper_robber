using UnityEngine;
using System.Collections;

namespace CopperRobber
{
  public class Levels : MonoBehaviour {
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
  }
}
