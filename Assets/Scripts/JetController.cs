using MRTKExtensions.QRCodes;
using UnityEngine;

/// <summary>
/// Class <c>JetController</c> Follows when a QR Code is detected, and activates the gameObject corresponding to the coordinate system visualizer.
/// </summary>
public class JetController : MonoBehaviour
{
    [SerializeField]
    private QRTrackerController trackerController;

    private void Start()
    {
        trackerController.PositionSet += PoseFound;
    }

    private void PoseFound(object sender, Pose pose)
    {
        var childObj = transform.GetChild(0);
        childObj.SetPositionAndRotation(pose.position, pose.rotation);
        childObj.gameObject.SetActive(true);
    }
}