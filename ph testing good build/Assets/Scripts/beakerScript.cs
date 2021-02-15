using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;

public class beakerScript : MonoBehaviour
{
    [SerializeField] int hitCount;
    [SerializeField] string nameOfFirstCollision;
    [SerializeField] Collider thisGameObjectCollider;
    [SerializeField] Transform fluidGameObject;
    [SerializeField] Material fluidGameObjectMat;
    [SerializeField] Material firstCollisionMat;
    [SerializeField] lidScript scriptToCheckPouringSolution;
    Rigidbody gameObjectRB;
    XRGrabInteractable grabScript;

    GameObject probe;

    [SerializeField] XRController LeftHand;
    [SerializeField] XRController RightHand;
    [SerializeField] InputDevice RDevice;
    [SerializeField] InputDevice LDevice;
    [SerializeField] AudioSource probeConfirmReading;
    [SerializeField]AudioSource liquidPour;
    

    bool buttonValue;

    public static bool InputReceivedL;
    public static bool InputReceivedR;

    public static bool processCompleteCleaning;
    public static bool processCompleteReading;

    

    bool fuckUnity; // THIS IS ALWAYS TRUE


    public static int beakerCount;

    bool cleaningProbe;

    bool beakerFilled;

    int cleanCount;

    public static bool PartOneComplete { get { return partOneComplete; } set { partOneComplete = value; } }
    public static bool PartTwoComplete { get { return partTwoComplete; } set { partTwoComplete = value; } }
    public static bool PartThreeComplete { get { return partThreeComplete; } set { partThreeComplete = value; } }


    static int beakerPourCount;
    public static int BeakerPourCount { get { return beakerPourCount; } set { beakerPourCount = value; } }

    static bool partOneComplete;
    static bool partTwoComplete;
    static bool partThreeComplete;

    string nameOfBeaker;

    static bool getReading;

    public static bool GetReading { get { return getReading; } set { getReading = value; } }

    public string NameOfBeaker { get { return nameOfBeaker; } set { nameOfBeaker = value; } }



    bool canMove;

    public bool CanMove { get { return canMove; } set { canMove = value; } }

    string nameOfCleaningBeaker;

    bool handOpenR;
    bool handOpenL;

    bool beakerTimerStart;
    float beakerResetTime;
    float solutionBeakerResetTime;

    bool solutionBeakerTimerStart;

    Vector3 beakerPosGlobal;

    Vector3 beakerPos;

    Vector3 gameObjectPos;

    Quaternion gameObjRot;

    public static bool takingReading;

     bool underProbe;

     bool hasBeaker;

    public static bool cleanBeakerUnderProbe;

    bool beenRead;

    [SerializeField] GameObject cleaningBeakerFluidGO;

    float fluidBeakerLevel;

    bool beakerReferenceEvent;

   static bool getFluidLevel;

    public static float liquidCleanerLevel;

    [SerializeField] Collider GOCollider;


    static int readingCount;

    static bool beakerPosReset;

    static bool CBPosReset;
    

