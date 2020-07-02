using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Data;

public class ChunkData {

  public static readonly int Length = Constants.CHUNK_SIZE * Constants.CHUNK_SIZE;
  public List<Tile[,]> layers = new List<Tile[,]>();

  public void GenerateChunk () {
    var tiles = new Tile[Constants.CHUNK_SIZE, Constants.CHUNK_SIZE];
    for (int x = 0; x < Constants.CHUNK_SIZE; x++) {
      for (int y = 0; y < Constants.CHUNK_SIZE; y++) {
        var tile = new Tile(new Vector3(x, 0.2f * layers.Count , y));
        tiles[x, y] = tile;
      }
    }
    layers.Add(tiles);
  }

  public void TransformTiles (Tile[,] tiles, int layerIndex) {
    for (int x = 0; x < Constants.CHUNK_SIZE; x++) {
      for (int y = 0; y < Constants.CHUNK_SIZE; y++) {
        if (Random.Range(0f, 1f) > 0.6f) {
          tiles[x, y].kind = Data.TileType.Empty;
        }

        tiles[x, y].UpdateShape(layers, layerIndex, x, y);
      }
    }
  }
}
