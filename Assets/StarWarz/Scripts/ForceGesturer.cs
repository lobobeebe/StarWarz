using LoboLabs.GestureNeuralNet;
using HTC.UnityPlugin.Vive;
using UnityEngine;

[RequireComponent(typeof(Lifter))]
[RequireComponent(typeof(LightningController))]
public class ForceGesturer : UnityGesturer
{
    private const string FORCE_LIFT = "lift";
    private const string FORCE_HEAL = "heal";
    private const string FORCE_PUSH = "push";
    private const string FORCE_LIGHTNING = "lightning ";
    private const string FORCE_GLIMPSE = "glimpse";

    public GameObject PushHitBox;
    public HealthWidget HealthWidget;

    private GestureDetector mDetector;
    private Vector3 mGestureStartPosition;

    private Lifter mLifter;
    private LightningController mLightningController;
    private GlimpseController mGlimpse;

    public override void Start()
    {
        base.Start();

        mDetector = UnityGestureIO.LoadDetector("data/ForceDetector.gd");
        mDetector.MinThreshold = .83f;
        mDetector.GestureDetected += OnGestureDetected;
        SetReceiver(mDetector);

        mLifter = GetComponent<Lifter>();
        mLightningController = GetComponent<LightningController>();
        mGlimpse = GetComponentInChildren<GlimpseController>();
        mGlimpse.gameObject.SetActive(false);
    }

    protected override void OnGestureButtonEngaged(HandRole hand)
    {
        // Manually set the Force Lift State to idle if trying to gesture.
        mLifter.LiftState = Lifter.ForceLiftState.Idle;

        // Manually deactivate the Force Lightning
        mLightningController.Deactivate();

        // Manually deactivate the Glimpse if trying to gesture
        mGlimpse.gameObject.SetActive(false);

        base.OnGestureButtonEngaged(hand);
        mGestureStartPosition = transform.position;
    }

    private void OnGestureDetected(string gestureName)
    {
        // Process Detection
        if (gestureName.Length > 0)
        {
            switch (gestureName)
            {
                case FORCE_PUSH:
                    OnPushDetected();
                    break;

                case FORCE_LIFT:
                    OnLiftDetected();
                    break;

                case FORCE_HEAL:
                    OnHealDetected();
                    break;

                case FORCE_LIGHTNING:
                    OnLightningDetected();
                    break;

                case FORCE_GLIMPSE:
                    OnGlimpseDetected();
                    break;

                default:
                    break;
            }

            SetStatusText("DETECTED: " + "(" + gestureName + ")");
        }
    }

    private void OnPushDetected()
    {
        // Create a Push Hit Box to push Pushables
        if (PushHitBox)
        {
            Instantiate(PushHitBox, transform.position, Quaternion.LookRotation(transform.position - mGestureStartPosition));
        }
    }

    private void OnLiftDetected()
    {
        // The Lifter should now be activated for selection
        mLifter.LiftState = Lifter.ForceLiftState.Selecting;
    }

    private void OnHealDetected()
    {
        // TODO: Add effect
        if (HealthWidget)
        {
            HealthWidget.CurrentHealth = HealthWidget.MaxHealth;
        }
    }

    private void OnLightningDetected()
    {
        mLightningController.Activate();
    }

    private void OnGlimpseDetected()
    {
        mGlimpse.gameObject.SetActive(true);
    }

    public void SetStatusText(string text)
    {
        GetComponentInChildren<TextMesh>().text = text;
    }
}
