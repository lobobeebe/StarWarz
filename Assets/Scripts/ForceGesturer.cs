using LoboLabs.GestureNeuralNet;
using UnityEngine;
using UnityEngine.UI;

public class ForceGesturer : UnityGesturer
{
    enum ForceAbility
    {
        None,
        Lift,
        Heal,
        Push,
        Lightning,
        Glimpse
    }

    public GameObject FireBall;
    public GameObject Shield;

    private GestureDetector mDetector;
    private ForceAbility mCurrentAbility;

    public override void Start()
    {
        base.Start();

        mDetector = UnityGestureIO.LoadDetector("data/ForceDetector.gd");
        mDetector.MinThreshold = .83f;
        mDetector.GestureDetected += OnGestureDetected;
        SetReceiver(mDetector);

        mCurrentAbility = ForceAbility.None;
    }

    private void OnGestureDetected(string gestureName)
    {
        if (gestureName.Length > 0)
        {
            // TODO: Perform ability

            SetStatusText("DETECTED: " + "(" + gestureName + ")");
        }
    }

    public void SetStatusText(string text)
    {
        GetComponentInChildren<TextMesh>().text = text;
    }
}
