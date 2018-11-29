using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

/// <summary>
/// Handles all the events that are triggered by controller input
/// </summary>
public class ControllerInputManager : MonoBehaviour {

    private bool isSet = false;
    private bool goodToRotate = true;
    private bool goodToRotateRight = true;
    private bool goodToRotateLeft = true;

    public bool rightGripPressedDown = false; // Set to to true when the grip is pressed
    public bool leftGripPressedDown = false; // Set to to true when the grip is pressed


    void LateUpdate ()
    {
        if (PositionManager.Instance.rightController && !isSet)
        {
            isSet = true;
            PositionManager.Instance.rightController.GetComponent<VRTK_ControllerEvents>().TouchpadAxisChanged += new ControllerInteractionEventHandler(AxisChanged);
            PositionManager.Instance.rightController.GetComponent<VRTK_ControllerEvents>().TouchpadTouchEnd += new ControllerInteractionEventHandler(TouchpadReleased);

            PositionManager.Instance.rightController.GetComponent<VRTK_ControllerEvents>().GripPressed += new ControllerInteractionEventHandler(RightGripPressed);
            PositionManager.Instance.leftController.GetComponent<VRTK_ControllerEvents>().GripPressed += new ControllerInteractionEventHandler(LeftGripPressed);

            PositionManager.Instance.rightController.GetComponent<VRTK_ControllerEvents>().GripReleased += new ControllerInteractionEventHandler(RightGripReleased);
            PositionManager.Instance.leftController.GetComponent<VRTK_ControllerEvents>().GripReleased += new ControllerInteractionEventHandler(LeftGripReleased);
        }

    }

    /// <summary>
    /// Lets the player rotate again once the let go of the touchpad
    /// </summary>
    private void TouchpadReleased(object sender, ControllerInteractionEventArgs e)
    {
        goodToRotate = true;
    }


    private void AxisChanged(object sender, ControllerInteractionEventArgs e)
    {
        rotatePlayer(e);
        checkIfGoodToRotate(e);
    }
    private void rotatePlayer(ControllerInteractionEventArgs e)
    {
        if (goodToRotate)
        {
            if (e.touchpadAxis.x >= 0.9f && goodToRotateRight)
            {
                // Rotate Right 90
                GameObject destination = new GameObject();
                destination.transform.position = PositionManager.Instance.player.position;
                destination.transform.Rotate(0, 90, 0);
                PositionManager.Instance.teleportPlayer(PositionManager.Instance.player, destination.transform);            
                GameObject.Destroy(destination);
                goodToRotate = false;
                goodToRotateRight = false;

            }
            else if (e.touchpadAxis.x <= -0.9f && goodToRotateLeft)
            {
                // Rotate Left 90
                GameObject destination = new GameObject();
                destination.transform.position = PositionManager.Instance.player.position;
                destination.transform.Rotate(0, -90, 0);
                PositionManager.Instance.teleportPlayer(PositionManager.Instance.player, destination.transform);
                GameObject.Destroy(destination);
                goodToRotate = false;
                goodToRotateLeft = false;
            }
        }
    }
    private void checkIfGoodToRotate(ControllerInteractionEventArgs e)
    {
        if(e.touchpadAxis.x < .1 ) // If we return to the origin of the joy stick the user is now alloud to rotate again
        {
            goodToRotate = true;
            goodToRotateRight = true;
        }
        if (e.touchpadAxis.x > -.1) // If we return to the origin of the joy stick the user is now alloud to rotate again
        {
            goodToRotate = true;
            goodToRotateLeft = true;

        }
    }


    #region GripFunction
    /// <summary>
    /// Sets the grip pressed bool value to true
    /// </summary>
    private void RightGripPressed(object sender, ControllerInteractionEventArgs e)
    {
        rightGripPressedDown = true;
    }

    private void RightGripReleased(object sender, ControllerInteractionEventArgs e)
    {
        rightGripPressedDown = false;
    }

    /// <summary>
    /// Sets the grip pressed bool value to true
    /// </summary>
    private void LeftGripPressed(object sender, ControllerInteractionEventArgs e)
    {
        leftGripPressedDown = true;
    }

    private void LeftGripReleased(object sender, ControllerInteractionEventArgs e)
    {
        leftGripPressedDown = false;
    }
    #endregion
    #region singleton
    public static ControllerInputManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }
    #endregion
}
