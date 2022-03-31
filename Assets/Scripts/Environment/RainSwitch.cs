using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class RainSwitch : MonoBehaviour
{
    public VisualEffect rain;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            rain.Stop();
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
            rain.Play();
    }
}
