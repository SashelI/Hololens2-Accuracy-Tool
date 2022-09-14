using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;
using UnityEngine;

/// <summary>
/// Class <c>JointsLeft</c> shows only the Index and Thumb joints (and tip) of the left hand.
/// </summary>
public class JointsLeft : MonoBehaviour
{
    public GameObject TipMarker;
    public GameObject JointMarker;

    private GameObject thumbTipObj;
    private GameObject thumbDObj;
    private GameObject thumbMObj;
    private GameObject thumbPObj;
    private GameObject IndexDObj;
    private GameObject IndexMiObj;
    private GameObject IndexKObj;
    private GameObject IndexMObj;

    public MixedRealityPose pose;
    // Start is called before the first frame update
    /// <summary>
    /// A GameObject is instantiated for every joint needed.
    /// </summary>
    void Start()
    {
        thumbTipObj = Instantiate(TipMarker, transform);

        thumbPObj = Instantiate(JointMarker, transform);
        thumbDObj = Instantiate(JointMarker, transform);
        thumbMObj = Instantiate(JointMarker, transform);

        IndexDObj = Instantiate(JointMarker, transform);
        IndexMiObj = Instantiate(JointMarker, transform);
        IndexKObj = Instantiate(JointMarker, transform);
        IndexMObj = Instantiate(JointMarker, transform);
    }

    // Update is called once per frame
    /// <summary>
    /// Each joint GameObject is placed where the HoloLens detects the joints.
    /// </summary>
    void Update()
    {
        thumbTipObj.GetComponent<Renderer>().enabled = false;

        thumbPObj.GetComponent<Renderer>().enabled = false;
        thumbDObj.GetComponent<Renderer>().enabled = false;
        thumbMObj.GetComponent<Renderer>().enabled = false;

        IndexDObj.GetComponent<Renderer>().enabled = false;
        IndexMiObj.GetComponent<Renderer>().enabled = false;
        IndexKObj.GetComponent<Renderer>().enabled = false;
        IndexMObj.GetComponent<Renderer>().enabled = false;

        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.ThumbTip, Handedness.Left, out pose))
        {
            thumbTipObj.GetComponent<Renderer>().enabled = true;
            thumbTipObj.transform.position = pose.Position;
        }

        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.ThumbDistalJoint, Handedness.Left, out pose))
        {
            thumbDObj.GetComponent<Renderer>().enabled = true;
            thumbDObj.transform.position = pose.Position;
        }
        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.ThumbMetacarpalJoint, Handedness.Left, out pose))
        {
            thumbMObj.GetComponent<Renderer>().enabled = true;
            thumbMObj.transform.position = pose.Position;
        }
        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.ThumbProximalJoint, Handedness.Left, out pose))
        {
            thumbPObj.GetComponent<Renderer>().enabled = true;
            thumbPObj.transform.position = pose.Position;
        }

        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.IndexDistalJoint, Handedness.Left, out pose))
        {
            IndexDObj.GetComponent<Renderer>().enabled = true;
            IndexDObj.transform.position = pose.Position;
        }
        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.IndexMiddleJoint, Handedness.Left, out pose))
        {
            IndexMiObj.GetComponent<Renderer>().enabled = true;
            IndexMiObj.transform.position = pose.Position;
        }
        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.IndexKnuckle, Handedness.Left, out pose))
        {
            IndexKObj.GetComponent<Renderer>().enabled = true;
            IndexKObj.transform.position = pose.Position;
        }
        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.IndexMetacarpal, Handedness.Left, out pose))
        {
            IndexMObj.GetComponent<Renderer>().enabled = true;
            IndexMObj.transform.position = pose.Position;
        }
    }
}
