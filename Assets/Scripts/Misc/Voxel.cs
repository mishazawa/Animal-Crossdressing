using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Voxel : MonoBehaviour {

    [SerializeField]
    MeshRenderer renderer;
    [SerializeField]
    MeshFilter filter;

    private static int CHUNK_LEN = VoxelData.CHUNK_SIZE * VoxelData.CHUNK_SIZE * VoxelData.CHUNK_SIZE;
    private static int VOXEL_LEN = VoxelData.VOXEL_FACE_SIZE * VoxelData.VOXEL_FACE_SIZE;
    // private int[]     triangles = new int[VOXEL_LEN * CHUNK_LEN];
    // private Vector3[] vertices  = new Vector3[VOXEL_LEN * CHUNK_LEN];
    // private Vector2[] uvs       = new Vector2[VOXEL_LEN * CHUNK_LEN];
    private int vertexIndex = 0;

    private List<int>     triangles = new List<int>();
    private List<Vector3> vertices  = new List<Vector3>();
    private List<Vector2> uvs       = new List<Vector2>();

    private bool [,,] voxelMap = new bool[VoxelData.CHUNK_SIZE, VoxelData.CHUNK_SIZE, VoxelData.CHUNK_SIZE];

    void Start () {
      MockChunkMap();
      CreateMeshData();
      Debug.Log(vertexIndex);
      CreateMesh();

    }

    void CreateMeshData () {
      for (int i = 0; i < CHUNK_LEN; i++) {
        var x = i / (VoxelData.CHUNK_SIZE * VoxelData.CHUNK_SIZE) % VoxelData.CHUNK_SIZE;
        var y = i / VoxelData.CHUNK_SIZE % VoxelData.CHUNK_SIZE;
        var z = i % VoxelData.CHUNK_SIZE;
        GenerateVoxelData(x, y, z);
      }
    }


    void GenerateVoxelData (int x, int y, int z) {
      for (int i = 0; i < VoxelData.VOXEL_FACE_SIZE; i++) {
        if (!IsSolidVoxel(x + VoxelData.faces[i, 0], y + VoxelData.faces[i, 1], z + VoxelData.faces[i, 2])) {
          var pos = new Vector3(x, y, z);

          vertices.Add(VoxelData.vertices[VoxelData.triangles[i, 0]]  + pos);
          vertices.Add(VoxelData.vertices[VoxelData.triangles[i, 1]]  + pos);
          vertices.Add(VoxelData.vertices[VoxelData.triangles[i, 2]]  + pos);
          vertices.Add(VoxelData.vertices[VoxelData.triangles[i, 3]]  + pos);

          uvs.Add(VoxelData.uvs[0]);
          uvs.Add(VoxelData.uvs[1]);
          uvs.Add(VoxelData.uvs[2]);
          uvs.Add(VoxelData.uvs[3]);

          triangles.Add(vertexIndex);
          triangles.Add(vertexIndex + 1);
          triangles.Add(vertexIndex + 2);
          triangles.Add(vertexIndex + 2);
          triangles.Add(vertexIndex + 1);
          triangles.Add(vertexIndex + 3);

          vertexIndex += 4;
        }
        /*

        vertices[vertexIndex]     = VoxelData.vertices[VoxelData.triangles[i, 0]]  + pos;
        vertices[vertexIndex + 1] = VoxelData.vertices[VoxelData.triangles[i, 1]]  + pos;
        vertices[vertexIndex + 2] = VoxelData.vertices[VoxelData.triangles[i, 2]]  + pos;
        vertices[vertexIndex + 3] = VoxelData.vertices[VoxelData.triangles[i, 3]]  + pos;

        uvs[vertexIndex]     = VoxelData.uvs[0];
        uvs[vertexIndex + 1] = VoxelData.uvs[1];
        uvs[vertexIndex + 2] = VoxelData.uvs[2];
        uvs[vertexIndex + 3] = VoxelData.uvs[3];

        triangles[vertexIndex]     = vertexIndex;
        triangles[vertexIndex + 1] = vertexIndex + 1;
        triangles[vertexIndex + 2] = vertexIndex + 2;
        triangles[vertexIndex + 3] = vertexIndex + 2;
        triangles[vertexIndex + 4] = vertexIndex + 1;
        triangles[vertexIndex + 5] = vertexIndex + 3;

        vertexIndex += 4;
        */
      }
    }

    void CreateMesh () {
      var mesh = new Mesh() {
        // vertices  = vertices,
        // triangles = triangles,
        // uv        = uvs
        vertices  = vertices.ToArray(),
        triangles = triangles.ToArray(),
        uv        = uvs.ToArray()
      };

      mesh.RecalculateNormals();

      filter.mesh = mesh;
    }

    private bool IsSolidVoxel (int x, int y, int z) {
      if (x < 0 ||
          x > VoxelData.CHUNK_SIZE - 1 ||
          y < 0 ||
          y > VoxelData.CHUNK_SIZE - 1 ||
          z < 0 ||
          z > VoxelData.CHUNK_SIZE - 1) return false;
      return voxelMap[x, y, z];
    }

    private void MockChunkMap () {
      for (int i = 0; i < CHUNK_LEN; i++) {
        var x = i / (VoxelData.CHUNK_SIZE * VoxelData.CHUNK_SIZE) % VoxelData.CHUNK_SIZE;
        var y = i / VoxelData.CHUNK_SIZE % VoxelData.CHUNK_SIZE;
        var z = i % VoxelData.CHUNK_SIZE;
        voxelMap[x, y, z] = true;
      }
    }

}
