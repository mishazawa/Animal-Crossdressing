using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VoxelData {

  public static readonly int CHUNK_SIZE = 32;

  public static readonly int VOXEL_FACE_SIZE = 6;

  public static readonly Vector3[] vertices = new Vector3[8] {
    new Vector3(0f, 0f, 0f),
    new Vector3(1f, 0f, 0f),
    new Vector3(1f, 1f, 0f),
    new Vector3(0f, 1f, 0f),
    new Vector3(0f, 0f, 1f),
    new Vector3(1f, 0f, 1f),
    new Vector3(1f, 1f, 1f),
    new Vector3(0f, 1f, 1f),
  };

  public static readonly int[,] triangles = {
    {0, 3, 1, 2}, // Back Face
    {5, 6, 4, 7}, // Front Face
    {3, 7, 2, 6}, // Top Face
    {1, 5, 0, 4}, // Bottom Face
    {4, 7, 0, 3}, // Left Face
    {1, 2, 5, 6}  // Right Face
  };

  public static readonly Vector2[] uvs = {
    new Vector2(0f, 0f),
    new Vector2(0f, 1f),
    new Vector2(1f, 0f),
    new Vector2(1f, 1f)
  };

  public static readonly int[,] faces = {
    { 0,  0, -1},
    { 0,  0,  1},
    { 0,  1,  0},
    { 0, -1,  0},
    {-1,  0,  0},
    { 1,  0,  0}
  };

}
