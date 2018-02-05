using LoboLabs.GestureNeuralNet;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class GestureButtonScript : MonoBehaviour
{
    private Button ParentButton;

	// Use this for initialization
	void Start ()
    {
        ParentButton = GetComponent<Button>();
        ParentButton.onClick.AddListener(() => OnClicked());
	}

    private GestureTrainingUI TrainingUI
    {
        get;
        set;
    }

    public void OnClicked()
    {
        Text text = GetComponentInChildren<Text>();
        TrainingUI.SetTrainingGesture(text.text);
    }

    public void Setup(string name, Vector3 position, GestureTrainingUI trainingUI)
    {
        transform.localScale = new Vector3(1, 1);
        transform.localRotation = Quaternion.Euler(0, 0, 0);
        transform.localPosition = position;

        Text text = GetComponentInChildren<Text>();
        text.text = name;

        TrainingUI = trainingUI;
    }
}
