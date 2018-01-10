using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsaberController : MonoBehaviour
{
    public GameObject Blade;

    private bool mIsOn;

    void OnEnabled()
    {
        SetIsOn(false);
    }

    public bool GetIsOn()
    {
        return mIsOn;
    }

    public void SetIsOn(bool isOn)
    {
        mIsOn = isOn;

        // Turn on or off the renderer
        Blade.GetComponent<Renderer>().enabled = isOn;
    }
}
