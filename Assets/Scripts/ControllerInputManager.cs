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

	void LateUpdate ()
    {
        if (PositionManager.Instance.rightController && !isSet)
        {
            isSet = true;
            PositionManager.Instance.rightController.GetComponent<VRTK_ControllerEvents>().TouchpadAxisChanged += new ControllerInteractionEventHandler(AxisChanged);
            PositionManager.Instance.rightController.GetComponent<VRTK_ControllerEvents>().TouchpadTouchEnd += new ControllerInteractionEventHandler(TouchpadReleased);

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
        if (goodToRotate)
        {
            if (e.touchpadAxis.x >= 0.9f)
            {
                // Rotate Right 90
                GameObject destination = new GameObject();
                destination.transform.position = PositionManager.Instance.player.position;
                destination.transform.rotation = PositionManager.Instance.player.rotation;
                destination.transform.localEulerAngles = new Vector3(destination.transform.localEulerAngles.x, destination.transform.localEulerAngles.y + 90.0f, destination.transform.localEulerAngles.z);
                PositionManager.Instance.teleportPlayer(PositionManager.Instance.player, destination.transform);
                print("The touchpad axis is :  " + e.touchpadAxis.ToString());
                GameObject.Destroy(destination);
                goodToRotate = false;

            }
            else if (e.touchpadAxis.x <= -0.9f)
            {
                // Rotate Left 90
                GameObject destination = new GameObject();
                destination.transform.position = PositionManager.Instance.player.position;
                destination.transform.rotation = PositionManager.Instance.player.rotation;
                destination.transform.localEulerAngles = new Vector3(destination.transform.localEulerAngles.x, destination.transform.localEulerAngles.y - 90.0f, destination.transform.localEulerAngles.z);
                PositionManager.Instance.teleportPlayer(PositionManager.Instance.player, destination.transform);
                GameObject.Destroy(destination);
                goodToRotate = false;
            }
        }
    }

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
