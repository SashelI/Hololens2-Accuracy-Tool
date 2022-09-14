using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Microsoft.MixedReality.Toolkit.Utilities;
using Microsoft;
using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Input;

/// <summary>
/// Class <c>Coordinates</c> gathers the object's coordinates data (Position + Rotation).
/// </summary>
public class Coordinates : MonoBehaviour
{
    private TextMeshPro textMarker;

    [SerializeField] private GameObject slateMarker;
    [SerializeField] private GameObject StartButton;
    [SerializeField] private GameObject EndButton;

    private string posMarker;
    private string posTip;

    private Matrix4x4 originTransformMatrix;

    private CoordCSV forCsv;
    private List<CoordCSV> needleRecords;

    private bool isRecording;
    private double startTime;

    private string folderName;

    public GameObject test; //Text for debugging

    // Start is called before the first frame update
    void Start()
    {
        textMarker = slateMarker.GetComponent<TextMeshPro>();

        posMarker = "Maqueur :\n";

        originTransformMatrix=Matrix4x4.identity;

        isRecording = false;

        folderName = "ObjectCSV Export";
    }

    // Update is called once per frame
    /// <summary>
    /// Checks if another coordinates system was created. If so, takes it as the origin.
    /// Coordinates are calculated using transition Matrix. The Z axis has to be inverted when using a QR code Coordinates System.
    /// Then, displays the position and rotation of the gameObject corresponding to the real object. If recording for csv, creates a new <c>CoordCSV</c> to save this data.
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

        posMarker = "Maqueur :\n";
        posTip = "Pointe :\n";

        if (!(GameObject.FindGameObjectWithTag("Marker") is null))          
        {
            Vector3 relPos;
            Vector3 pose = GameObject.FindGameObjectWithTag("Marker").transform.position;
            Quaternion rot = GameObject.FindGameObjectWithTag("Marker").transform.rotation;
            Vector4 worldPos = new Vector4(pose.x, pose.y, pose.z, 1.0f);
            if (!newOriginExists)
            {
                relPos = pose;
            }
            else
            {
                relPos =  originTransformMatrix * worldPos;
                relPos.z = -relPos.z;
            }
            relPos *= 1000.0f;

            if (isRecording)
            {
                CoordCSV marker = new CoordCSV("Marker", RelativeTime(), relPos.ToString(), rot.ToString());
                forCsv.SaveRecord(needleRecords, marker);
            }

            posMarker = posMarker + "Position : " + relPos + "\n";
            posMarker = posMarker + "Rotation : " + rot + "\n";
        }
        textMarker.SetText(posMarker);
        textMarker.color = Color.yellow;
    }

    /// <summary>
    /// Method <c>BeginMeasures</c> instantiates new <c>CoordCSV</c> and record lists, sets the recording property to True, and saves the time.
    /// It is now saving data until recording is set to False.
    /// </summary>
    public void BeginMeasures()
    {
        forCsv = new CoordCSV();
        needleRecords = new List<CoordCSV>();
        StartButton.SetActive(false);
        EndButton.SetActive(true);
        startTime = Time.realtimeSinceStartupAsDouble;
        isRecording = true;
    }

    /// <summary>
    /// Method <c>SaveMeasures</c> stops recording (false) and writes the data in a .csv file.
    /// </summary>
    public void SaveMeasures()
    {
        forCsv.WriteInCsv(needleRecords,folderName, 2);
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
    /// Writes the data in a .csv file.
    /// </summary>
    void OnApplicationQuit()
    {
        if (isRecording)
        {
            forCsv.WriteInCsv(needleRecords,folderName, 2);
        }
    }

}

