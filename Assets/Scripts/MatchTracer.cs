using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchTracer : MonoBehaviour
{
    // Holds traced planets
    List<Planet> tracedPlanets;

    RaycastHit hit;
    Planet traceRoot;
    float maxTraceDistance;
    Planet tempPlanet;

    private void Start()
    {
        // Create objects
        tracedPlanets = new List<Planet>();
        hit = new RaycastHit();
    }

    void StartTrace()
    {
        // Raycast to scene and try to fetch a planet
        tempPlanet = PlanetRayCast();

        if (tempPlanet != null)
        {
            // Add planet to trace
            traceRoot = tempPlanet;
            tracedPlanets.Add(traceRoot);

            // Get max trace distance while we're at it
            // This won't change at runtime, but it's convenient to access it this way
            maxTraceDistance = traceRoot.GetComponent<SphereCollider>().bounds.size.x;
        }        
    }

    void UpdateTrace()
    {
        // Raycast to scene and try to fetch a planet
        tempPlanet = PlanetRayCast();

        // We hit something!
        if (tempPlanet != null)
        {
            // We don't care about the same planet
            if (tracedPlanets.Contains(tempPlanet))
                return;

            // We only care for neighboring planets
            if (Vector3.Distance(tracedPlanets[tracedPlanets.Count - 1].transform.position, tempPlanet.transform.position) > (maxTraceDistance * 1.5f)) // Arbitrary 1.2 to add off margin
                return;

            // Only add to trace if planet is of the same kind
            if (traceRoot.GetPlanetType() != tempPlanet.GetPlanetType())
                return;

            // Add planet to trace
            tracedPlanets.Add(tempPlanet.gameObject.GetComponent<Planet>());
        }
    }

    void EndTrace()
    {
        // See if we got a match 
        if(tracedPlanets.Count >= 3)
        {
            // Hurray!!
            PlanetsMatched();
        }
        
        // Clear fields
        tracedPlanets.RemoveRange(0, tracedPlanets.Count);
        traceRoot = null;
        tempPlanet = null;
    }

    Planet PlanetRayCast()
    {
        // Trace ray from screen to world
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Check for occurrence
        if (Physics.Raycast(ray, out hit))
        {
            // We hit something! Let's make sure it's a planet
            if (hit.collider.tag == "Planet")
            {
                return hit.collider.gameObject.GetComponent<Planet>();
            }
        }

        return null;
    }

    void PlanetsMatched()
    {
        // Notify every planet in match that they've been consumed
        foreach (Planet planet in tracedPlanets)
        {
            planet.Consume();
        }
    }

	// Update is called once per frame
	void Update ()
    {
        // Check for initial tap
        if (Input.GetMouseButtonDown(0))
        {
            StartTrace();
        }
        // Check if user is holding tap
        else if(Input.GetMouseButton(0) && (tracedPlanets.Count > 0))
        {
            UpdateTrace();
        }
        // Check for tap lift
        else if(Input.GetMouseButtonUp(0))
        {
            EndTrace();
        }
    }
}
