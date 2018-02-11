using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroidSpawnController : MonoBehaviour
{
    public GameObject DroidPrefab;
    public GameObject Player;

    // Timing
    private float mSpawnDelaySeconds = 10;
    private float mNextSpawnTimeSeconds = 0;
	
	// Update is called once per frame
	void FixedUpdate ()
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

        DroidController droidController = droid.GetComponent<DroidController>();
        droidController.SetTargetPlayer(Player);
    }
}
