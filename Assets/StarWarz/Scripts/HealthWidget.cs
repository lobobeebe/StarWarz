using UnityEngine;

public class HealthWidget : MonoBehaviour
{
    public float MaxHealth;
    public float CurrentHealth;

    private GameObject mCurrentHealthWidget;

	// Use this for initialization
	void Start ()
    {
        mCurrentHealthWidget = transform.Find("CurrentHealth").gameObject;
	}

    // Update is called once per frame
    void FixedUpdate()
    {
        if (MaxHealth != 0 && mCurrentHealthWidget)
        {
            float scale = Mathf.Clamp(CurrentHealth / MaxHealth, 0, 1);

            Vector3 scaleVector = mCurrentHealthWidget.transform.localScale;
            scaleVector.x = scale;
            mCurrentHealthWidget.transform.localScale = scaleVector;
        }
	}
}
