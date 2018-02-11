using UnityEngine;

public class Liftable : MonoBehaviour
{
    public delegate void LiftedHandler();
    public LiftedHandler Lifted = delegate { };
}