    // Start is called before the first frame update
    void Start()
    {
        readingCount = 0;
        liquidPour = gameObject.GetComponent<AudioSource>();


     //   PartOneComplete = false;
        PartTwoComplete = false;
        beakerCount = 0;
        beakerPourCount = 0;
        CanMove = true;
        hitCount = 0;
        thisGameObjectCollider = gameObject.GetComponentInChildren<Collider>();
        fluidGameObjectMat = gameObject.GetComponentInChildren<MeshRenderer>().material;
        fluidGameObjectMat.renderQueue = 4000;
       
        beakerPosGlobal = new Vector3(-0.3f, -0.2f, 0.1f);
        beakerPos = gameObject.transform.position;
        probe = GameObject.Find("Probe");


        RDevice = InputDevices.GetDeviceAtXRNode(RightHand.controllerNode);
        LDevice = InputDevices.GetDeviceAtXRNode(LeftHand.controllerNode);

        takingReading = false;

        partTwoComplete = false;

        cleanCount = 0;

        beakerResetTime = 0;
        beakerTimerStart = false;

        gameObjectPos = gameObject.transform.position;

        gameObjRot = gameObject.transform.rotation;
        processCompleteCleaning = false;

        hasBeaker = false;

        solutionBeakerTimerStart = false;

        beakerReferenceEvent = true;

        getFluidLevel = false;

        fluidBeakerLevel = 0;


       
        

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("GRIFFITH" + " " + partTwoComplete + " " + probeScript.probeCleaned + " " + AudioScript.actionCompleted + " " + beakerCount + " " + buttonPressPhMeter.textOff);
      
        if(beakerPourCount ==3 && !buttonPressPhMeter.textOff && AudioScript.audioClipNumber <=3)
        {  
            if(!partTwoComplete)
            {
                AudioScript.actionCompleted = true;
            }

            partTwoComplete = true;
            probeScript.probeCleaned = false;
           
        }

       if(beakerPosReset && gameObject.name != nameOfCleaningBeaker)
        {
            grabScript = gameObject.GetComponent<XRGrabInteractable>();
            grabScript.interactionLayerMask = 0;
            gameObject.transform.position = gameObjectPos;
            gameObject.transform.rotation = gameObjRot;

        }

       if(CBPosReset && gameObject.name == nameOfCleaningBeaker)
        {
            grabScript = gameObject.GetComponent<XRGrabInteractable>();
            grabScript.interactionLayerMask = 0;
            gameObject.transform.position = gameObjectPos;
            gameObject.transform.rotation = gameObjRot;
        }
        
        Debug.Log(PartThreeComplete + " " + "OAK");

       if(gameObject.name == nameOfCleaningBeaker)
        {
            Debug.Log("NOICE" + " " + GetReading + "  " + probeScript.probeCleaned + " " + canMove + " " + "NEO" + " " + underProbe);
        }
    
        if(readingCount == 3)
        {
            PartThreeComplete = true;
        }

        if (beakerReferenceEvent)
        {
            if(gameObject.name == nameOfCleaningBeaker)
            {
                beakerReferenceEvent = false;
                fluidBeakerLevel = cleaningBeakerFluidGO.transform.localScale.z;
                squeezeBottleScript.cleaningBeakerFluid = cleaningBeakerFluidGO;
                // This event is used to get a proper reference to the cleaningBeaker fluid child object. The problem with trying to reference this elsewhere is that there isn't any place I can reference it without the reference being reset to 0. So I had to make this event just to get the reference properly once. Once this reference is obtained, cleaning fluid can rise in the cleaning beaker.
            }
        }
        Debug.Log("Fluid Beaker Level" + " " + fluidBeakerLevel);


        if (beakerTimerStart)
        {
            beakerResetTime += Time.deltaTime;
            if(beakerResetTime >=1.5f)
            {
                  beakerTimerStart = false;
                    beakerResetTime = 0;
                    GetReading = true; 
                    probeScript.probeCleaned = true;
                    CanMove = false;
                    grabScript = gameObject.GetComponent<XRGrabInteractable>();
                    grabScript.interactionLayerMask = 0;
                    gameObject.transform.position = gameObjectPos;
                    gameObject.transform.rotation = gameObjRot;
                    processCompleteCleaning = false;
                CBPosReset = true;
                

                    // Used for when the user is grabbing the cleaning beaker from the probe.  After the user has held the cleaning beaker for 1.5 seconds, the position of the cleaning beaker is reset to its original starting position/rotation. Since GetReading is true the cleaning beaker can't move or rotate and has the same position/rotation that it did at the start of the scenario

                

              
            }
        }

        if(solutionBeakerTimerStart)
        {
            solutionBeakerResetTime += Time.deltaTime;
           
            
            if (solutionBeakerResetTime >= 1.5f)
            {
                
                    solutionBeakerTimerStart = false;
                    readingCount++;
                    solutionBeakerResetTime = 0;
                    probeScript.probeCleaned = false;
                    GetReading = false;
                    CanMove = false;
                    grabScript = gameObject.GetComponent<XRGrabInteractable>();
                    grabScript.interactionLayerMask = 0;
                    gameObject.transform.position = gameObjectPos;
                    gameObject.transform.rotation = gameObjRot;
                beakerCount = 0;
                beakerPosReset = true;
                CBPosReset = false;
                

            }
        }





        /// START OF INPUT CODE//////

        Debug.Log("ALADDIN" + " " + probeScript.probeCleaned);

        RDevice = InputDevices.GetDeviceAtXRNode(RightHand.controllerNode);
        LDevice = InputDevices.GetDeviceAtXRNode(LeftHand.controllerNode);
        if (RDevice.TryGetFeatureValue(CommonUsages.triggerButton, out buttonValue) && buttonValue)
        {
            Debug.Log("ATILLA");
            Debug.Log(gameObject.name + " " + "HOMER");

           
                InputReceivedR = true;
                Debug.Log("DONKEY");
            
        }

        else
        {
            InputReceivedR = false;
            handOpenR = true;  // This if/else statement checks to see if the user has their hand open during the time they are grabbing an item, if they are and use the grab button they will grab the beaker, otherwise it will remain still.
        }


        if (LDevice.TryGetFeatureValue(CommonUsages.triggerButton, out buttonValue) && buttonValue)
        {
            
                InputReceivedL = true;
                Debug.Log("SHREK");
            


        }

        else { InputReceivedL = false; handOpenL = true; }


        ///////////// HAND INPUT SECTION///////////////////


        if (partTwoComplete && !buttonPressPhMeter.textOff)
        {
            grabScript = gameObject.GetComponent<XRGrabInteractable>();
            grabScript.interactionLayerMask = -1;

            if (fluidGameObject.transform.localScale.z < 1.40)
            {

                Transform goParentTransform = fluidGameObject.transform.parent;
                GameObject goParent = goParentTransform.gameObject;

                goParent.name = "Beaker For Cleaning Solution";


                nameOfCleaningBeaker = goParent.name;




                Debug.Log("ZOO");

            }
        }

        if (!PartTwoComplete)
        {
            gameObjectRB = gameObject.GetComponent<Rigidbody>();
            gameObjectRB.constraints = RigidbodyConstraints.FreezeAll;
            if(gameObject.GetComponent<XRGrabInteractable>()!=null)
            {
                grabScript = gameObject.GetComponent<XRGrabInteractable>();
                grabScript.interactionLayerMask = 0;  // If part two of tutorial isn't complete, you can't pick up any of the beakers.

              
            }

          
        }

        if(PartThreeComplete && gameObject.name != nameOfCleaningBeaker)
        {
            gameObjectRB = gameObject.GetComponent<Rigidbody>();
            gameObjectRB.constraints = RigidbodyConstraints.FreezeAll;
            if (gameObject.GetComponent<XRGrabInteractable>() != null)
            {
                grabScript = gameObject.GetComponent<XRGrabInteractable>();
                grabScript.interactionLayerMask = 0;  // If part two of tutorial isn't complete, you can't pick up any of the beakers.


            }

            else { grabScript.interactionLayerMask = 0; }

        }

        if (CanMove)
        {
            gameObjectRB = gameObject.GetComponent<Rigidbody>();
            gameObjectRB.constraints = RigidbodyConstraints.None;

            if (gameObject.GetComponent<XRGrabInteractable>() == null)
            {
                grabScript = gameObject.AddComponent<XRGrabInteractable>();
                grabScript.interactionLayerMask = -1;
            }

            else
            {
                grabScript = gameObject.GetComponent<XRGrabInteractable>();
                grabScript.interactionLayerMask = -1;
            }

        }

        else if (CanMove == false)
        {
            gameObjectRB = gameObject.GetComponent<Rigidbody>();
            gameObjectRB.constraints = RigidbodyConstraints.FreezeAll;
            gameObject.transform.rotation = gameObjRot;

            if (gameObject.GetComponent<XRGrabInteractable>() == null)
            {
                grabScript = gameObject.AddComponent<XRGrabInteractable>();
                grabScript.interactionLayerMask = 0;
            }

            else
            {
                grabScript = gameObject.GetComponent<XRGrabInteractable>();
                grabScript.interactionLayerMask = 0;
            }


        }

        

            if (gameObject.name == nameOfCleaningBeaker && !probeScript.probeCleaned && !GetReading)
            {
                CanMove = true;  // If probe isn't cleaned and you aren't getting the PH reading and probe isn't underneath the probe, you can move it.
            }

            if (gameObject.name == nameOfCleaningBeaker && !probeScript.probeCleaned && !GetReading && underProbe)
            {
                CanMove = false;  // If Beaker is under probe it can't be moved
            }

            if (gameObject.name == nameOfCleaningBeaker && probeScript.probeCleaned && GetReading)
            {
                CanMove = false;  // If gameObject is the cleaning beaker and the probe has been cleaned and you are ready to get a reading, you can't move cleaning beaker.
            }

        if (!partThreeComplete)
        {

            if (gameObject.name != nameOfCleaningBeaker && probeScript.probeCleaned && GetReading && !underProbe)
            {
                CanMove = true;  // If beaker is not the cleaning beaker and the probe is clean and you are ready to get the reading and it isn't under the probe, you can move this beaker.
            }

            if (gameObject.name != nameOfCleaningBeaker && probeScript.probeCleaned && GetReading && underProbe)
            {
                CanMove = false;
            }
            if (gameObject.name != nameOfCleaningBeaker && !probeScript.probeCleaned && !GetReading)
            {
                CanMove = false;
            }


        }

        else if(PartThreeComplete && gameObject.name != nameOfCleaningBeaker && GetReading && probeScript.probeCleaned) { canMove = false; }

        if(probeScript.fillCleaningBeaker)
        {
            if (gameObject.name == nameOfCleaningBeaker)
            {
                fluidBeakerLevel = fluidBeakerLevel + (.20f * Time.deltaTime);

                cleaningBeakerFluidGO.transform.localScale = new Vector3(cleaningBeakerFluidGO.transform.localScale.x, cleaningBeakerFluidGO.transform.localScale.y, fluidBeakerLevel);

            }


        }

        if(!probeScript.fillCleaningBeaker)
        {
            
        }


      
        }

