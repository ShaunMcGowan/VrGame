using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script is responsible for handling the cocking of a rifle after it is fired
/// </summary>
public class CockRifle : MonoBehaviour {


    /// <summary>
    ///  Handles rotating the bolt
    /// </summary>
    /// <param name="collision"></param>
    public void OnTriggerStay(Collider collision)
    {
        RotateBolt(collision);
    }

    /// <summary>
    ///  Handles rotating the bolt
    /// </summary>
    /// <param name="collision"></param>
    public void OnTriggerEnter(Collider collision)
    {
        RotateBolt(collision);
    }

    private void RotateBolt(Collider collision)
    {
        print("The collider is :  " + collision.transform.parent.name);
        if (ControllerInputManager.Instance.GripPressedDown)
        {
            
        }
    }
}
