using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Pushable))]
[RequireComponent(typeof(Liftable))]
[RequireComponent(typeof(Lightningable))]
[RequireComponent(typeof(AudioSource))]
public class DroidController : MonoBehaviour
{
    public GameObject LaserBoltPrefab;
    public float ImpulseDestructionThresholdSquared;

    public AudioClip HoverLoopClip;
    public AudioClip CollisionExplosionClip;

    // Movement
    private const float NEXT_LOCATION_DELAY_SECONDS = 15;
    private float mNextLocationTimeSeconds = NEXT_LOCATION_DELAY_SECONDS;

    private const float SPEED = 1;

    private bool mIsMovementActive;

    private const float BASE_TARGET_LOCATION_HEIGHT = 3.0f;
    private const float TARGET_LOCATION_HEIGHT_OFFSET = 1.0f;

    private const float BASE_TARGET_LOCATION_HORIZONTAL = -10.0f;
    private const float TARGET_LOCATION_HORIZONTAL_OFFSET = 10.0f;

    private Vector3 mTargetLocation;

    // Force
    private Pushable mPushable;
    private Liftable mLiftable;
    private Lightningable mLightningable;

    // Firing
    private GameObject mPlayer;
    private float mShotDelaySeconds = 1f; // 1 Shot/s
    private float mNextShotTimeSeconds;
    private const float HALF_SHOT_OFFSET = .25f;

    // Audio
    private AudioSource mAudioSource;

    // Use this for initialization
    void Start ()
    {
        // The droid will rise directly above its spawn location to a random height.
        Vector3 firstTargetLocation = new Vector3(transform.position.x, BASE_TARGET_LOCATION_HEIGHT + (Random.value * TARGET_LOCATION_HEIGHT_OFFSET), transform.position.z);
        SetTargetLocation(firstTargetLocation);

        mIsMovementActive = true;

        mPushable = GetComponent<Pushable>();
        mPushable.Pushed += OnPushed;

        mLiftable = GetComponent<Liftable>();
        mLiftable.Lifted += OnLifted;

        mLightningable = GetComponent<Lightningable>();
        mLightningable.Lightninged += OnLightninged;

        mAudioSource = GetComponent<AudioSource>();
        mAudioSource.clip = HoverLoopClip;
        mAudioSource.Play();
    }
	
	void FixedUpdate()
    {
        if (mIsMovementActive)
        {
            // Rotate to look at the player
            if (mPlayer != null)
            {
                transform.LookAt(mPlayer.transform.position + new Vector3(0, -.2f, 0));
            }

            // Move towards target location
            if (mTargetLocation != null)
            {
                transform.position = Vector3.MoveTowards(transform.position, mTargetLocation, SPEED * Time.deltaTime);
            }

            // If the shot delay has completed, shoot
            if (Time.time > mNextShotTimeSeconds)
            {
                Shoot();
                mNextShotTimeSeconds = Time.time + mShotDelaySeconds;
            }

            // If the next target delay has completed, update target location
            if (Time.time > mNextLocationTimeSeconds)
            {
                Vector3 nextTargetLocation = new Vector3(BASE_TARGET_LOCATION_HORIZONTAL + (Random.value * TARGET_LOCATION_HORIZONTAL_OFFSET),
                    BASE_TARGET_LOCATION_HEIGHT + (Random.value * TARGET_LOCATION_HEIGHT_OFFSET),
                    BASE_TARGET_LOCATION_HORIZONTAL + (Random.value * TARGET_LOCATION_HORIZONTAL_OFFSET));
                SetTargetLocation(nextTargetLocation);

                mNextLocationTimeSeconds = Time.time + NEXT_LOCATION_DELAY_SECONDS;
            }
        }
    }

    public void Explode(bool byCollision = false)
    {
        mAudioSource.PlayOneShot(CollisionExplosionClip);

        Destroy(gameObject);
    }

    public void OnLifted()
    {
        mIsMovementActive = false;
    }

    public void OnLightninged()
    {
        Explode();
    }

    public void OnPushed()
    {
        mIsMovementActive = false;
    }

    public void SetTargetPlayer(GameObject player)
    {
        mPlayer = player;
    }

    public void SetTargetLocation(Vector3 targetLocation)
    {
        mTargetLocation = targetLocation;
    }

    private void Shoot()
    {
        Vector3 positionOffset = new Vector3(-HALF_SHOT_OFFSET + Random.value * 2 * HALF_SHOT_OFFSET,
                -HALF_SHOT_OFFSET + Random.value * 2 * HALF_SHOT_OFFSET,
                -HALF_SHOT_OFFSET + Random.value * 2 * HALF_SHOT_OFFSET);

        Vector3 laserDirection = (mPlayer.transform.position + new Vector3(0, -.2f, 0) + positionOffset) - transform.position;
        Quaternion laserRotation = Quaternion.LookRotation(laserDirection, Vector3.up);

        GameObject laserBolt = Instantiate(LaserBoltPrefab, transform.position + (transform.forward * .5f), laserRotation);
        laserBolt.GetComponent<LaserBoltController>().SetParentDroid(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Projectile")
        {
            Explode();

            LaserBoltController laserBolt = other.gameObject.GetComponentInParent<LaserBoltController>();
            if (laserBolt)
            {
                laserBolt.Explode();
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.impulse.sqrMagnitude > ImpulseDestructionThresholdSquared)
        {
            Explode();
        }
    }
}
