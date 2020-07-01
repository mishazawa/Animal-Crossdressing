using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class Terrain : MonoBehaviour {

  [SerializeField]
  public TileShape[] shapes;
  [SerializeField]
  public Material mat;
  [SerializeField]
  public bool debug = false;

  private ChunkData chunk = new ChunkData();

  void Awake () {
    chunk.GenerateChunk();
    chunk.TransformTiles();
    CombineMesh();
  }

  void CombineMesh () {
    CombineInstance[] combine = new CombineInstance[ChunkData.Length];

    chunk.Map((tile, i, arr) => {
      if (tile.kind == Data.TileType.Empty) return;
      var t = new GameObject(tile.name);
      var mf = t.AddComponent<MeshFilter>();
      var mr = t.AddComponent<MeshRenderer>();

      mf.mesh = shapes[(int)tile.shape].mesh;
      mr.material = mat;

      t.transform.localScale *= 50;
      t.transform.localRotation = Quaternion.Euler(-90, tile.rotation, 0);
      t.transform.position = tile.position;

      combine[i].mesh = shapes[(int)tile.shape].mesh;
      combine[i].transform = mf.transform.localToWorldMatrix;
      if (!debug) {
        t.SetActive(false);
        Destroy(t);
      }
    });

    transform.GetComponent<MeshFilter>().mesh = new Mesh();
    transform.GetComponent<MeshFilter>().mesh.CombineMeshes(combine);
    transform.gameObject.SetActive(true);
  }
}


 [Serializable]
 public struct TileShape {
     public Data.TileShape name;
     public Mesh mesh;
 }
