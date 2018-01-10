using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBoltController : MonoBehaviour
{
    private float mSpeed = 5; // 5 units/s
	
	// Update is called once per frame
	void Update ()
    {
        // Move the position forward (according to the bolt's rotation)
        transform.position += transform.forward * mSpeed * Time.deltaTime;	
	}
}
