using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetsPool : ScriptableObject
{
    List<GameObject> planets;

    // Easier to handle a prefab in case we want to change some properties
    GameObject originalPrefab;

    // Holds all planets in scene
    GameObject sceneRoot;

    public void Init(int initialSize)
    {
        // Create scene root
        sceneRoot = new GameObject("Planets");

        // Get planet prefab
        originalPrefab = Resources.Load<GameObject>("Prefabs/Planet");

        // Initialize list
        planets = new List<GameObject>(initialSize);

        // Create objects and add them to list
        for (int i = 0; i < initialSize; i++)
        {
            // Instantiate
            planets.Add(Instantiate(originalPrefab));

            // Name, disable and add to root
            planets[i].name = "planet" + i;
            planets[i].SetActive(false);
            planets[i].transform.parent = sceneRoot.transform;
        }
    }

    public GameObject GetPlanet()
    {
        foreach (GameObject planet in planets)
        {
            if (!planet.activeSelf)
            {
                planet.SetActive(true);
                return planet;
            }
        }

        // Create new planet and return it
        return Instantiate(originalPrefab);
    }
}
