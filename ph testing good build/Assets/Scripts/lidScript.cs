using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class lidScript : MonoBehaviour
{
    int objChildCount;
    public bool canPour;
    [SerializeField] ParticleSystem containerParticleSystem;
    ParticleSystem.ShapeModule liquidShapeSettings;
    ParticleSystem.ForceOverLifetimeModule liquidGravity;
    ParticleSystemShapeType liquidShape;
    ParticleSystem.EmissionModule liquidEmissionSettings = new ParticleSystem.EmissionModule();
    ParticleSystem.MainModule liquidParticleSettings = new ParticleSystem.MainModule();
    [SerializeField] ParticleSystemRenderer liquidRenderer;
    [SerializeField] ParticleSystem.CollisionModule particleCollisions;
    RaycastHit liquidPourPoint;
    [SerializeField] float gameObjectTransformY;
    float test;
    [SerializeField] Quaternion startRot;
    [SerializeField] Quaternion currentRot;
    [SerializeField] float rotDiffY;
    [SerializeField] float rotDiffX;
    [SerializeField] float StartRotY;
    [SerializeField] float currentRotY;
    [SerializeField] Material phSolutionColor;
    [SerializeField] LayerMask testing;

    [SerializeField] GameObject ESSSolution;

    [SerializeField] Vector3 gameObjectOGPos;
    [SerializeField] Vector3 gameObjectRot;
    Vector3 gameObjectOGRot;

    [SerializeField] Vector3 capOGPos;
    [SerializeField] Vector3 capRot;
    [SerializeField] Vector3 OGCapRot;

    [SerializeField] GameObject beakerCap;

    XRGrabInteractable lidGrabScript;
    XRGrabInteractable goGrabScript;
    Rigidbody lidRigidbody;
    Rigidbody goRigidbody;

    public bool liquidCanBePoured;

    AudioSource beakerAudio;

    int layerIndex;


    Quaternion goOrigRot;
    Quaternion capQRot;
    [SerializeField] GameObject particleEmissionParent;

    
    [SerializeField] string nameOfBeaker;


    bool getBeakerName;

    [SerializeField] Transform beakerFluidLevel;

    bool playAudio;


    // Start is called before the first frame update
    void Start()
    {
        getBeakerName = false;
        layerIndex = LayerMask.NameToLayer("Default");
        string test = LayerMask.LayerToName(8);
        Debug.Log("MOIRA" + test);
        testing.value = 1 << 8;
        canPour = true;

        lidRigidbody = beakerCap.GetComponent<Rigidbody>();
        goRigidbody = particleEmissionParent.GetComponent<Rigidbody>();
        goGrabScript = particleEmissionParent.GetComponent<XRGrabInteractable>();
        lidGrabScript = beakerCap.GetComponent<XRGrabInteractable>();

        OGCapRot = beakerCap.transform.localRotation.eulerAngles;


        gameObjectOGRot = particleEmissionParent.transform.rotation.eulerAngles;


        gameObjectOGPos = particleEmissionParent.transform.localPosition;
        capOGPos = beakerCap.transform.localPosition;

        capQRot = beakerCap.transform.localRotation;
        goOrigRot = particleEmissionParent.transform.rotation;



        objChildCount = particleEmissionParent.transform.childCount;

        StartRotY = gameObject.transform.localEulerAngles.y;
        startRot = particleEmissionParent.transform.localRotation;
        //gameObject.AddComponent<ParticleSystem>();


        gameObject.AddComponent<ParticleSystem>();
        containerParticleSystem = gameObject.GetComponent<ParticleSystem>();

        nameOfBeaker = "";

    }

    // Update is called once per frame
    void Update()
    {  if(beakerFluidLevel != null && beakerAudio != null)
        {

            if(beakerFluidLevel.transform.localScale.z >=1.42f)
            {
                playAudio = false;
                beakerAudio.Pause();
                //beakerAudio.Pause();
            }

         /*   else if (beakerFluidLevel.transform.localScale.z <1.42f)
            {
                playAudio = true;
                beakerAudio.Play();
            } */

           
        }

        if (beakerScript.PartTwoComplete)
        {

            lidGrabScript.interactionLayerMask = 0;

            lidRigidbody.useGravity = false;

            beakerCap.transform.localPosition = capOGPos;
            //capRot = beakerCap.transform.rotation;
            // capRot = OGCapRot;

            beakerCap.transform.localRotation = capQRot;
            particleEmissionParent.transform.rotation = goOrigRot;
            lidRigidbody.constraints = RigidbodyConstraints.FreezeAll;

            goGrabScript.interactionLayerMask = 0;
            goRigidbody.useGravity = false;
            particleEmissionParent.transform.localPosition = gameObjectOGPos;

            // gameObjectRot = gameObject.transform.rotation.eulerAngles;
            // gameObjectRot = gameObjectOGRot;
            goRigidbody.constraints = RigidbodyConstraints.FreezeAll;





        }
        // rotDiff = gameObject.GetComponent<Transform>().localEulerAngles.y;
        gameObjectTransformY = transform.localEulerAngles.y;



        currentRot = gameObject.transform.localRotation;

        currentRotY = Quaternion.Angle(startRot, currentRot);

        //rotDiff = StartRotY - gameObject.transform.localEulerAngles.y;

        test = Vector3.Angle(new Vector3(0, 0, 0), gameObject.transform.rotation.eulerAngles);


        rotDiffY = particleEmissionParent.transform.localEulerAngles.y;
        rotDiffX = particleEmissionParent.transform.localEulerAngles.x;


        //rotDiffY >= 98.7f || rotDiffY <=-98.7f ) //|| rotDiffX > 110 || rotDiffX <-101)

        if (rotDiffX > 25 && rotDiffX < 130 && gameObject != ESSSolution)
        {


            if (gameObject.GetComponent<ParticleSystem>() == null)
            {
                gameObject.AddComponent<ParticleSystem>();

            }



            containerParticleSystem = gameObject.GetComponent<ParticleSystem>();

            particleCollisions = containerParticleSystem.collision;
            particleCollisions.enabled = true;
            particleCollisions.lifetimeLoss = 1;
            particleCollisions.type = ParticleSystemCollisionType.World;
            particleCollisions.mode = ParticleSystemCollisionMode.Collision3D;
           // particleCollisions.collidesWith = -1;  //testing;

            particleCollisions.sendCollisionMessages = true;
            // containerParticleSystem = gameObject.GetComponent<ParticleSystem>();
            liquidShapeSettings = containerParticleSystem.shape;
            liquidShapeSettings.shapeType = ParticleSystemShapeType.Cone;
            liquidShapeSettings.angle = 0.18f;
            liquidShapeSettings.radius = 0.001f;
            liquidShapeSettings.radiusThickness = 1;
            liquidShapeSettings.arc = 360;
            liquidShapeSettings.position = new Vector3(0, 0, 0.11f);

            liquidEmissionSettings = containerParticleSystem.emission;
            liquidEmissionSettings.rateOverTime = 2267.12f;

            liquidParticleSettings = gameObject.GetComponent<ParticleSystem>().main;
            liquidParticleSettings.duration = 3.37f;
            liquidParticleSettings.loop = true;
            liquidParticleSettings.startLifetime = 0.16f;
            liquidParticleSettings.startSpeed = 5;
            liquidParticleSettings.startSize = 0.01f;
            liquidParticleSettings.maxParticles = 1000;
            liquidParticleSettings.gravityModifier = 7.18f;

            liquidRenderer = gameObject.GetComponent<ParticleSystemRenderer>();
            liquidRenderer.material = phSolutionColor;

            liquidGravity = gameObject.GetComponent<ParticleSystem>().forceOverLifetime;
            liquidGravity.enabled = true;
            //liquidGravity.yMultiplier = -10.63f;
            //liquidGravity.multiplier.
        }

        else
        {

            liquidParticleSettings = GetComponent<ParticleSystem>().main;
            liquidParticleSettings.maxParticles = 0;
        }




        if (beakerScript.PartTwoComplete == true)
        {
            liquidParticleSettings = gameObject.GetComponent<ParticleSystem>().main;
            liquidParticleSettings.maxParticles = 0;
        }



    }

    private void OnTriggerExit(Collider other)
    {
        if (gameObject.transform.childCount == objChildCount - 1)

            gameObject.AddComponent<XRGrabInteractable>(); //When cap is removed from beaker, the child count is 1 less and this will allow the beaker to be picked up and poured into containers.

        //canPour = true;


    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.tag == "Beaker")
        { 
            Debug.Log("Hanah" + " " + other.gameObject.name);

            if (other.gameObject.name.Contains("pH") && !getBeakerName)
            {
                getBeakerName = true;
                nameOfBeaker = other.gameObject.name;
            }



            if (other.gameObject.name == nameOfBeaker)
            {
                beakerFluidLevel = other.transform.GetChild(0).transform;
                Debug.Log("TOMMY W" + " " + beakerFluidLevel.transform.localScale.z);

                beakerAudio = other.GetComponent<AudioSource>();
                if (!beakerAudio.isPlaying && beakerFluidLevel.transform.localScale.z < 1.44f)
                { 
                    beakerAudio.Play();  
                } 
            }
        }

        else { Debug.Log("JIM"); if (beakerAudio != null) { beakerAudio.Pause(); } }
            //{ if (beakerAudio.isPlaying) { beakerAudio.Pause(); } } }
            

       // else { beakerAudio.Stop(); } 

      
    }


}
