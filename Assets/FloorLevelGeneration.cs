using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class FloorLevelGeneration : MonoBehaviour
{
    public Transform parent;

    //Level
    public int width = 30;
    public int length = 30;

    public GameObject floorPrefab;
    public GameObject wallPrefab;

    public NavMeshSurface[] surfaces;

    private void Start()
    {
        parent = this.transform;
        GenerateWalls();
        ChangeTransform();
    }

    void GenerateWalls()
    {
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < length; z++)
            {
                if (x == 0 || x == width - 1 || z == 0 || z == length - 1)
                {
                    // Generate walls on the edge of the level
                    if (z == 0) // facing south
                    {
                        Quaternion rotation = Quaternion.Euler(0, -90, 0);
                        GameObject wall = Instantiate(wallPrefab, new Vector3(x, 0, z), rotation);
                        wall.transform.parent = parent.transform;
                        
                    }
                    else if (z == length - 1) // facing north
                    {
                        Quaternion rotation = Quaternion.Euler(0, 90, 0);
                        GameObject wall = Instantiate(wallPrefab, new Vector3(x, 0, z), rotation);
                        wall.transform.parent = parent.transform;
                    }
                    else if (x == 0) // facing west
                    {
                        Quaternion rotation = Quaternion.Euler(0, 180, 0);
                        GameObject wall = Instantiate(wallPrefab, new Vector3(x, 0, z - 1), rotation);
                        wall.transform.parent = parent.transform;
                    }
                    else if (x == width - 1) // facing east
                    {
                        Quaternion rotation = Quaternion.Euler(0, 180, 0);
                        GameObject wall = Instantiate(wallPrefab, new Vector3(x, 0, z - 1), rotation);
                        wall.transform.parent = parent.transform;
                    }
                }
                else
                {
                    // Generate floor
                    GameObject floor = Instantiate(floorPrefab, new Vector3(x, this.transform.position.y, z), Quaternion.identity);
                    floor.transform.parent = parent.transform;
                    
                }
            }
        }
    }

    void ChangeTransform()
    {
        parent.transform.position = new Vector3(this.transform.position.x - 17, 0, this.transform.position.z - 16);
    }

}
