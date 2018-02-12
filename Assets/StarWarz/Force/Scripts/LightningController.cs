using DigitalRuby.LightningBolt;
using HTC.UnityPlugin.Vive;
using UnityEngine;

[RequireComponent(typeof(ViveRoleSetter))]
[RequireComponent(typeof(AudioSource))]
public class LightningController : MonoBehaviour
{
    public GameObject LightningPrefab;
    public AudioClip LightningLoopClip;

    private AudioSource mAudioSource;
    private GameObject mLightningInstance;
    private LightningBoltScript mLightningScript;

    private ViveRoleProperty mHandRole;
    private bool mIsActive;

    private void Start()
    {
        mHandRole = GetComponent<ViveRoleSetter>().viveRole;

        if (LightningPrefab)
        {
            mLightningInstance = Instantiate(LightningPrefab, transform);
            mLightningInstance.SetActive(false);
            mLightningScript = mLightningInstance.GetComponent<LightningBoltScript>();
        }

        mAudioSource = GetComponent<AudioSource>();
    }

    public void Activate()
    {
        mIsActive = true;
    }

    public void Deactivate()
    {
        mIsActive = false;
    }

    public void FixedUpdate()
    {
        if (mLightningInstance && mIsActive)
        {
            if (ViveInput.GetPressDown(mHandRole, ControllerButton.Trigger))
            {
                mLightningInstance.SetActive(true);
                mAudioSource.clip = LightningLoopClip;
                mAudioSource.Play();
            }
            else if (ViveInput.GetPressUp(mHandRole, ControllerButton.Trigger))
            {
                mLightningInstance.SetActive(false);
                mAudioSource.Stop();
            }
        }
    }
}
