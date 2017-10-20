using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    public enum PlanetType
    {
        VENUS,
        EARTH,
        MOON,
        MARS,
        JUPITER
    }

    PlanetType type;
    BoardColumn hostColumn;

    float rotationSpeed;

    public void Init(Material material, BoardColumn boardColumn)
    {
        // Assign material
        GetMaterial(material);

        // Assign host column
        hostColumn = boardColumn;

        // Init rotation speed
        rotationSpeed = 20;
    }

    // Planet is being touched/released
    public void SetHighlight(bool highlightOn)
    {
        // Alter shader
        gameObject.GetComponent<Renderer>().material.SetFloat("_Metallic", highlightOn ? 1.0f : 0.0f);
    }

    // Explode the planet!
    public void Consume()
    {
        gameObject.SetActive(false);
        hostColumn.ConsumePlanet(this);
        SetHighlight(false);
    }

    public PlanetType GetPlanetType()
    {
        return type;
    }

    void GetMaterial(Material material)
    {
        // Assign material
        gameObject.GetComponent<Renderer>().material = material;

        string planetName = material.ToString();
        planetName = planetName.Substring(0, material.ToString().IndexOf(' '));

        // Assign type
        switch (planetName)
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
        // Rotate planet
        gameObject.transform.Rotate(0, -Time.deltaTime * rotationSpeed, 0);
    }
}
