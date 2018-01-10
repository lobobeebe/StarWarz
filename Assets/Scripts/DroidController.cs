using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroidController : MonoBehaviour
{
    public GameObject LaserBoltPrefab;

    // Movement
    private Vector3 mTargetLocation;
    private float mSpeed = 5;

    // Firing
    private GameObject mPlayer;
    private float mShotDelaySeconds = 1; // 1 Shot/s
    private float mNextShotTimeSeconds;

    // Use this for initialization
    void Start ()
    {
        mTargetLocation = transform.position;
	}
	
	void FixedUpdate()
    {
        // Rotate to look at the player
        if (mPlayer != null)
        {
            transform.LookAt(mPlayer.transform);
        }

        // Move towards target location
        if (mTargetLocation != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, mTargetLocation, mSpeed * Time.deltaTime);
        }

        // If the shot delay has completed, shoot
        if (Time.time > mNextShotTimeSeconds)
        {
            Shoot();
            mNextShotTimeSeconds = Time.time + mShotDelaySeconds;
        }
    }

    public void SetTargetPlayer(GameObject player)
    {
        mPlayer = player;
    }

    public void SetTargetLocation(Vector3 targtLocation)
    {
        mTargetLocation = targtLocation;
    }

    private void Shoot()
    {
        Instantiate(LaserBoltPrefab, transform.position, transform.rotation);
    }
}
