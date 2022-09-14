using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit;
using UnityEngine;
using TMPro;
using Microsoft.MixedReality.Toolkit.Utilities;
using Microsoft.MixedReality.Toolkit.Input;

/// <summary>
/// Class <c>CreateRepere</c> allows to create a new coordinate system, to use as origin for measurements.
/// This was made before using the QR Code method, but can still be tested in the app
/// </summary>
public class CreateRepere : MonoBehaviour
{
    [SerializeField] private GameObject axes;
    [SerializeField] private TextMeshPro text;

    private GameObject origin;
    private Vector3 pos;
    private Vector3 forwardPos;
    private MixedRealityPose pose;
    // Start is called before the first frame update
    void Start()
    {
        pos = Vector3.zero;
        forwardPos = Vector3.zero;
    }

    /// <summary>
    /// Method <c>GetOriginCoord</c> saves the new system's origin at the index tip's position, after a countdown.
    /// </summary>
    /// <returns></returns>
    IEnumerator GetOriginCoord()
    {
        for (int i = 3; i > 0; i--)
        {
            text.SetText(i.ToString());
            yield return new WaitForSeconds(1);
        }
        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.IndexTip, Handedness.Right, out pose))
        {
            pos = pose.Position;
            forwardPos = pose.Forward;
        }
        text.SetText("Done");
        yield return new WaitForSeconds(1);
        text.SetText("");

        if (origin.IsNull())
        {
            origin = Instantiate(axes);
            origin.tag = "Origin";
        }

        origin.transform.position = pos;
        origin.transform.forward = forwardPos;
    }

    /// <summary>
    /// Calls the <c>GetOriginCoord</c> coroutine.
    /// </summary>
    public void InstantiateCoordinateSystem()
    {
        StartCoroutine(GetOriginCoord());
    }
}
