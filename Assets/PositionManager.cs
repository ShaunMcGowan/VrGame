using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

/// <summary>
/// Handles controller the position of player
/// </summary>
public class PositionManager : MonoBehaviour {

    [HideInInspector]
    public Transform player;

    [HideInInspector]
    public Transform rightController;
    [HideInInspector]
    public Transform leftController;
    void Start ()
    {
        player = VRTK_DeviceFinder.DeviceTransform(VRTK_DeviceFinder.Devices.Headset);
        leftController = VRTK_DeviceFinder.DeviceTransform(VRTK_DeviceFinder.Devices.LeftController);
        rightController = VRTK_DeviceFinder.DeviceTransform(VRTK_DeviceFinder.Devices.RightController);
    }
	
    /// <summary>
    /// Deals with teleporting the player correctly based of the camera pos not the player area pos
    /// </summary>
    /// <param name="player">The transform of the player object</param>
    /// <param name="destination">The transform of the destination for the player</param>
   public void teleportPlayer(Transform player, Transform destination)
   {

   }

    #region singleton
    public static PositionManager Instance;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }
    private void OnDestroy()
    {
        if(Instance == this)
        {
            Instance = null;
        }
    }
    #endregion
}
