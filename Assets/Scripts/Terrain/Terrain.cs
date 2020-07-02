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
    transform.GetComponent<MeshFilter>().mesh = new Mesh();
    chunk.GenerateChunk();
    chunk.GenerateChunk();
    var combine = new CombineInstance[ChunkData.Length * chunk.layers.Count];

    for(int i = 0; i < chunk.layers.Count; i++){
      chunk.TransformTiles(chunk.layers[i], i);
      CombineMesh(combine, i, chunk.layers[i]);
    }

    transform.GetComponent<MeshFilter>().mesh.CombineMeshes(combine, true);
  }

  void CombineMesh (CombineInstance[] combine, int index, Tile[,] tiles) {

    for (int i = 0; i < ChunkData.Length; i++) {
      var x = i / Constants.CHUNK_SIZE;
      var y = i % Constants.CHUNK_SIZE;

      if (tiles[x, y].kind == Data.TileType.Empty) continue;

      var t = new GameObject(tiles[x, y].name);

      var mf = t.AddComponent<MeshFilter>();
      var mr = t.AddComponent<MeshRenderer>();

      mf.mesh = shapes[(int)tiles[x, y].shape].mesh;
      mr.material = mat;

      t.transform.localScale *= 50;
      t.transform.localRotation = Quaternion.Euler(-90, tiles[x, y].rotation, 0);
      t.transform.position = tiles[x, y].position;

      combine[index * i].mesh = shapes[(int)tiles[x, y].shape].mesh;
      combine[index * i].transform = mf.transform.localToWorldMatrix;

      if (!debug) {
        t.SetActive(false);
        Destroy(t);
      }
    }

  }
}


 [Serializable]
 public struct TileShape {
     public Data.TileShape name;
     public Mesh mesh;
 }
