using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeToLive : MonoBehaviour
{
    public float TimeToLiveSeconds;

    private float mCreationTime;

    void Start()
    {
        mCreationTime = Time.time;
    }

    // Update is called once per frame
    void FixedUpdate ()
    {
        // Destroy self if lifetime has been met
        if (Time.time >= TimeToLiveSeconds + mCreationTime)
        {
            Destroy(gameObject);
        }
    }
}
