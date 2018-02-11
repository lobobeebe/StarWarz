using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Pushable))]
[RequireComponent(typeof(Lightningable))]
public class LaserBoltController : MonoBehaviour
{
    private Pushable mPushable;
    private Lightningable mLightningable;

    private const float SPEED = 5; // 5 units/s
    private const float REFLECT_SPEED = 10; // 10 units/s

    private const float REFLECTION_OFFSET = .5f;

    private bool mIsMovementActive;
    private GameObject mParentDroid;

    private bool mIsReflected;
    private float mSpeed = SPEED;

    void Start()
    {
        mIsMovementActive = true;

        mPushable = GetComponent<Pushable>();
        mPushable.Pushed += OnPushed;

        mLightningable = GetComponent<Lightningable>();
        mLightningable.Lightninged += OnLightninged;
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if (mIsMovementActive)
        {
            if (mIsReflected && mParentDroid)
            {
                transform.LookAt(mParentDroid.transform.position);
            }
            
            // Move the position forward (according to the bolt's rotation)
            transform.position += transform.forward * mSpeed * Time.deltaTime;
        }
	}

    public void Explode()
    {
        Destroy(gameObject);
    }

    public void OnPushed()
    {
        mIsMovementActive = false;
    }

    public void OnLightninged()
    {
        Explode();
    }

    public void Reflect()
    {
        if (mParentDroid)
        {
            mIsReflected = true;
            mSpeed = REFLECT_SPEED;
        }
        else
        {
            Explode();
        }
    }

    public void SetParentDroid(GameObject parentDroid)
    {
        mParentDroid = parentDroid;
    }
}
