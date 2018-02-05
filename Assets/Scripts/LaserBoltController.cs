using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBoltController : MonoBehaviour
{
    private float mCreationTime;
    private const float LIFETIME_SECONDS = 5; // After 5 seconds, laser disappears
    private const float SPEED = 5; // 5 units/s

    private const float REFLECTION_OFFSET = .5f;

    private GameObject mParentDroid;

    void Start()
    {
        mCreationTime = Time.time;
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        // Move the position forward (according to the bolt's rotation)
        transform.position += transform.forward * SPEED * Time.deltaTime;

        // Destroy self if lifetime has been met
        if (Time.time >= LIFETIME_SECONDS + mCreationTime)
        {
            Destroy(gameObject);
        }
	}

    public void Reflect()
    {
        if (mParentDroid)
        {
            transform.LookAt(mParentDroid.transform.position + new Vector3(-REFLECTION_OFFSET + Random.value * 2 * REFLECTION_OFFSET,
                -REFLECTION_OFFSET + Random.value * 2 * REFLECTION_OFFSET,
                -REFLECTION_OFFSET + Random.value * 2 * REFLECTION_OFFSET));
        }
    }

    public void SetParentDroid(GameObject parentDroid)
    {
        mParentDroid = parentDroid;
    }
}
