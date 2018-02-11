using DigitalRuby.LightningBolt;
using HTC.UnityPlugin.Vive;
using UnityEngine;

[RequireComponent(typeof(ViveRoleSetter))]
public class LightningController : MonoBehaviour
{
    public GameObject Lightning;

    private GameObject LightningInstance;
    private LightningBoltScript mLightningScript;

    private ViveRoleProperty mHandRole;
    private bool mIsActive;

    private void Start()
    {
        mHandRole = GetComponent<ViveRoleSetter>().viveRole;

        if (Lightning)
        {
            LightningInstance = Instantiate(Lightning, transform);

            mLightningScript = LightningInstance.GetComponent<LightningBoltScript>();
        }
    }

    public void Activate()
    {
        mIsActive = true;
    }

    public void Deactivate()
    {
        mIsActive = false;
    }

    public void FixedUpdate()
    {
        bool isActive = false;

        if (LightningInstance && mIsActive)
        {
            if (ViveInput.GetPress(mHandRole, ControllerButton.Trigger))
            {
                isActive = true;
            }
        }

        LightningInstance.SetActive(isActive);
    }
}
