using UnityEngine;
using System.Collections;

namespace CopperRobber
{
  public class LevelTransition : MonoBehaviour {
    void OnMouseDown()
    {
      Debug.Log("Was clicked");
      Game.AdvanceInstanceLevel();
    }

    void OnMouseEnter()
    {
      Debug.Log("ENTERED");
    }
  }
}
