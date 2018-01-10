using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{
    public GameObject HiltClip;
    public LightsaberController LightSaber;

    private bool mIsCollidingHiltClip;
    private bool mIsHoldingLightSaber;
    private SteamVR_TrackedController mController;

    private void OnEnable()
    {
        // Get the Steam Controller and add event handlers
        mController = GetComponent<SteamVR_TrackedController>();
        mController.Gripped += HandleGripped;
        mController.MenuButtonClicked += HandleMenuClicked;
    }

    private void HandleGripped(object sender, ClickedEventArgs e)
    {
        if (mIsCollidingHiltClip)
        {
            if (mIsHoldingLightSaber)
            {
                LightSaber.SetIsOn(false);
                LightSaber.transform.parent = HiltClip.transform;
                
                mIsHoldingLightSaber = false;
            }
            else
            {
                LightSaber.transform.parent = transform;
                mIsHoldingLightSaber = true;
            }
        }
    }

    private void HandleMenuClicked(object sender, ClickedEventArgs e)
    {
        if (mIsHoldingLightSaber)
        {
            if (LightSaber.GetIsOn())
            {
                LightSaber.SetIsOn(false);
            }
            else
            {
                LightSaber.SetIsOn(true);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other == HiltClip)
        {
            mIsCollidingHiltClip = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other == HiltClip)
        {
            mIsCollidingHiltClip = false;
        }
    }
}
