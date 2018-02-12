using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Pushable : MonoBehaviour
{
    public delegate void PushedHandler();
    public PushedHandler Pushed = delegate { };
    
    private Rigidbody mRigidbody;
    
    void Start()
    {
        mRigidbody = GetComponent<Rigidbody>();
    }

    void OnTriggerEnter(Collider collider)
    {
        Pusher pushScript = collider.GetComponentInParent<Pusher>();

        if (pushScript)
        {
            Pushed();

            mRigidbody.AddForce(pushScript.PushDirection * pushScript.PushForce);
        }
    }
}
