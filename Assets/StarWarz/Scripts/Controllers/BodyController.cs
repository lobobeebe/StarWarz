using UnityEngine;

public class BodyController : MonoBehaviour
{
    public HealthWidget HealthWidget;

    private void TakeDamage(float damage)
    {
        if (HealthWidget)
        {
            HealthWidget.CurrentHealth -= damage;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Projectile")
        {
            TakeDamage(10);

            LaserBoltController laserBolt = other.gameObject.GetComponentInParent<LaserBoltController>();
            if (laserBolt)
            {
                laserBolt.Explode();
            }
        }       
    }
}
