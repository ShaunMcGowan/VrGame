using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowController : MonoBehaviour {

    public Transform controller;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        this.transform.position = controller.position;
	}
}
