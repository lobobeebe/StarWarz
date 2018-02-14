using HTC.UnityPlugin.Vive;
using UnityEngine;

[RequireComponent(typeof(ViveRoleSetter))]
[RequireComponent(typeof(AudioSource))]
public class Lifter : MonoBehaviour
{
    public AudioClip ForceRumble;
    public float LiftForce;
    public GameObject LiftSelector;
    public Material PositiveLiftMaterial;
    public Material NegativeLiftMaterial;

    private ViveRoleProperty mHand;
    private Liftable mCurrentLiftable;
    private GameObject mLiftSelector;

    private Vector3 mPreviousPosition;

    private AudioSource mAudioSource;

    private ForceLiftState mLiftState = ForceLiftState.Idle;

    public enum ForceLiftState
    {
        Idle,
        Selecting,
        Lifting
    }

    public void Start()
    {
        LiftState = ForceLiftState.Idle;

        mHand = GetComponent<ViveRoleSetter>().viveRole;

        if (LiftSelector)
        {
            mLiftSelector = Instantiate(LiftSelector);
            mLiftSelector.SetActive(false);
        }

        mAudioSource = GetComponent<AudioSource>();
    }

    public void FixedUpdate()
    {
        if (LiftState == ForceLiftState.Selecting)
        {
            // If the Hair Trigger is activated, start drawing a selector
            if (ViveInput.GetPress(mHand, ControllerButton.HairTrigger))
            {
                RaycastHit hit;

                // Raycast out from the hand position
                if (Physics.Raycast(transform.position, transform.forward, out hit))
                {
                    // Update the selector to be at the position of the collision
                    if (mLiftSelector)
                    {
                        mLiftSelector.SetActive(true);
                        mLiftSelector.transform.position = hit.point;
                    }

                    // Try to get a liftable from the collider.
                    mCurrentLiftable = hit.collider.GetComponentInParent<Liftable>();
                    Renderer renderer = mLiftSelector.GetComponent<MeshRenderer>();

                    if (mCurrentLiftable)
                    {
                        renderer.material = PositiveLiftMaterial;
                    }
                    else
                    {
                        renderer.material = NegativeLiftMaterial;
                    }
                }
            }
            else
            {
                mLiftSelector.SetActive(false);
            }

            if (ViveInput.GetPressDown(mHand, ControllerButton.FullTrigger) && mCurrentLiftable != null)
            {
                mLiftSelector.SetActive(false);
                LiftState = ForceLiftState.Lifting;

                mCurrentLiftable.Lifted();
            }
        }
        else if (LiftState == ForceLiftState.Lifting)
        {
            if (ViveInput.GetPressDown(mHand, ControllerButton.Trigger))
            {
                // Stop the object.
                Rigidbody liftableRigidbody = mCurrentLiftable.GetComponent<Rigidbody>();
                liftableRigidbody.velocity = Vector3.zero;
            }

            if (ViveInput.GetPress(mHand, ControllerButton.Trigger))
            {
                Rigidbody liftableRigidbody = mCurrentLiftable.GetComponent<Rigidbody>();
                liftableRigidbody.AddForce(LiftForce * (transform.position - mPreviousPosition));
            }
            else
            {
                mAudioSource.Stop();
            }
        }

        mPreviousPosition = transform.position;
    }

    public ForceLiftState LiftState
    {
        get
        {
            return mLiftState;
        }
        set
        {
            mLiftState = value;

            if (mLiftState == ForceLiftState.Selecting)
            {
                mAudioSource.clip = ForceRumble;
                mAudioSource.Play();
            }
            else if (mLiftState == ForceLiftState.Selecting)
            {
                mAudioSource.Stop();
            }
        }
    }
}
