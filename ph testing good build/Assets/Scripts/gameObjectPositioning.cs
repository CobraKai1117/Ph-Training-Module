using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class gameObjectPositioning : MonoBehaviour
{
    [SerializeField] List<Transform> ContainerPos;
    [SerializeField] List<Transform> phLids;
    [SerializeField] List<Transform> phBeakerTransforms;
    [SerializeField] List<GameObject> InteractableObjects;
    [SerializeField] List<GameObject> InteractableObjectsA;
    [SerializeField] Canvas PauseMenu;
    [SerializeField] Light LHandLight;
    [SerializeField] Vector3 ph10Pos;
    [SerializeField] Vector3 ph7Pos;
    [SerializeField] Vector3 ph4Pos;
    
    [SerializeField] GameObject LHand;
    [SerializeField] GameObject RHand;
    [SerializeField] GameObject LController;
   
    // Start is called before the first frame update
    void Start()
    {
        LController.SetActive(false);
       
        LHandLight.gameObject.SetActive(false);

        
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("TIME " + Time.timeScale);
        if (Input.GetKeyDown(KeyCode.R))
        {
           // InteractableObjects[0].transform.position = ContainerPos[0].transform.position;
        }

        /*if(Input.GetKeyDown(KeyCode.T))
        { if (Time.timeScale == 1)
            {
                Time.timeScale = 0;
                //Time.fixedDeltaTime = 0;
                PauseMenu.gameObject.SetActive(true);
                LHandLight.gameObject.SetActive(true);
              
                LHand.SetActive(false);
                RHand.SetActive(false);
                LController.gameObject.SetActive(true);
                
            }

            else { Time.timeScale = 1;  PauseMenu.gameObject.SetActive(false); LHandLight.gameObject.SetActive(false); LHand.gameObject.SetActive(true); RHand.gameObject.SetActive(true); LController.gameObject.SetActive(false); }
        }

       // else { Time.timeScale = 1; } */

        void SavePosition()
        {
            BeakerPositions phPositions = new BeakerPositions();
            phPositions.Ph10BottleTransform = ph10Pos;
            phPositions.Ph7BottleTransform = ph7Pos;
            phPositions.Ph4BottleTransform = ph4Pos;

        }
    }
}
