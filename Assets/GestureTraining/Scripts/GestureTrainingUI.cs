using LoboLabs.GestureNeuralNet;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using LoboLabs.Utilities;

public class GestureTrainingUI : MonoBehaviour, GestureDataReceiver
{
    public UnityGesturer Gesturer;

    // Buttons
    private static Vector3 BUTTON_START_POSITION = new Vector3(115, 130);
    private static Vector3 BUTTON_OFFSET = new Vector3(0, -35);

    public GameObject ButtonPrefab;
    private List<GameObject> mGestureButtons;
    private Vector3 mNextGestureButtonPosition;

    private Slider mThresholdSlider;
    private Text mThresholdValue;

    // Gesturing management
    private GestureNeuralNetworkGenerator mGenerator;
    private GestureDetector mDetector;
    private bool mIsTraining;

    // Use this for initialization
    void Start()
    {
        mGestureButtons = new List<GameObject>();
        mNextGestureButtonPosition = BUTTON_START_POSITION;
        
        mGenerator = new GestureNeuralNetworkGenerator();
        Gesturer.SetReceiver(this);

        mThresholdSlider = GameObject.Find("ThresholdSlider").GetComponent<Slider>();
        mThresholdValue = GameObject.Find("ThresholdValue").GetComponent<Text>();

        mIsTraining = true;
    }

    private void AddGestureButton(string gestureName)
    {
        GameObject go = Instantiate(ButtonPrefab, transform);
        GestureButtonScript button = go.GetComponent<GestureButtonScript>();

        button.Setup(gestureName, mNextGestureButtonPosition, this);

        mGestureButtons.Add(go);
        mNextGestureButtonPosition += BUTTON_OFFSET;
    }

    public void ClearGestureButtons()
    {
        // Clear the buttons
        foreach (GameObject go in mGestureButtons)
        {
            Destroy(go);
        }

        mGestureButtons.Clear();
        mNextGestureButtonPosition = BUTTON_START_POSITION;
    }

    public void OnAddClicked()
    {
        InputField inputField = GetComponentInChildren<InputField>();

        if (inputField.text != "" && mGestureButtons.Count < 8)
        {
            AddGestureButton(inputField.text);
            SetTrainingGesture(inputField.text);

            // Clear input
            inputField.text = "";
        }
    }

    public void OnClearClicked()
    {
        // Clear buttons
        ClearGestureButtons();

        mGenerator.ClearGestures();
    }

    public void OnGenerateClicked()
    {
        mDetector = new GestureDetector(mGenerator.Generate(), mGenerator.GetGestureNames());
        mDetector.MinThreshold = mGenerator.MinThreshold;
        mDetector.GestureDetected += OnGestureDetected;
        
        UnityGestureIO.SaveDetector("data/Detector.gd", mDetector);

        OnToggleTrainingClicked();
    }

    protected void OnGestureDetected(string gestureName)
    {
        if (gestureName.Length > 0)
        {
            SetStatusText("DETECTED: (" + gestureName + ")");
        }
    }

    public void OnLoadClicked()
    {
        // Clear buttons
        ClearGestureButtons();

        using (BinaryReader reader = new BinaryReader(File.Open("data/Generator.gg", FileMode.Open)))
        {
            mGenerator = new GestureNeuralNetworkGenerator(reader);
        }
        
        // Load in gestures
        List<string> gestureNames = mGenerator.GetGestureNames();

        // Create buttons for the gestures
        foreach (string name in gestureNames)
        {
            AddGestureButton(name);
        }

        if (gestureNames.Count > 0)
        {
            SetTrainingGesture(gestureNames[gestureNames.Count - 1]);
        }

        // Set current Generator threshold
        mThresholdSlider.value = mGenerator.MinThreshold;

        SetStatusText("LOAD Success");
    }

    public void OnSaveClicked()
    {
        using (BinaryWriter writer = new BinaryWriter(File.Open("data/Generator.gg", FileMode.Create)))
        {
            mGenerator.Save(writer);
        }
    }
    
    public void OnThresholdValueChanged()
    {
        float value = Mathf.Round(mThresholdSlider.value * 100.0f) / 100.0f;
        mThresholdValue.text = value.ToString();

        mGenerator.MinThreshold = value;
    }

    public void OnToggleTrainingClicked()
    {
        mIsTraining = !mIsTraining;

        if (mIsTraining)
        {
            SetStatusText("TRAINING MODE");
        }
        else
        {
            SetStatusText("PRACTICE MODE");;
        }
    }

    public void SetStatusText(string text)
    {
        GetComponentInChildren<Text>().text = text;
    }

    public void SetTrainingGesture(string name)
    {
        mGenerator.CurrentGestureName = name;
        SetStatusText("Training: " + name);
    }

    public void StartGesturing()
    {
        SetStatusText("Gesture Begin");

        if (mIsTraining)
        {
            mGenerator.StartGesturing();
        }
        else
        {
            mDetector.StartGesturing();
        }
    }

    public void StopGesturing()
    {
        SetStatusText("Gesture Complete");

        if (mIsTraining)
        {
            mGenerator.StopGesturing();
        }
        else
        {
            mDetector.StopGesturing();
        }
    }

    public void UpdateGesturePosition(Vector position)
    {
        // TODO: Possibly create 3D representation of Gesture
        if (mIsTraining)
        {
            mGenerator.UpdateGesturePosition(position);
        }
        else
        {
            mDetector.UpdateGesturePosition(position);
        }
    }
}
