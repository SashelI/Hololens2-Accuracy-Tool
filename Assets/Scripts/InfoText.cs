using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Visometry.VisionLib.SDK.Core;

public class InfoText : MonoBehaviour
{
    [SerializeField] private TextMeshPro LaplacianText;
    [SerializeField] private TextMeshPro NormalText;
    [SerializeField] private TextMeshPro MinInitText;
    [SerializeField] private TextMeshPro MinTrackText;
    [SerializeField] private TextMeshPro KeyDistText;
    [SerializeField] private TextMeshPro StateText;

    private float laplacianValue;
    private float normalValue;
    private float minInitValue;
    private float minTrackValue;
    private float keyDistValue;

    private string initialLaplacianText;
    private string initialNormalText;
    private string initialMinInitText;
    private string initialMinTrackText;
    private string initialKeyDistText;
    private string initialStateText;
    // Start is called before the first frame update
    void Start()
    {
        laplacianValue = 1.0f;
        normalValue = 0.3f;
        minInitValue = 0.6f;
        minTrackValue = 0.45f;
        keyDistValue = 50;

        initialKeyDistText = KeyDistText.text;
        initialLaplacianText = LaplacianText.text;
        initialMinInitText = MinInitText.text;
        initialMinTrackText = MinTrackText.text;
        initialNormalText = NormalText.text;
        initialStateText = StateText.text;
    }

    // Update is called once per frame
    void Update()
    {
        LaplacianText.SetText(initialLaplacianText + " " + laplacianValue);
        NormalText.SetText(initialNormalText + " " + normalValue);
        MinInitText.SetText(initialMinInitText +" "+ minInitValue);
        MinTrackText.SetText(initialMinTrackText +" "+ minTrackValue);
        KeyDistText.SetText(initialKeyDistText +" "+ keyDistValue);
    }

    public void laplacianChanged(Single data)
    {
        laplacianValue = data;
    }
    public void normalChanged(Single data)
    {
        normalValue = data;
    }
    public void minInitChanged(Single data)
    {
        minInitValue = data;
    }
    public void minTrackingChanged(Single data)
    {
        minTrackValue = data;
    }
    public void keyDistChanged(Single data)
    {
        keyDistValue = data;
    }

    public void TrackedState()
    {
        StateText.SetText(initialStateText + " Tracked");
        StateText.color = Color.green;
    }

    public void CriticalState()
    {
        StateText.SetText(initialStateText + " Critical");
        StateText.color = Color.yellow;
    }

    public void LostState()
    {
        StateText.SetText(initialStateText + " Lost");
        StateText.color = Color.red;
    }
}
