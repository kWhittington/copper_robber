using UnityEngine;
using System;

[ExecuteInEditMode]
public class Gridable : MonoBehaviour {
  public float cell_size = 1f;

  public Vector3 GridPosition()
  {
    return new Vector3(GridX(), GridY(), GridZ());
  }

  public float GridX()
  {
    return Mathf.Round(transform.position.x / cell_size) * cell_size;
  }

  public float GridY()
  {
    return Mathf.Round(transform.position.y / cell_size) * cell_size;
  }

  public float GridZ()
  {
    return transform.position.z;
  }

  public void SnapToGrid()
  {
    transform.position = GridPosition();
  }

  void Update()
  {
    SnapToGrid();
  }
}
