using HTC.UnityPlugin.Vive;
using UnityEngine;

public class HandController : MonoBehaviour
{
    // Lightsaber Management
    private LightsaberController mLightSaberHolding;
    private GameObject mLightsaberTouching;
    private GameObject mHolsterTouching;

    private ViveRoleProperty mHandRole;
    
    void Awake()
    {
        mHandRole = GetComponent<ViveRoleSetter>().viveRole;
    }
    
    void Update()
    {
        // Grip Button = Lightsaber or Force
        if (ViveInput.GetPressDown(mHandRole, ControllerButton.Grip))
        {
            bool isUpdated = UpdateLightsaberAnchor();

            // If a Lightsaber wasn't picked up or put down, begin force
            if (!isUpdated)
            {
            }
        }

        // Top Menu Button = Toggle Lightsaber
        if (ViveInput.GetPressDown(mHandRole, ControllerButton.Menu))
        {
            if (mLightSaberHolding)
            {
                mLightSaberHolding.ToggleActivateBlade();
            }
        }
    }

    private bool UpdateLightsaberAnchor()
    {
        if (mLightSaberHolding)
        {
            if (mHolsterTouching != null)
            {
                mLightSaberHolding.SetAnchor(mHolsterTouching);
                mLightSaberHolding = null;

                return true;
            }
        }
        else
        {
            if (mLightsaberTouching != null)
            {
                mLightSaberHolding = mLightsaberTouching.GetComponentInParent<LightsaberController>();
                mLightSaberHolding.SetAnchor(gameObject);

                return true;
            }
        }

        return false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (!mHolsterTouching && other.gameObject.tag == "Holster")
        {
            mHolsterTouching = other.gameObject;
        }
        else if (!mLightsaberTouching && other.gameObject.tag == "Lightsaber")
        {
            mLightsaberTouching = other.gameObject;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Holster" && mHolsterTouching.gameObject == other.gameObject)
        {
            mHolsterTouching = null;
        }
        else if (other.gameObject.tag == "Lightsaber" && mLightsaberTouching.gameObject == other.gameObject)
        {
            mLightsaberTouching = null;
        }
    }
}
