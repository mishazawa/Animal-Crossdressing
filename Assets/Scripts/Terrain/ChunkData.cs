using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Data;

public class ChunkData {

  public static readonly int Length = Constants.CHUNK_SIZE * Constants.CHUNK_SIZE;
  public Tile[,] tiles = new Tile[Constants.CHUNK_SIZE, Constants.CHUNK_SIZE];

  public void GenerateChunk () {
    for (int x = 0; x < Constants.CHUNK_SIZE; x++) {
      for (int y = 0; y < Constants.CHUNK_SIZE; y++) {
        var tile = new Tile(new Vector3(x, 0f, y));
        tiles[x, y] = tile;
      }
    }
  }

  public void TransformTiles () {
    for (int x = 0; x < Constants.CHUNK_SIZE; x++) {
      for (int y = 0; y < Constants.CHUNK_SIZE; y++) {
        tiles[x, y].UpdateShape(tiles, x, y);
      }
    }
  }

  public GameObject[] Map (Action<Tile, int, GameObject[]> callback) {
    var len = Constants.CHUNK_SIZE * Constants.CHUNK_SIZE;
    var objects = new GameObject[len];

    for (int i = 0; i < len; i++) {
        var x = i / Constants.CHUNK_SIZE;
        var y = i % Constants.CHUNK_SIZE;
        callback(tiles[x, y], i, objects);
    }

    return objects;
  }
}
