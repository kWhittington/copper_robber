using UnityEngine;
using System.Collections;

namespace CopperRobber
{
  public class CanExit : MonoBehaviour {
    public int scorePoints;

    void OnTriggerEnter2D(Collider2D other)
    {
      if (other.CompareTag("Exit"))
      {
        OnExitEnter();
      }
    }

    void OnExitEnter()
    {
      ExitLevel();
    }

    void ExitLevel()
    {
      Game.instance.AdvanceLevel();
    }
  }
}
