using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsaberController : MonoBehaviour
{
    public GameObject Anchor;
    private BladeController mBlade;

    void Start()
    {
        mBlade = GetComponentInChildren<BladeController>();
    }

    public void SetAnchor(GameObject anchor)
    {
        Anchor = anchor;
        mBlade.ActivateBlade(false);
    }

    public void ToggleActivateBlade()
    {
        mBlade.ToggleActivateBlade();
    }

    void FixedUpdate()
    {
        if (Anchor != null)
        {
            transform.position = Anchor.transform.position;
            transform.rotation = Anchor.transform.rotation;
        }
    }
}
