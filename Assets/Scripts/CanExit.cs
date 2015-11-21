using UnityEngine;
using System.Collections;

public class CanExit : MonoBehaviour {
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
  }
}
