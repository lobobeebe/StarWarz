using UnityEngine;

public class StickToBody : MonoBehaviour
{
    public GameObject Head;
    public float HeightFraction;
    public float ForwardOffset;

    private void FixedUpdate()
    {
        Vector3 forwardVector = Head.transform.forward;
        forwardVector.y = 0; // Force the forward vector to stay level

        // Match 2D position and a fraction of the height
        transform.position = (new Vector3(Head.transform.position.x,
            Head.transform.position.y * HeightFraction,
            Head.transform.position.z)) + forwardVector * ForwardOffset;

        // Match 2D Rotation
        transform.rotation = Quaternion.LookRotation(forwardVector, Vector3.up);
    }
}
