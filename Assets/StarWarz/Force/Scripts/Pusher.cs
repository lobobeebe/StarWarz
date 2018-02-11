using UnityEngine;

public class Pusher : MonoBehaviour
{
    public float PushForce;

    public Vector3 PushDirection
    {
        get
        {
            return transform.forward;
        }
    }

    private float mStartTime;
    private float mLifetimeSeconds = .1f;

    private void Start()
    {
        mStartTime = Time.time;
    }

    private void FixedUpdate()
    {
        if (Time.time > mStartTime + mLifetimeSeconds)
        {
            Destroy(gameObject);
        }
    }
}
