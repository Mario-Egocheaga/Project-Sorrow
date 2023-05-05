using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridLevelGenerator : MonoBehaviour
{
    public Transform parent;

    public GameObject[] props;  // Array of prefabs for props to be placed in the level
    public int gridWidth = 10;  // Width of the grid
    public int gridHeight = 10;  // Height of the grid
    public float cellSize = 1f;  // Size of each grid cell
    public float propDensity = 0.5f;  // Density of props in the grid
    public int numLayers = 3;  // Number of layers of props to generate

    public float minPropDistance = 2f;  // Minimum distance between props

    private bool ranY, ranX;

    void Start()
    {
        parent = this.transform;
        GenerateLevel();
    }

    void GenerateLevel()
    {
        // Loop through each cell in the grid
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                // Loop through each layer of props
                for (int layer = 0; layer < numLayers; layer++)
                {
                    // Choose whether to place a prop in this cell and layer based on the prop density
                    if (Random.value < propDensity)
                    {
                        // Choose a random prop from the array
                        GameObject prop = props[Random.Range(0, props.Length)];

                        // Calculate the position of the prop within the cell and layer
                        Vector3 position = new Vector3((x - gridWidth / 2f + 0.5f) * cellSize, layer * cellSize, (y - gridHeight / 2f + 0.5f) * cellSize);

                        // Instantiate the prop at the calculated position
                        GameObject propPlaced = Instantiate(prop, position, Quaternion.identity);
                        propPlaced.transform.parent = parent.transform;
                        propPlaced.transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y + Random.Range(0f, 360f), transform.rotation.z);

                    }
                }
            }
        }
    }

    private Vector3 GetRandomRots(Vector3 currentRot)
    {
        float x = ranX ? Random.Range(0f, 360f) : currentRot.x;
        float y = ranY ? Random.Range(0f, 360f) : currentRot.y;

        return new Vector3(x, y, this.transform.position.z);
    }
}