using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Microsoft.MixedReality.Toolkit.Utilities;
using Microsoft;
using Microsoft.MixedReality.Toolkit.Input;

/// <summary>
/// Class <c>Coordinates</c> gathers Thumb and Index coordinates data (Position + Rotation) on both hands.
/// </summary>
public class Coordinates : MonoBehaviour
{
    public MixedRealityPose pose;
    private TextMeshPro textIndex;
    private TextMeshPro textThumb;

    [SerializeField] private GameObject slateIndex;
    [SerializeField] private GameObject slateThumb;
    [SerializeField] private GameObject StartButton;
    [SerializeField] private GameObject EndButton;

    private string posIndex;
    private string posThumb;

    private Matrix4x4 originTransformMatrix;

    private CoordCSV forCsv;
    private List<CoordCSV> leftHandrecords;
    private List<CoordCSV> rightHandrecords;

    private bool isRecording;
    private double startTime;

    private string folderName;

    public GameObject test; //Text for debugging

    // Start is called before the first frame update
    void Start()
    {
        textIndex = slateIndex.GetComponent<TextMeshPro>();
        textThumb = slateThumb.GetComponent<TextMeshPro>();

        posIndex = "Index :\n";
        posThumb = "Pouce :\n";

        originTransformMatrix=Matrix4x4.identity;

        isRecording = false;

        folderName = "CSV Export";
    }

