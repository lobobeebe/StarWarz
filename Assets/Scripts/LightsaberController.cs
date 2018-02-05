using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsaberController : MonoBehaviour
{
    private BladeController mBlade;
    private GameObject mAnchor;

    private bool mIsHeld;

    void Start()
    {
        mBlade = GetComponentInChildren<BladeController>();
    }

    public void SetAnchor(GameObject anchor)
    {
        mAnchor = anchor;
        mBlade.ActivateBlade(false);
    }

    public void ToggleActivateBlade()
    {
        mBlade.ToggleActivateBlade();
    }

    void FixedUpdate()
    {
        if (mAnchor != null)
        {
            transform.position = mAnchor.transform.position;
            transform.rotation = mAnchor.transform.rotation;
        }
    }
}
