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
    private void Start()
    {
        Vector3 targetPos = new Vector3(transform.position.x,45,transform.position.z);
      //  transform.LookAt(targetPos);
    }
    private void RotateBolt(Collider collision)
    {
        if (collision.transform.parent.parent.name.Equals("RightController"))   // if the matching controller is pressing the grip down and colliding we can rotate the bolt
        {
            sniper.GetComponent<VRTK_InteractableObject>().allowedGrabControllers = VRTK_InteractableObject.AllowedController.LeftOnly;
            if (ControllerInputManager.Instance.rightGripPressedDown)
            {
                Vector3 targetPos = new Vector3(transform.position.x, transform.position.y, collision.transform.position.z);
                transform.LookAt(collision.transform.position);
            }
        }
        if (collision.transform.parent.parent.name.Equals("LeftController"))
        {
            sniper.GetComponent<VRTK_InteractableObject>().allowedGrabControllers = VRTK_InteractableObject.AllowedController.RightOnly;
            if (ControllerInputManager.Instance.leftGripPressedDown)
            {
                Vector3 targetPos = new Vector3(transform.position.x, transform.position.y, collision.transform.position.z);
                this.transform.LookAt(collision.transform.position);
            }
        }
        sniper.GetComponent<VRTK_InteractableObject>().allowedGrabControllers = VRTK_InteractableObject.AllowedController.Both;
    }
}
