using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardColumn
{
    // Used to fetch available planet prefabs
    PlanetsPool prefabPool;

    // Holds planets in column
    List<Planet> planets;

    // Rows in column
    int rows;

    float spawnPointY;

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

        planets = new List<Planet>(rows);
    }

    public void Fill()
    {
        // Fill column
        for (int i = 0; i < rows; i++)
            SpawnPlanet(i * tileSize);

        // Save spawning point for later
        spawnPointY = (rows * tileSize);
    }

    void SpawnPlanet(float y)
    {
        // Get new planet from pool
        Planet planet = prefabPool.GetPlanet().GetComponent<Planet>();

        // Initialize planet with material/type
        planet.Init(materials[Random.Range(0, materials.Length)], this);

        // Assign position
        planet.gameObject.transform.position = new Vector3(xPosition, y, 0);

        // Add it to list
        planets.Add(planet);
    }

    public void ConsumePlanet(Planet planet)
    {
        // Remove planet from list
        planets.Remove(planet);

        // Add new one
        // TODO: Implement spawn cooldown!
        SpawnPlanet(spawnPointY);
    }
}
