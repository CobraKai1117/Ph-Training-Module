using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using OVRTouchSample;

public class lineRendererRaycast : MonoBehaviour
{

    [SerializeField] private OVRInput.Controller controller = OVRInput.Controller.None;
    bool touchTrigger;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        OVRInput.Update();
        //touchTrigger 

        float test = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger);

        Debug.Log("TEST" + test);

      // if(touchTrigger) { Debug.Log("BORAT");}

       /* InputDevice Device =  InputDevices.GetDeviceAtXRNode(LController);
        bool triggerButton;

        Device.TryGetFeatureValue(CommonUsages.triggerButton, out triggerButton);

        if(triggerButton) { Debug.Log("MJ"); }

        if(triggerButton == false) { Debug.Log("LEBRON"); } */
        
        RaycastHit Hit;


        Vector3 menuHighlight = transform.TransformDirection(Vector3.forward);
        if(Physics.Raycast(transform.position,transform.forward,out Hit) && Hit.transform.tag == "Menu")
      
        {
            if(Hit.transform.gameObject.name == "Resume Collider Offset")
                //&& test > 0) 
            {
                Debug.Log("Yugi");
            
            }
        }

        
    }
}
