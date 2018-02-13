using UnityEngine;
using HTC.UnityPlugin.Vive;

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

    // Game Mode
    private GameMode mMode = GameMode.FreePlay;
    private Vector2 mPreviousPressVector;

    public GameMode Mode
    {
        get
        {
            return mMode;
        }

        set
        {
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

    private void Start()
    {
        Mode = GameMode.FreePlay;
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

        Vector2 pressVector = ViveInput.GetPadPressVector(HandRole.LeftHand);
        if (pressVector == Vector2.zero && mPreviousPressVector != Vector2.zero)
        {
            if (mPreviousPressVector.x > Mathf.Abs(mPreviousPressVector.y))
            {
                Mode = GameMode.FreePlay;
            }
            else if (mPreviousPressVector.x < -Mathf.Abs(mPreviousPressVector.y))
            {
                Mode = GameMode.Training;
            }
            else if (mPreviousPressVector.y > Mathf.Abs(mPreviousPressVector.x))
            {
                Mode = GameMode.Survival;
            }
        }

        mPreviousPressVector = pressVector;
    }

    void Spawn()
    {
        // Choose a random spawn
        Vector3 spawnLocation = new Vector3(-5 + Random.value * 10, .5f, -5 + Random.value * 10);
        GameObject droid = Instantiate(TrainingDroidPrefab, spawnLocation, Quaternion.identity);

        DroidController droidController = droid.GetComponent<DroidController>();
        droidController.SetTargetPlayer(Player);
    }
}
