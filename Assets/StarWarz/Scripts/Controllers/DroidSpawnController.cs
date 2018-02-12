using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroidSpawnController : MonoBehaviour
{
    public GameObject DroidPrefab;
    public GameObject Player;

    // Timing
    private float mSpawnDelaySeconds = 10;
    private float mNextSpawnTimeSeconds = 5;
	
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
        // Choose a random spawn
        Vector3 spawnLocation = new Vector3(-5 + Random.value * 10, .5f, -5 + Random.value * 10);
        GameObject droid = Instantiate(DroidPrefab, spawnLocation, Quaternion.identity);

        DroidController droidController = droid.GetComponent<DroidController>();
        droidController.SetTargetPlayer(Player);
    }
}
