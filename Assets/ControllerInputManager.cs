using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles all the events that are triggered by controller input
/// </summary>
public class ControllerInputManager : MonoBehaviour {

	
	void Start ()
    {
		
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
