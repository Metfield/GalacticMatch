using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skysphere : MonoBehaviour
{
	// Update is called once per frame
	void Update ()
    {
        // Simple rotation
        gameObject.transform.Rotate(0, Time.deltaTime * 2, Time.deltaTime);
	}
}
