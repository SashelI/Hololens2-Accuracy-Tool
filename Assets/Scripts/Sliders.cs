using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.UI;
using TMPro;
using UnityEngine;
using Visometry.VisionLib.SDK.Core;

public class Sliders : MonoBehaviour
{
    [SerializeField] private GameObject menu;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LaplacianUpdate(SliderEventData data)
    {
        GameObject offsetSlider = GameObject.FindGameObjectWithTag("LaplacianOffset");
        if (data.NewValue + (offsetSlider.GetComponent<PinchSlider>().SliderValue * 10) != 0)
        {
            GameObject.FindGameObjectWithTag("Laplacian").GetComponent<RuntimeParameter>().SetValue(data.NewValue + (offsetSlider.GetComponent<PinchSlider>().SliderValue * 10));
        }
    }

    public void NormalUpdate(SliderEventData data)
    {
        GameObject offsetSlider = GameObject.FindGameObjectWithTag("NormalOffset");
        if (data.NewValue + offsetSlider.GetComponent<PinchSlider>().SliderValue * 100 != 0)
        {
            GameObject.FindGameObjectWithTag("Normal").GetComponent<RuntimeParameter>().SetValue(data.NewValue + offsetSlider.GetComponent<PinchSlider>().SliderValue * 100);
        }
    }

    public void MinInitUpdate(SliderEventData data)
    {
        if (data.NewValue != 0.0f)
        {
            GameObject.FindGameObjectWithTag("MinInitQuality").GetComponent<RuntimeParameter>().SetValue(data.NewValue);
        }
    }

    public void MinTrackUpdate(SliderEventData data)
    {
        if (data.NewValue != 0.0f)
        {
            GameObject.FindGameObjectWithTag("MinTrackingQuality").GetComponent<RuntimeParameter>().SetValue(data.NewValue);
        }
    }

    public void KeyUpdate(SliderEventData data)
    {
        GameObject offsetSlider = GameObject.FindGameObjectWithTag("KeyOffset");
        if (data.NewValue * 100 - offsetSlider.GetComponent<PinchSlider>().SliderValue * 10 != 0)
        {
            GameObject.FindGameObjectWithTag("KeyFrame").GetComponent<RuntimeParameter>().SetValue(data.NewValue * 100 - offsetSlider.GetComponent<PinchSlider>().SliderValue * 10);
        }
    }

    public void lockPos()
    {
        if (menu.GetComponent<NearInteractionGrabbable>().enabled == true)
        {
            menu.GetComponent<NearInteractionGrabbable>().enabled = false;
            menu.GetComponent<ObjectManipulator>().enabled = false;
            menu.GetComponent<BoxCollider>().enabled = false;
        }
        else
        {
            menu.GetComponent<NearInteractionGrabbable>().enabled = true;
            menu.GetComponent<ObjectManipulator>().enabled = true;
            menu.GetComponent<BoxCollider>().enabled = true;
        }
    }
}
