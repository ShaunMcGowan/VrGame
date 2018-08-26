using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Spawns the player at the position of the object script is attatched to
/// </summary>
public class PlayerSpawner : MonoBehaviour {

	
	void Update ()
    {
		if(PositionManager.Instance.player != null)
        {
            PositionManager.Instance.teleportPlayer(PositionManager.Instance.player, this.transform);
            Destroy(this.gameObject);
        }
	}
	
	
}
