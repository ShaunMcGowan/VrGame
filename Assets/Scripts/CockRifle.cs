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

    // Both these values are used to lock the axis and position for the bolt
    private Vector3 startingAngle;
    private Vector3 startingPos;

    float previousYAngle = 359;

    /// <summary>
    /// The first step of the cock is false when we can pull the bolt back
    /// </summary>
    private bool rotateBolt = true;

    /// <summary>
    /// Pulling the bolt is ready when the bolt is fully rotated
    /// </summary>
    private bool pullBolt = false;

    private void LateUpdate()
    {
        transform.localEulerAngles = new Vector3(startingAngle.x,transform.localEulerAngles.y,startingAngle.z);
        transform.position = new Vector3(startingPos.x, transform.position.y, startingPos.z);
        print("The local y is : " + transform.localEulerAngles.y);
        if (transform.localEulerAngles.y > 360 || transform.localEulerAngles.y < 325)
        {
           transform.localEulerAngles = new Vector3(startingAngle.x, previousYAngle, startingAngle.z);
        }
        previousYAngle = transform.localEulerAngles.y;
        ValidateRotateBolt();
    }

    /// <summary>
    ///  Handles rotating the bolt
    /// </summary>
    /// <param name="collision"></param>
    public void OnTriggerStay(Collider collision)
    {
        if (rotateBolt)
        {
            RotateBolt(collision);
        }
        if (pullBolt)
        {
            PullOutBolt(collision);
        }
    }

    /// <summary>
    ///  Handles rotating the bolt
    /// </summary>
    /// <param name="collision"></param>
    public void OnTriggerEnter(Collider collision)
    {
        if (rotateBolt)
        {
            RotateBolt(collision);
        }
        if (pullBolt)
        {
            PullOutBolt(collision);
        }
    }
    private void Start()
    {
        Vector3 targetPos = new Vector3(transform.position.x,45,transform.position.z);
        startingPos = transform.position;
        startingAngle = transform.localEulerAngles;
    }

    /// <summary>
    /// Checks to see if the bolt has rotated enough to be cocked
    /// </summary>
    private void ValidateRotateBolt()
    {
        if(transform.localEulerAngles.y < 330)
        {
            rotateBolt = false;
        }
        pullBolt = true;
    }
    private void RotateBolt(Collider collision)
    {
        if (collision.transform.parent.parent != null && collision.transform.parent.parent.name.Equals("RightController"))   // if the matching controller is pressing the grip down and colliding we can rotate the bolt
        {
            sniper.GetComponent<VRTK_InteractableObject>().allowedGrabControllers = VRTK_InteractableObject.AllowedController.LeftOnly;
            if (ControllerInputManager.Instance.rightGripPressedDown)
            {
                transform.LookAt(collision.transform.position);
            }
        }
        if (collision.transform.parent.parent.name.Equals("LeftController"))
        {
            sniper.GetComponent<VRTK_InteractableObject>().allowedGrabControllers = VRTK_InteractableObject.AllowedController.RightOnly;
            if (ControllerInputManager.Instance.leftGripPressedDown)
            {         
                this.transform.LookAt(collision.transform.position);
            }
        }
        sniper.GetComponent<VRTK_InteractableObject>().allowedGrabControllers = VRTK_InteractableObject.AllowedController.Both;
    }
    /// <summary>
    /// Handles moving the bolt toward the controller
    /// </summary>
    private void PullOutBolt(Collider collision)
    {
        print("Pulling out");
        if (collision.transform.parent.parent != null && collision.transform.parent.parent.name.Equals("RightController"))   // if the matching controller is pressing the grip down and colliding we can rotate the bolt
        {
            if (ControllerInputManager.Instance.rightGripPressedDown)
            {
                transform.position = Vector3.MoveTowards(transform.position, collision.transform.position, 1);
            }
        }
        if (collision.transform.parent.parent.name.Equals("LeftController"))
        {
            if (ControllerInputManager.Instance.leftGripPressedDown)
            {
                transform.position = Vector3.MoveTowards(transform.position, collision.transform.position, 1);
            }
        };
    }
}
