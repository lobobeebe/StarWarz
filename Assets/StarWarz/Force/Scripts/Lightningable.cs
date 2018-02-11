using UnityEngine;

public class Lightningable : MonoBehaviour
{
    public delegate void LightningedHandler();
    public LightningedHandler Lightninged = delegate { };
    
    void OnTriggerStay(Collider collider)
    {
        LightningController lightningContoller = collider.GetComponentInParent<LightningController>();

        if (lightningContoller)
        {
            Lightninged();
        }
    }
}
