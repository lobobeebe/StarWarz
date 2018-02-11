using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeController : MonoBehaviour
{
    public float ActivateSpeed = 5;
    
    private bool mIsOn;
    private float mTargetScale;

    public void ActivateBlade(bool doActivate)
    {
        mIsOn = doActivate;
        mTargetScale = mIsOn ? 1 : 0;
    }

    public void ToggleActivateBlade()
    {
        ActivateBlade(!mIsOn);
    }

    void FixedUpdate()
    {
        // Move towards the target scale with constant step.
        transform.localScale = new Vector3(transform.localScale.x,
            Mathf.MoveTowards(transform.localScale.y, mTargetScale, ActivateSpeed * Time.deltaTime),
            transform.localScale.z);
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
