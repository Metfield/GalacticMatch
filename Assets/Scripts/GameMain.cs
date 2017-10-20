using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMain : MonoBehaviour
{
    bool gameHasStarted;

    // Use this for initialization
    void Start()
    {
        gameHasStarted = false;
    }

    public void OnGameStart()
    {
        // Gotta protect it like this because apparently the button callback
        // happens twice...
        if (!gameHasStarted)
        {
            gameHasStarted = true;

            // Create board in scene
            Instantiate(Resources.Load<GameObject>("Prefabs/Board"));

            // Create the helper that traces tile matching
            Instantiate(Resources.Load<GameObject>("Prefabs/MatchTracer"));
        }
    }

	// Update is called once per frame
	void Update ()
    {
		
	}
}
