using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    public GameObject Target;
    public GameObject Bullet;

    public Vector2 FloatOffset;
    public float FloatHeight = 4;
    public float FloatDistance = 3;

    public float FloatSpeed = 5;

    public float FireOffsetDistance = .3f;
    
    private float mSwitchTimer = 0;
    private float mSwitchTimeOut = 1000;

    private float mFireTimer = 0;
    private float mFireTimeOut = 200;


    // Use this for initialization
    void Start()
    {
        RandomizeLocation();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Target != null)
        {
            mSwitchTimer++;
            mFireTimer++;

            if (mSwitchTimer >= mSwitchTimeOut)
            {
                RandomizeLocation();

                mSwitchTimer = 0;
            }

            if (mFireTimer >= mFireTimeOut)
            {
                Vector3 targetLocation = Target.transform.position + new Vector3(0, -.5f) + Random.insideUnitSphere * FireOffsetDistance;
                GameObject bullet = Instantiate(Bullet, transform.position, Quaternion.LookRotation(
                    targetLocation - transform.position));
                bullet.layer = LayerMask.NameToLayer("Enemy");

                mFireTimer = 0;
            }

            Vector3 newLocation = Target.transform.position + new Vector3(FloatOffset.x, 0, FloatOffset.y) * FloatDistance;
            newLocation.y = FloatHeight;
            Vector3 directionVector = (newLocation - transform.position);

            if (directionVector.magnitude >= FloatSpeed)
            {
                transform.position += directionVector.normalized * FloatSpeed;
            }
        }
    }

    void RandomizeLocation()
    {
        FloatOffset = Random.insideUnitCircle;
    }
}
