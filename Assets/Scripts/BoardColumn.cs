﻿using System.Collections;
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

    // Variables used for spawning cooldown
    bool planetIsSpawning;
    float cooldownTimeStamp;

    [SerializeField]
    float spawnCooldownInSecs = 0.45f;

    AudioManager audioManager;

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

        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    public void Fill()
    {
        // Fill column
        for (int i = 0; i < rows; i++)
            SpawnPlanet(i * tileSize, true);

        // Save spawning point for later
        spawnPointY = (rows * tileSize);
    }

    void SpawnPlanet(float y, bool isFirstSpawn = false)
    {
        // Get new planet from pool
        Planet planet = prefabPool.GetPlanet().GetComponent<Planet>();

        // Initialize planet with material/type
        planet.Init(materials[Random.Range(0, materials.Length)], this);

        // Assign position
        planet.gameObject.transform.position = new Vector3(xPosition, y, 0);

        // Add it to list
        planets.Add(planet);

        // Play sound
        if(!isFirstSpawn)
            audioManager.PlaySFX(AudioClips.SFX_PLANET_SPAWN);
    }

    public void ConsumePlanet(Planet planet)
    {
        // Remove planet from list
        planets.Remove(planet);

        // Explosion particles?
        
        
    }

    public void Update()
    {
        // Spawn a planet if it's missing
        if(planets.Count < rows)
        {
            if (cooldownTimeStamp <= Time.time)
                planetIsSpawning = false;

            if (!planetIsSpawning)
            {
                planetIsSpawning = true;
                SpawnPlanet(spawnPointY);
                cooldownTimeStamp = Time.time + spawnCooldownInSecs;
            }
        }
    }
}
