using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardColumn
{
    // Used to fetch available planet prefabs
    PlanetsPool prefabPool;

    // Holds planets in column
    List<GameObject> planets;

    // Rows in column
    int rows;

    float xPosition;
    float tileSize;

    // Materials available when spawning
    // This determines the type of planet (tile)
    Material[] materials;

    public BoardColumn(int rows, float xPosition, float tileSize, PlanetsPool pool, Material[] materials)
    {
        prefabPool = pool;
        this.rows = rows;
        this.materials = materials;

        this.xPosition = xPosition;
        this.tileSize = tileSize;

        planets = new List<GameObject>(rows);
    }

    public void Fill()
    {
        for(int i = 0; i < rows; i++)
            SpawnPlanet(i * tileSize);
    }

    void SpawnPlanet(float y)
    {
        // Get new planet from pool
        GameObject planet = prefabPool.GetPlanet();

        // Initialize planet with material/type
        planet.GetComponent<Planet>().Init(materials[Random.Range(0, materials.Length)]);

        // Assign position
        planet.gameObject.transform.position = new Vector3(xPosition, y, 0);

        // Add it to list
        planets.Add(planet);
    }
}
