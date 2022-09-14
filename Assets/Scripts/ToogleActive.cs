using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToogleActive : MonoBehaviour
{
    [SerializeField] private GameObject marker;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Toogle()
    {
        marker.SetActive(!marker.activeInHierarchy);
    }
}
