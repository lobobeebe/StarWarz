using UnityEngine;

public class Destroyable : MonoBehaviour
{
    public delegate void DestroyedHandler();
    public DestroyedHandler Destroyed = delegate { };
}