        private void OnParticleCollision(GameObject other)  /////// PARTICLE COLLISION SECTION//////////////
    {
     

        Collider otherGameCollider = other.GetComponent<Collider>();
        scriptToCheckPouringSolution = other.GetComponent<lidScript>();
        if (hitCount == 0)
        {
            if (scriptToCheckPouringSolution.canPour)
            {  // This code makes sure that the user can only pour each solution once into only one beaker. The code prevents the user from pouring say a solution 7 into more than one beaker, allowing for each solution to be tested.

                scriptToCheckPouringSolution.canPour = false;
                hitCount = 1;
                nameOfFirstCollision = other.name;

              

                firstCollisionMat = other.GetComponent<ParticleSystemRenderer>().material;
            }
        }




        this.gameObject.name = nameOfFirstCollision + "Beaker";

        NameOfBeaker = gameObject.name;

        if (other.name != nameOfFirstCollision)
        {
            Physics.IgnoreCollision(thisGameObjectCollider, otherGameCollider, true);

            // This code prevents the user from pouring another substance into a beaker already poured. So say that you pour a Ph7 solution into a beaker, this code prevents you from pouring a ph4 solution into the same beaker and mixing the chemicals.
        }

        if (other.name == nameOfFirstCollision)
        {
            if (fluidGameObject.localScale.z < 1.44)
            {
                fluidGameObject.localScale = new Vector3(fluidGameObject.localScale.x, fluidGameObject.localScale.y, fluidGameObject.localScale.z + .025f);

                fluidGameObjectMat.color = firstCollisionMat.color;

                //This section allows the user to pour the solution into the beaker if they already poured this solution in already. For example, if the user poured in a solution of Ph4 and stopped/paused, they could continue and pour the solution in again later.

                if (fluidGameObject.localScale.z >= 1.43)
                {
                    beakerFilled = true;
                    beakerPourCount++;
                    beakerFilled = false;

                    if (beakerPourCount == 3)
                    {
                        //partTwoComplete = true;
                        //probeScript.probeCleaned = false;   //////////// GO BACK TO LATER ON!!!!!!!!!!!
                        
                       
                        
                       /* if (AudioScript.audioClipNumber >=4 && AudioScript.audioClipNumber < 6)
                        {
                            AudioScript.actionCompleted = true;
                        } */

                    }
                }

            }
        }

      

    }

