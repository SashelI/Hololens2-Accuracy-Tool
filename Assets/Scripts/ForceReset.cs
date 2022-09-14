using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Visometry.VisionLib.SDK.Core;

/// <summary>
/// Class <c>ForceReset</c> resets the tracking when it is critical for too long (if not, the tracking gets stuck).
/// </summary>
public class ForceReset : MonoBehaviour
{
    [SerializeField] private GameObject ModelTracker;
    private bool wasCritical;
    private double timestamp;
    private double timer;

    void Start()
    {
        wasCritical = false;
        timestamp = 0.0;
        timer = 0.0;
    }

    void Update()
    {
        if (wasCritical)
        {
            timer += Time.timeAsDouble - timestamp;
            if (timer > 20.0)
            {
                ModelTracker.GetComponent<ModelTracker>().ResetTrackingSoft();
                wasCritical = false;
                timestamp = 0.0;
            }
        }
        else
        {
            timer = 0.0;
        }
    }

    public void isCritical()
    {
        if(!wasCritical)
        {
            wasCritical = true;
            timestamp = Time.timeAsDouble;
        }
    }

    public void notCritical()
    {
        wasCritical = false;
        timer = 0.0;
    }
}
