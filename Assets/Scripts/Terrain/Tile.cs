using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data;

public class Tile {
  public Vector3           position {get; private set;}
  public float             rotation {get; private set;}
  public Data.TileType     kind     {get; set;}
  public Data.TileShape    shape    {get; set;}
  public string name = ";";

  public Tile (Vector3 p) {
    position = p;
    if (Random.Range(0f, 1f) > 0.6f) {
      kind = Data.TileType.Empty;
    }
  }

  public void UpdateShape (Tile[,] world, int x, int y) {
    if (kind == Data.TileType.Empty) return;
    if (kind == Data.TileType.Half)  return;

    var isOutTileTop    = x == 0;
    var isOutTileBottom = x == world.GetLength(0) - 1;
    var isOutTileLeft   = y == 0;
    var isOutTileRight  = y == world.GetLength(1) - 1;

    var isTopEmpty    = !isOutTileTop    && world[x-1, y].kind == Data.TileType.Empty;
    var isBottomEmpty = !isOutTileBottom && world[x+1, y].kind == Data.TileType.Empty;
    var isLeftEmpty   = !isOutTileLeft   && world[x, y-1].kind == Data.TileType.Empty;
    var isRightEmpty  = !isOutTileRight  && world[x, y+1].kind == Data.TileType.Empty;

    var isTREmpty     = !isOutTileRight && !isOutTileTop && world[x-1, y+1].kind == Data.TileType.Empty;
    var isTLEmpty     = !isOutTileLeft  && !isOutTileTop && world[x-1, y-1].kind == Data.TileType.Empty;
    var isBREmpty     = !isOutTileRight && !isOutTileBottom && world[x+1, y+1].kind == Data.TileType.Empty;
    var isBLEmpty     = !isOutTileLeft  && !isOutTileBottom && world[x+1, y-1].kind == Data.TileType.Empty;

    var sidesOut   = new bool[] { isOutTileTop, isOutTileBottom, isOutTileLeft, isOutTileRight };
    var sidesEmpty = new bool[] { isTopEmpty, isBottomEmpty, isLeftEmpty, isRightEmpty };

    var isAlone = sidesOut.Where(c => c).Count() + sidesEmpty.Where(c => c).Count();

    if (isAlone >= 3) {
      shape = Data.TileShape.Full;
      return;
    }

    // alone
    if ((isTopEmpty && isBottomEmpty && isLeftEmpty && isRightEmpty) || (isOutTileTop && isOutTileBottom && isOutTileLeft && isOutTileRight)) {
      shape = Data.TileShape.Full;
      return;
    }

    // top
    if (!isLeftEmpty && !isTopEmpty && !isBottomEmpty && !isRightEmpty && !isOutTileTop && !isOutTileBottom && !isOutTileLeft && !isOutTileRight) {
      shape = Data.TileShape.Top;
      return;
    }

    // corners
    // TR
    if (
        (isOutTileRight && isOutTileTop) ||
        (isRightEmpty && isTopEmpty)     ||
        (isOutTileRight && isTopEmpty)   ||
        (isRightEmpty && isOutTileTop)
       ) {
      shape = Data.TileShape.Corner;
      rotation = -90f;
      name = "[" + position + "]" + "TR";
      if (isTREmpty) {
        shape = Data.TileShape.Triangle;
        rotation = 0f;
        kind = Data.TileType.Half;
      }
      return;
    }

    if (
        (isOutTileLeft && isOutTileTop) ||
        (isLeftEmpty && isTopEmpty)     ||
        (isOutTileLeft && isTopEmpty)   ||
        (isLeftEmpty && isOutTileTop)
       ) {
      shape = Data.TileShape.Corner;
      rotation = 180f;
      name = "[" + position + "]" + "TL";
      if (isTLEmpty) {
        shape = Data.TileShape.Triangle;
        rotation = -90f;
        kind = Data.TileType.Half;

      }
      return;
    }

    if (
        (isOutTileLeft && isOutTileBottom) ||
        (isLeftEmpty && isOutTileBottom)   ||
        (isOutTileLeft && isBottomEmpty)   ||
        (isLeftEmpty && isBottomEmpty)
       ) {
      shape = Data.TileShape.Corner;
      rotation = 90f;
      name = "[" + position + "]" + "BL";
      if (isBLEmpty) {
        shape = Data.TileShape.Triangle;
        rotation = 180f;
        kind = Data.TileType.Half;
      }
      return;
    }

    if (
        (isOutTileRight && isOutTileBottom) ||
        (isRightEmpty && isOutTileBottom) ||
        (isOutTileRight && isBottomEmpty) ||
        (isRightEmpty && isBottomEmpty)
       ) {
      shape = Data.TileShape.Corner;
      rotation = 0f;
      name = "[" + position + "]" + "BR";
      if (isBREmpty) {
        shape = Data.TileShape.Triangle;
        rotation = 90f;
        kind = Data.TileType.Half;
      }
      return;
    }


    if ((isLeftEmpty && isRightEmpty) || (isOutTileLeft && isRightEmpty) || (isOutTileRight && isLeftEmpty)) {
      shape = Data.TileShape.Both;
      return;
    }

    if ((isBottomEmpty && isTopEmpty) || (isOutTileBottom && isTopEmpty) || (isOutTileTop && isBottomEmpty)) {
      shape = Data.TileShape.Both;
      rotation = 90f;
      return;
    }

    shape = Data.TileShape.Front;

    if (isTopEmpty || isOutTileTop) {
      rotation = -90f;
    }

    if (isBottomEmpty || isOutTileBottom) {
      rotation = 90f;
    }

    if (isLeftEmpty || isOutTileLeft) {
      rotation = 180f;
    }
  }
}
