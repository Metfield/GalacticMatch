using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet
{
    enum PlanetType
    {
        VENUS,
        EARTH,
        MOON,
        MARS,
        JUPITER
    }

    public GameObject gameObject;
    PlanetType type;

    public Planet(GameObject gameObject, Material material)
    {
        // Copy gameobject
        this.gameObject = gameObject;

        // Assign material
        GetMaterial(material);        
    }

    void GetMaterial(Material material)
    {
        // Assign material
        gameObject.GetComponent<Renderer>().material = material;

        // Assign type
        switch (material.ToString())
        {
            case "Venus":
                type = PlanetType.VENUS;
                break;

            case "Earth":
                type = PlanetType.EARTH;
                break;

            case "Moon":
                type = PlanetType.MOON;
                break;

            case "Mars":
                type = PlanetType.MARS;
                break;

            case "Jupiter":
                type = PlanetType.JUPITER;
                break;
        }
    }

    void Update()
    {
        Debug.Log("NO WAY YO!");
        gameObject.transform.Rotate(0, Time.deltaTime * 20, 0);
    }
}