    /// END OF PARTICLE COLLISION EVENTS//////////////////////
    /// 

    private void OnTriggerEnter(Collider other)
    {
       if(gameObject.name != nameOfCleaningBeaker && other.gameObject.name == "Probe" && beakerCount ==0 && !beenRead)
        {
            if (beakerCount == 0)
            {
               
                beakerCount = 1;
                hasBeaker = true;

            }
            
        }

        if (gameObject.name == nameOfCleaningBeaker && other.gameObject.name == "Probe")
        {
            getFluidLevel = true;
        }

    }

    private void OnTriggerStay(Collider other)
    {////////////////////// BEGIN HERE/////////////////
        ///
        if (gameObject.name != nameOfCleaningBeaker && other.gameObject.name == "Probe" && hasBeaker)
        {
            
            if (!processCompleteReading && hasBeaker && !partThreeComplete)

            {  
                
              
                underProbe = true;

                gameObject.transform.position = probe.transform.position;
                gameObject.transform.rotation = gameObjRot;
               
                
                if(probeConfirmReading.isPlaying)
                {
                    probeConfirmReading.loop = true;
                }


            }


        }

        // THESE BOTTOM TWO ARE FOR WHEN YOU NEED TO GRAB THE BEAKER AFTER THE READING HAS FINISHED
        if (gameObject.name != nameOfCleaningBeaker && other.gameObject.name == "LeftHand" && probeScript.probeCleaned && GetReading && !partThreeComplete)
        {
            if (handOpenL)
            {
                grabScript = gameObject.GetComponent<XRGrabInteractable>();
                grabScript.interactionLayerMask = -1;

                if (InputReceivedL && processCompleteReading && other.gameObject.name == "LeftHand")
                {
                    if (underProbe)
                    {
                        
                        underProbe = false;
                        solutionBeakerTimerStart = true;
                        if(probeConfirmReading.isPlaying)
                        {
                            probeConfirmReading.Stop();
                        }
                       
                       
                        hasBeaker = false;
                        gameObject.tag = "beenRead";
                        beenRead = true;
                    }
                }
            }
        }

        if (gameObject.name != nameOfCleaningBeaker && other.gameObject.name == "RightHand" && probeScript.probeCleaned && GetReading && !partThreeComplete)
        {

            if (handOpenR)
            {
                grabScript = gameObject.GetComponent<XRGrabInteractable>();
                grabScript.interactionLayerMask = -1;

                Debug.Log("TEST1");

                if (InputReceivedR && processCompleteReading && other.gameObject.name == "RightHand")
                { 
                    if (underProbe)
                    {
                        Debug.Log("TEST2");
                        underProbe = false;
                        solutionBeakerTimerStart = true;
                        

                        if(probeConfirmReading.isPlaying)
                        {
                            probeConfirmReading.Stop();
                        }
                        
                        hasBeaker = false;
                        gameObject.tag = "beenRead";
                        beenRead = true;

                    }
                }
            }
        }




        if (gameObject.name == nameOfCleaningBeaker && other.gameObject.name == "Probe")
        {
            if (!processCompleteCleaning)
            {
                underProbe = true;
                GOCollider.enabled = false;

                if (AudioScript.audioClipNumber < 5)
                {
                    AudioScript.audioClipNumber = 4;
                    AudioScript.actionCompleted = true;
                }
                
             

               
                if (getFluidLevel)
                {
                    fluidBeakerLevel = cleaningBeakerFluidGO.transform.localScale.z;
                    liquidCleanerLevel = fluidBeakerLevel + .40f;
                    getFluidLevel = false;

                }


                gameObject.transform.position = probe.transform.position;
                gameObject.transform.rotation = gameObjRot;
                cleanBeakerUnderProbe = true;

            }
        }

     
        if (gameObject.name == nameOfCleaningBeaker && other.gameObject.name == "LeftHand" && !probeScript.probeCleaned && !GetReading) 
            //&& !partThreeComplete)
        {
            if (handOpenL)
            {
                GOCollider.enabled = true;
                grabScript = gameObject.GetComponent<XRGrabInteractable>();
                grabScript.interactionLayerMask = -1;

                if (InputReceivedL && processCompleteCleaning)
                { if (underProbe)
                    {
                        beakerPosReset = false;
                        underProbe = false;
                        beakerTimerStart = true;
                        cleanBeakerUnderProbe = false;
                    }
                }
            }
        }

        if (gameObject.name == nameOfCleaningBeaker && other.gameObject.name == "RightHand" && !probeScript.probeCleaned && !GetReading)
            //&& !partThreeComplete)  // FIXES ISSUE WITH CLEANING BEAKER MOVING AWAY WHEN GRABBING HOWEVER I STILL NEED TO MAKE SURE IT WILL BE USED TO CLEAN PROBE ONE MORE TIME AFTER
        {

            if (handOpenR)
            {
                GOCollider.enabled = true;
                grabScript = gameObject.GetComponent<XRGrabInteractable>();
                grabScript.interactionLayerMask = -1;

                Debug.Log("TEST1");

                if (InputReceivedR && processCompleteCleaning)
                { 
                    if (underProbe)
                    {
                        beakerPosReset = false;
                        Debug.Log("TEST2");
                        underProbe = false;
                        beakerTimerStart = true;
                        cleanBeakerUnderProbe = false;

                    }
                }
            } 
        }

        

    }

    private void OnTriggerExit(Collider other)  
    {
     
      
    }

  
}
