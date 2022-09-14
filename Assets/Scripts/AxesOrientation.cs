using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class <c>AxesOrientation</c> manages the good orientation of the coordinates system's axes. 
/// </summary>
public class AxesOrientation : MonoBehaviour
{
    private GameObject obj;
    private bool changedRotation;
    void Start()
    {
        changedRotation = false;
    }
    
    /// <summary>
    /// Checks if a new Origin was created, and if so, makes sure it's placed perpendicular to the player's head (as the QR code is supposed to be).
    /// </summary>
    void Update()
    {
        if (!(GameObject.FindWithTag("QROrigin") is null))
        {
            obj = GameObject.FindGameObjectWithTag("QROrigin");

            if (!changedRotation)
            {
                obj.transform.rotation = new Quaternion(1.0f, 0.0f, -0.5f, 0.0f);
                float angle = Vector3.Angle(UnityEngine.Camera.main.transform.forward, obj.transform.right);
                angle = 90 - angle;
                obj.transform.rotation *= Quaternion.Euler(0.0f, -angle, 0.0f);
                if (obj.transform.forward.x > 0 && obj.transform.forward.z > 0)
                {
                    obj.transform.rotation *= Quaternion.Euler(0.0f, 180.0f, 0.0f);
                }
                changedRotation = true;
            }
        }
    }

    /// <summary>
    /// Method <c>ResetQrOrigin</c> resets the "changedRotation" bool, as a new Origin is going to be created and will need the rotation.
    /// </summary>
    public void ResetQrOrigin()
    {
        changedRotation = false;
    }
}
