using LoboLabs.GestureNeuralNet;
using LoboLabs.Utilities;
using UnityEngine;
using HTC.UnityPlugin.Vive;

public class UnityGesturer : MonoBehaviour
{
    public bool IsRightHand;

    // Gesturing management
    protected HandRole mHand;
    private GestureDataReceiver mGestureReceiver;
    private GameObject mGesturingStart;
    private bool mIsGesturing;
    
    // Use this for initialization
    public virtual void Start()
    {
        LogWriter.SetLogWriter(new UnityLogWriter());

        mHand = IsRightHand ? HandRole.RightHand : HandRole.LeftHand;
    }
    
    public void SetReceiver(GestureDataReceiver gestureReceiver)
    {
        mGestureReceiver = gestureReceiver;
    }

    public virtual void FixedUpdate()
    {
        // If gesturing, update the reciver with the positions of the controllers
        Vector3 positionFromStart;
        Vector gesturePosition;
        if (mGestureReceiver != null && mIsGesturing && mGesturingStart != null)
        {
            // Get the position in reference to the start point
            positionFromStart = mGesturingStart.transform.InverseTransformPoint(transform.position);
            gesturePosition = new Vector(positionFromStart.x, positionFromStart.y, positionFromStart.z);

            mGestureReceiver.UpdateGesturePosition(gesturePosition);
        }
    }

    public void Update()
    {
        ControllerButton gestureButton = ControllerButton.Grip;

        if (ViveInput.GetPressDown(mHand, gestureButton))
        {
            OnGestureButtonEngaged(mHand);
        }
        else if (ViveInput.GetPressUp(mHand, gestureButton))
        {
            OnGestureButtonUnengaged(mHand);
        }
    }

    protected virtual void OnGestureButtonEngaged(HandRole hand)
    {
        if (mGesturingStart != null)
        {
            Destroy(mGesturingStart);
        }

        mGestureReceiver.StartGesturing();

        // Create a Game Object to serve as the Gesturing Start
        mGesturingStart = new GameObject("GestureStartPoint");
        mGesturingStart.transform.position = transform.position;
        mGesturingStart.transform.rotation = transform.rotation;

        mIsGesturing = true;
        Debug.Log("Gesturing Begin");
    }

    protected virtual void OnGestureButtonUnengaged(HandRole hand)
    {
        if (mIsGesturing)
        {
            mGestureReceiver.StopGesturing();

            mIsGesturing = false;
            Debug.Log("Gesturing Complete");
        }
    }
}
