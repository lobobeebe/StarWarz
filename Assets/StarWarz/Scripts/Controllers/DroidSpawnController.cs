using UnityEngine;
using HTC.UnityPlugin.Vive;
using System.Collections.Generic;

public class DroidSpawnController : MonoBehaviour
{
    public GameObject TrainingDroidPrefab;
    public GameObject Player;

    public enum GameMode
    {
        Training,
        Survival,
        FreePlay
    }

    // Timing
    private float FREE_PLAY_SPAWN_DELAY_SECONDS = Mathf.Infinity;
    private float FREE_PLAY_INITIAL_SPAWN_TIME_SECONDS = Mathf.Infinity;

    private float TRAINING_MODE_SPAWN_DELAY_SECONDS = Mathf.Infinity;
    private float TRAINING_MODE_INITIAL_SPAWN_TIME_SECONDS = 0;

    private float SURVIVAL_SPAWN_DELAY_SECONDS = 5;
    private float SURVIVAL_INITIAL_SPAWN_TIME_SECONDS = 0;

    private float mSpawnDelaySeconds;
    private float mNextSpawnTimeSeconds;
    
    private List<GameObject> mDroids = new List<GameObject>();

    // Game Mode
    private GameMode mMode;

    public GameMode Mode
    {
        get
        {
            return mMode;
        }

        set
        {
            if (mMode != value)
            {
                ClearDroids();

                mMode = value;

                switch (mMode)
                {
                    case GameMode.FreePlay:
                        mSpawnDelaySeconds = FREE_PLAY_SPAWN_DELAY_SECONDS;
                        mNextSpawnTimeSeconds = Time.time + FREE_PLAY_INITIAL_SPAWN_TIME_SECONDS;
                        break;

                    case GameMode.Training:
                        mSpawnDelaySeconds = TRAINING_MODE_SPAWN_DELAY_SECONDS;
                        mNextSpawnTimeSeconds = Time.time + TRAINING_MODE_INITIAL_SPAWN_TIME_SECONDS;
                        break;

                    case GameMode.Survival:
                        mSpawnDelaySeconds = SURVIVAL_SPAWN_DELAY_SECONDS;
                        mNextSpawnTimeSeconds = Time.time + SURVIVAL_INITIAL_SPAWN_TIME_SECONDS;
                        break;
                }
            }
        }
    }

    private void Start()
    {
        Mode = GameMode.FreePlay;
    }

    void ClearDroids()
    {
        foreach(GameObject obj in mDroids)
        {
            Destroy(obj);
        }

        mDroids.Clear();
    }

    // Update is called once per frame
    void FixedUpdate ()
    {
        // If the spawn delay has completed, spawn
        if (Time.time > mNextSpawnTimeSeconds)
        {
            Spawn();
            mNextSpawnTimeSeconds = Time.time + mSpawnDelaySeconds;
        }
        
        if (ViveInput.GetPressDown(HandRole.LeftHand, ControllerButton.Menu))
        {
            switch (Mode)
            {
                case GameMode.FreePlay:
                    Mode = GameMode.Training;
                    break;

                case GameMode.Training:
                    Mode = GameMode.Survival;
                    break;

                case GameMode.Survival:
                    Mode = GameMode.FreePlay;
                    break;
            }
        }
    }

    void Spawn()
    {
        // Choose a random spawn
        float randomRotation = Random.value * Mathf.PI * 2;
        Vector3 spawnLocation = new Vector3(Mathf.Cos(randomRotation) * 10, .5f + Random.value * 3, Mathf.Sin(randomRotation) * 10);
        GameObject droid = Instantiate(TrainingDroidPrefab, spawnLocation, Quaternion.identity);
        mDroids.Add(droid);

        DroidController droidController = droid.GetComponent<DroidController>();
        droidController.Player = Player;

        if (Mode == GameMode.Survival)
        {
            droidController.mIsAttackDroid = true;
        }
    }
}
