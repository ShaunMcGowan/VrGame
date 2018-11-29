using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using VRTK.GrabAttachMechanics;
using VRTK.SecondaryControllerGrabActions;

/// <summary>
/// Script is responsible for handling the cocking of a rifle after it is fired
/// </summary>
public class CockRifle : MonoBehaviour {

    /// <summary>
    /// Used to modify vrtk 
    /// </summary>
    public GameObject sniper;


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
        if (collision.transform.parent.parent.name.Equals("RightController"))   // if the matching controller is pressing the grip down and colliding we can rotate the bolt
        {
            sniper.GetComponent<VRTK_InteractableObject>().allowedGrabControllers = VRTK_InteractableObject.AllowedController.LeftOnly;
            if (ControllerInputManager.Instance.rightGripPressedDown)
            {
                print("Right grip pressed true");
            }
        }
        if (collision.transform.parent.parent.name.Equals("LeftController"))
        {
            sniper.GetComponent<VRTK_InteractableObject>().allowedGrabControllers = VRTK_InteractableObject.AllowedController.RightOnly;
            if (ControllerInputManager.Instance.leftGripPressedDown)
            {
                print("Left grip grab is true now also");
            }
        }
        sniper.GetComponent<VRTK_InteractableObject>().allowedGrabControllers = VRTK_InteractableObject.AllowedController.Both;
    }
}
