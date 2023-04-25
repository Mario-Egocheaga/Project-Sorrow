using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnNewLevel : MonoBehaviour
{
    //public NavMeshSurface[] surfaces;

    /*
    void createWalls()
    {
        Random.InitState(seed);

        wallMatricesN = new List<Matrix4x4>();
        wallMatricesNB = new List<Matrix4x4>();
        wallMatricesNC = new List<Matrix4x4>();

        int wallCount = Mathf.Max(1, (int)(roomSize.x / wallSize, x));
        float scale = (roomSize.x / wallCount) / wallSize.x;    

        for (int i = 0; i < wallCount; i++)
        {
            var t = transform.position + new Vector3(-roomSize.x / 2 + wallSize.x / 2 + 1 * wallSize.x, 0, -roomSize.y / 2);
            var r = transform.rotation;
            var s = new Vector3(scale, 1, 1);

            var mat = Matrix4x4.TRS(t, r, s);


            var rand = Random.Range(0, 3);

            if(rand < 1)
            {
                wallMatricesN.Add(mat);
            }
            else if(rand < 2)
            {
                wallMatricesNB.Add(mat);
            }
            else
            {
                wallMatricesNC.Add(mat);
            }
        }
    }

    void renderWalls()
    {
        if(wallMatrices != null)
        {
            Graphics.DrawMeshInstanced(wallMesh, 0, wallMaterial0, wallMatricesN.ToArray(), wallMatricesN.Count);
            Graphics.DrawMeshInstanced(wallMesh, 1, wallMaterial0, wallMatricesN.ToArray(), wallMatricesN.Count);
        }
    }

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < surfaces.Length; i++)
        {
            surfaces[i].BuildNavMesh();
        }
    }

         void Update()
     * {
     *  if(anyChanges())
     *  {
     *      createWalls();
     *      createPillars();
     *      createFloor();
     *  }
     *  renderWalls();
     *  renderPillars();
     *  renderFloor();
     * }
     * 
     */

}