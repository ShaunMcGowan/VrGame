using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

/// <summary>
/// Handles controller the position of player
/// </summary>
public class PositionManager : MonoBehaviour {

    #region PublicVars
    [HideInInspector]
    public Transform player;
    [HideInInspector]
    public Transform playerRotator;
    [HideInInspector]
    public Transform rightController;
    [HideInInspector]
    public Transform leftController;
    [HideInInspector]
    public GameObject playAreaScripts;
    #endregion


    private void LateUpdate()
    {
        if (player == null)
        {
            player = VRTK_DeviceFinder.DeviceTransform(VRTK_DeviceFinder.Devices.Headset);
            leftController = VRTK_DeviceFinder.DeviceTransform(VRTK_DeviceFinder.Devices.LeftController);
            rightController = VRTK_DeviceFinder.DeviceTransform(VRTK_DeviceFinder.Devices.RightController);
            playAreaScripts = GameObject.Find("PlayArea");
            playerRotator = GameObject.Find("Rotator").transform;
        }
    }
    /// <summary>
    /// Deals with teleporting the player correctly based of the camera pos not the player area pos
    /// </summary>
    /// <param name="player">The transform of the player object</param>
    /// <param name="destination">The transform of the destination for the player</param>
    public void teleportPlayer(Transform player, Transform destination)
    {

        Transform playArea = VRTK_DeviceFinder.PlayAreaTransform();
        Transform oldParent = playArea.parent;
        GameObject uselessEmpty = new GameObject("UselessEmpty"); //Used to rotate the player

        playArea.parent = uselessEmpty.transform; // Parenting the play area so it can be rotated

        // Rotating the player to the correct pos
        StartCoroutine(fadeToBlack());
        uselessEmpty.transform.position = destination.position;
        uselessEmpty.transform.localEulerAngles = new Vector3(playArea.localEulerAngles.x, destination.localEulerAngles.y, playArea.localEulerAngles.z);

        // removing the useless empty as a parent and reatching to the old one
        playArea.parent = oldParent;

        // Calculating the offset of the player from the destination position
        Vector3 offset = player.position - destination.position;
        playArea.position = playArea.position - offset;

        // Removing the empty we created for the player
        GameObject.Destroy(uselessEmpty);

    }


   
    private IEnumerator fadeToBlack()
    {
        float fadeTime = 0.3f;
        playAreaScripts.GetComponent<VRTK_HeadsetFade>().Fade(Color.black,fadeTime);
        yield return new WaitForSeconds(fadeTime);

        playAreaScripts.GetComponent<VRTK_HeadsetFade>().Unfade(fadeTime);

        yield return null;
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
