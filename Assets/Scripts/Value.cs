using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.UI;
using TMPro;
using UnityEngine;

public class Value : MonoBehaviour
{
    [SerializeField] private GameObject slider;
    [SerializeField] private bool offsetValue;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<TextMeshPro>().SetText(slider.GetComponent<PinchSlider>().SliderValue.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        if (offsetValue)
        {
            if (slider.tag.Equals("LaplacianOffset") || slider.tag.Equals("KeyOffset"))
            {
                float value = slider.GetComponent<PinchSlider>().SliderValue * 10;
                gameObject.GetComponent<TextMeshPro>().SetText(value.ToString());
            }
            else if (slider.tag.Equals("NormalOffset"))
            {
                int value = (int)Math.Floor(slider.GetComponent<PinchSlider>().SliderValue * 100);
                gameObject.GetComponent<TextMeshPro>().SetText(value.ToString());
            }
        }
        else if (slider.name.Equals("KeySlider"))
        {
            int value = (int)Math.Floor(slider.GetComponent<PinchSlider>().SliderValue * 100);
            gameObject.GetComponent<TextMeshPro>().SetText(value.ToString());
        }
        else
        {
            gameObject.GetComponent<TextMeshPro>().SetText(slider.GetComponent<PinchSlider>().SliderValue.ToString());
        }
    }
}
