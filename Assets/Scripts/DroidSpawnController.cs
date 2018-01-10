using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroidSpawnController : MonoBehaviour
{
    public GameObject DroidPrefab;
    public GameObject Player;

    // Timing
    private float mSpawnDelaySeconds = 3000;
    private float mNextSpawnTimeSeconds;
	
	// Update is called once per frame
	void Update ()
    {
        // If the spawn delay has completed, spawn
        if (Time.time > mNextSpawnTimeSeconds)
        {
            Spawn();
            mNextSpawnTimeSeconds = Time.time + mSpawnDelaySeconds;
        }
    }

    void Spawn()
    {
        // Choose a random spawn location below the floor. 
        Vector3 spawnLocation = new Vector3(-10 + Random.value * 20, -0.5f, -10 + Random.value * 20);
        GameObject droid = Instantiate(DroidPrefab, spawnLocation, Quaternion.identity);

        // The droid will rise directly above its spawn location to a random height.
        Vector3 firstTargetLocation = new Vector3(spawnLocation.x, Random.value * 3, spawnLocation.z);

        DroidController droidController = droid.GetComponent<DroidController>();
        droidController.SetTargetLocation(firstTargetLocation);
        droidController.SetTargetPlayer(Player);
    }
}