    // Update is called once per frame
    /// <summary>
    /// Checks if another coordinates system was created. If so, takes it as the origin.
    /// Coordinates are calculated using transition Matrix. The Z axis has to be inverted when using a QR code Coordinates System.
    /// Then, for each active finger (Thumb or Index) of each hand, displays their position and rotation. If recording for csv, creates a new <c>CoordCSV</c> to save this data.
    /// </summary>
    void Update()
    {
        bool newOriginExists = !(GameObject.FindWithTag("QROrigin") is null);
        if (newOriginExists)
        {
            Transform origintest = GameObject.FindWithTag("QROrigin").transform;
            originTransformMatrix = GameObject.FindWithTag("QROrigin").transform.worldToLocalMatrix;
            test.GetComponent<TextMeshPro>().SetText("F: " + origintest.forward + "\n" + "Up: " + origintest.up + "\n" + "R: " + origintest.right + "\n" + origintest.position);
        }

        posIndex = "Index :\n";
        posThumb = "Pouce :\n";

        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.IndexTip, Handedness.Right, out pose))          //Index tip, right hand
        {
            Vector3 relPos;
            Vector4 worldPos = new Vector4(pose.Position.x, pose.Position.y, pose.Position.z, 1.0f);
            if (!newOriginExists)
            {
                relPos = pose.Position;
            }
            else
            {
                relPos =  originTransformMatrix * worldPos;
                relPos.z = -relPos.z;
            }
            relPos *= 1000.0f; //Converting the data from meter to millimeter

            if (isRecording)
            {
                CoordCSV rightIndex = new CoordCSV("Index", RelativeTime(), relPos.ToString(), pose.Rotation.ToString());
                forCsv.SaveRecord(rightHandrecords, rightIndex);
            }

            posIndex += "Main droite\n";
            posIndex = posIndex + "Position : " + relPos + "\n";
            posIndex = posIndex + "Rotation : " + pose.Rotation + "\n";
        }
        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.IndexTip, Handedness.Left, out pose))          //Index tip, left hand
        {
            Vector3 relPos;
            Vector4 worldPos = new Vector4(pose.Position.x, pose.Position.y, pose.Position.z, 1.0f);
            if (!newOriginExists)
            {
                relPos = pose.Position;
            }
            else
            {
                relPos = originTransformMatrix * worldPos;
                relPos.z = -relPos.z;
            }
            relPos *= 1000.0f;

            if (isRecording)
            {
                CoordCSV leftIndex = new CoordCSV("Index", RelativeTime(), relPos.ToString(), pose.Rotation.ToString());
                forCsv.SaveRecord(leftHandrecords, leftIndex);
            }

            posIndex += "\nMain gauche\n";
            posIndex = posIndex + "Position : " + relPos + "\n";
            posIndex = posIndex + "Rotation : " + pose.Rotation + "\n";
        }
        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.ThumbTip, Handedness.Right, out pose))          //Thumb tip, right hand
        {
            Vector3 relPos;
            Vector4 worldPos = new Vector4(pose.Position.x, pose.Position.y, pose.Position.z, 1.0f);
            if (!newOriginExists)
            {
                relPos = pose.Position;
            }
            else
            {
                relPos = originTransformMatrix * worldPos;
                relPos.z = -relPos.z;
            }
            relPos *= 1000.0f;

            if (isRecording)
            {
                CoordCSV rightThumb = new CoordCSV("Thumb", RelativeTime(), relPos.ToString(), pose.Rotation.ToString());
                forCsv.SaveRecord(rightHandrecords, rightThumb);
            }

            posThumb += "Main droite\n";
            posThumb = posThumb + "Position : " + relPos + "\n";
            posThumb = posThumb + "Rotation : " + pose.Rotation + "\n";
        }
        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.ThumbTip, Handedness.Left, out pose))          //Thumb tip, left hand
        {
            Vector3 relPos;
            Vector4 worldPos = new Vector4(pose.Position.x, pose.Position.y, pose.Position.z, 1.0f);
            if (!newOriginExists)
            {
                relPos = pose.Position;
            }
            else
            {
                relPos = originTransformMatrix * worldPos;
                relPos.z = -relPos.z;
            }
            relPos *= 1000.0f;

            if (isRecording)
            {
                CoordCSV leftThumb = new CoordCSV("Thumb", RelativeTime(), relPos.ToString(), pose.Rotation.ToString());
                forCsv.SaveRecord(leftHandrecords, leftThumb);
            }

            posThumb += "\nMain gauche\n";
            posThumb = posThumb + "Position : " + relPos + "\n";
            posThumb = posThumb + "Rotation : " + pose.Rotation + "\n";
        }
        textIndex.SetText(posIndex);
        textThumb.SetText(posThumb);
        textThumb.color = Color.yellow;
    }

    /// <summary>
    /// Method <c>BeginMeasures</c> instantiates new <c>CoordCSV</c> and record lists, sets the recording property to True, and saves the time.
    /// It is now saving data until recording is set to False.
    /// </summary>
    public void BeginMeasures()
    {
        forCsv = new CoordCSV();
        leftHandrecords = new List<CoordCSV>();
        rightHandrecords = new List<CoordCSV>();
        StartButton.SetActive(false);
        EndButton.SetActive(true);
        startTime = Time.realtimeSinceStartupAsDouble;
        isRecording = true;
    }

    /// <summary>
    /// Method <c>SaveMeasures</c> stops recording (false) and writes the data in one .csv file per hand.
    /// </summary>
    public void SaveMeasures()
    {
        forCsv.WriteInCsv(leftHandrecords,folderName, 1);
        forCsv.WriteInCsv(rightHandrecords, folderName);
        StartButton.SetActive(true);
        EndButton.SetActive(false);
        isRecording = false;
    }

    /// <summary>
    /// Method <c>RelativeTime</c> calculates the seconds since the beginning of the current recording session, for timestamp.
    /// </summary>
    /// <returns>Returns time in seconds</returns>
    private double RelativeTime()
    {
        return (Time.realtimeSinceStartupAsDouble - startTime);
    }

    /// <summary>
    /// Writes the data in one .csv file per hand.
    /// </summary>
    void OnApplicationQuit()
    {
        if (isRecording)
        {
            forCsv.WriteInCsv(leftHandrecords,folderName, 1);
            forCsv.WriteInCsv(rightHandrecords, folderName);
        }
    }

}

