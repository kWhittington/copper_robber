using UnityEngine;
using System.Collections;

namespace CopperRobber
{
  public class Tutorial : MonoBehaviour
  {
    public string[] tutorialProgression;

    private int currentTutorialIndex;

    public int CurrentTutorialIndex
    {
      get { return currentTutorialIndex; }
    }

    public string CurrentTutorialName
    {
      get { return tutorialProgression[CurrentTutorialIndex]; }
    }

    public int NextTutorialIndex
    {
      get { return CurrentTutorialIndex + 1; }
    }

    public string NextTutorialName
    {
      get { return tutorialProgression[NextTutorialIndex]; }
    }

    public int PreviousTutorialIndex
    {
      get { return CurrentTutorialIndex - 1; }
    }

    public string PreviousTutorialName
    {
      get { return tutorialProgression[PreviousTutorialIndex]; }
    }

    public void AdvanceTutorial()
    {
      if (!OnLastScreen())
      {
        currentTutorialIndex += 1;
      }
    }

    public bool ContinueInputDetected()
    {
      return Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.Return);
    }

    public void ContinueToNextScreen()
    {
      Game.instance.ContinueTutorial();
    }

    public bool OnLastScreen()
    {
      return CurrentTutorialIndex >= tutorialProgression.Length - 1;
    }

    public void Reset()
    {
      currentTutorialIndex = 0;
    }

    public override string ToString()
    {
      string result = "< Tutorial: ";

      foreach (string tutorialName in tutorialProgression)
      {
        result += tutorialName + ", ";
      }

      result += ">";

      return result;
    }

    void Update()
    {
      if (ContinueInputDetected())
      {
        ContinueToNextScreen();
      }
    }
  }
}
