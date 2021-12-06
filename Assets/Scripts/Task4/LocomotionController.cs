using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR.Interaction.Toolkit; 

public class LocomotionController : MonoBehaviour
{

    public ActionBasedController leftRay;
    
    public float activationThreshold = 0.1f;

    // Update is called once per frame
    void Update()
    {
        if(leftRay)
        {
            leftRay.gameObject.SetActive(CheckIfActivated(leftRay));
        }
    }
    private bool CheckIfActivated(ActionBasedController controller)
    {
        float result = controller.activateAction.action.ReadValue<float>();
        return (result > activationThreshold);
    }
}
