using UnityEngine;
using System.Collections;

namespace CopperRobber
{
  public class Game : MonoBehaviour
  {
    public static Game instance = null;
    public static Levels levels = null;

    void Awake()
    {
      InitStaticReference();
      InitGame();
    }

    void InitGame()
    {
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
  }
}
