using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script is responsible for handling the cocking of a rifle after it is fired
/// </summary>
public class CockRifle : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        print("The current eurler angle of the bolt is " + transform.localEulerAngles.ToString());
	}
}
