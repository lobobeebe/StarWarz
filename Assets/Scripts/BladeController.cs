using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeController : MonoBehaviour
{
    private bool mIsOn;


    public void ActivateBlade(bool doActivate)
    {
        mIsOn = doActivate;

        // Turn on or off the blade
        GetComponent<Renderer>().enabled = mIsOn;
    }

    // Use this for initialization
    void Start ()
    {

    }

    public void ToggleActivateBlade()
    {
        ActivateBlade(!mIsOn);
    }

    // Update is called once per frame
    void Update ()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (mIsOn && other.tag == "Projectile")
        {
            LaserBoltController laserBolt = other.gameObject.GetComponentInParent<LaserBoltController>();
            laserBolt.Reflect();
        }
    }
}
