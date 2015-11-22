using UnityEngine;
using System.Collections;

namespace CopperRobber
{
  public class Loader : MonoBehaviour
  {
    public GameObject game;

    void Awake()
    {
      if (Game.instance == null)
      {
        Instantiate(game);
      }
    }
  }
}
