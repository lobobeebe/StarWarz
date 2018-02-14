using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BladeController : MonoBehaviour
{
    public float ActivateSpeed = 5;
    public AudioClip BladeOnSound;
    public AudioClip BladeOffSound;
    public AudioClip BladeConstantSound;
    public AudioClip BladeReflectSound;
    public Light BladeLight;

    private AudioSource mAudioSource;

    private bool mIsOn;
    private float mTargetScale;

    private void Start()
    {
        mAudioSource = GetComponent<AudioSource>();
        mAudioSource.clip = BladeConstantSound;
        BladeLight.intensity = 0;
    }

    public void ActivateBlade(bool doActivate)
    {
        if (doActivate && !mIsOn)
        {
            mAudioSource.PlayOneShot(BladeOnSound);
            mAudioSource.Play();
            BladeLight.intensity = 1;
        }
        else if (!doActivate && mIsOn)
        {
            mAudioSource.Stop();
            mAudioSource.PlayOneShot(BladeOffSound);
            BladeLight.intensity = 0;
        }

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
        if (mIsOn)
        {
            if (other.tag == "Projectile")
            {
                LaserBoltController laserBolt = other.gameObject.GetComponentInParent<LaserBoltController>();
                laserBolt.Reflect();

                mAudioSource.PlayOneShot(BladeReflectSound);
            }

            Destroyable destroyable = other.gameObject.GetComponent<Destroyable>();
            if (destroyable)
            {
                destroyable.Destroyed();
            }
        }
    }
}
