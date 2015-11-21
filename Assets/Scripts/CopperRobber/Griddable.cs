using UnityEngine;
using System;

namespace CopperRobber
{
  [ExecuteInEditMode]
  public class Griddable : MonoBehaviour {
    public float cell_size = 1f;

    public Vector3 Position
    {
      get { return new Vector3(X, Y, Z); }
    }

    public float X
    {
      get { return Mathf.Round(transform.position.x / cell_size) * cell_size; }
    }

    public float Y
    {
      get { return Mathf.Round(transform.position.y / cell_size) * cell_size; }
    }

    public float Z
    {
      get { return transform.position.z; }
    }


    public void SnapToGrid()
    {
      transform.position = Position;
    }

    void Update()
    {
      SnapToGrid();
    }
  }
}
