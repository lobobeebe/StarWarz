using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BodyController : MonoBehaviour
{
    public AudioClip[] DamageAudioClips;

    private int mNextClipIndex;
    private AudioSource mAudioSource;

    public HealthWidget HealthWidget;

    private void Start()
    {
        mAudioSource = GetComponent<AudioSource>();
    }

    public void TakeDamage(float damage)
    {
        if (HealthWidget)
        {
            HealthWidget.CurrentHealth -= damage;
        }

        if (DamageAudioClips.Length > mNextClipIndex)
        {
            mAudioSource.PlayOneShot(DamageAudioClips[mNextClipIndex]);
        }

        mNextClipIndex = (mNextClipIndex + 1) % DamageAudioClips.Length;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Projectile")
        {
            TakeDamage(10);

            LaserBoltController laserBolt = other.gameObject.GetComponentInParent<LaserBoltController>();
            if (laserBolt)
            {
                laserBolt.Explode();
            }
        }       
    }
}
