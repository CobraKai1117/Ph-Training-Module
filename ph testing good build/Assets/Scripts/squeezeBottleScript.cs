using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class squeezeBottleScript : MonoBehaviour
{
    [SerializeField] ParticleSystem squeezeBottleParticleSystem;
    ParticleSystem.ShapeModule squeezeBottleLiquidShapeSettings;
    ParticleSystem.ForceOverLifetimeModule liquidGravitySqueezeBottle;
    ParticleSystem.EmissionModule liquidEmissionSettingsSqueezeBottle = new ParticleSystem.EmissionModule();
    ParticleSystem.MainModule liquidParticleSettingsSqueezeBottle = new ParticleSystem.MainModule();
    [SerializeField] ParticleSystemRenderer liquidRendererSqueezeBottle;
    [SerializeField] ParticleSystem.CollisionModule squeezeBottleParticleCollision;
    ParticleSystem.TriggerModule squeezeBottleTModule;
    [SerializeField] float rotDiffX;
    [SerializeField] Material phSolutionColorSB;
    XRGrabInteractable grabSqueezeBottle;
    [SerializeField] LayerMask collisionLayerMask;
    [SerializeField] GameObject cleanerPourPoint;
    Rigidbody SqueezeBottleRB;
    Vector3 squeezeBottlePos;
    Quaternion squeezeBottleRot;
    [SerializeField] GameObject GOParent;
    [SerializeField] AudioSource probeAS;
    bool startPlaying;



    [SerializeField] public static GameObject cleaningBeakerFluid;
    float cleaningBeakerFluidScaleZ;
    bool particleCollision;

    // Start is called before the first frame update
    void Start()
    {
        squeezeBottlePos = GOParent.transform.position;
        squeezeBottleRot = GOParent.transform.rotation;
        
    }

    // Update is called once per frame
    void Update()
    {

        if (cleaningBeakerFluid != null) { Debug.Log(cleaningBeakerFluid + " " + "Molly"); cleaningBeakerFluidScaleZ = cleaningBeakerFluid.transform.localScale.z; }

        rotDiffX = gameObject.transform.eulerAngles.x;

        if(!beakerScript.processCompleteCleaning  && beakerScript.PartTwoComplete && beakerScript.cleanBeakerUnderProbe)
        {
            cleanProbe();
        }

        if (beakerScript.processCompleteCleaning)
        {
            liquidParticleSettingsSqueezeBottle = cleanerPourPoint.GetComponent<ParticleSystem>().main;
            liquidParticleSettingsSqueezeBottle.maxParticles = 0;
            grabSqueezeBottle = GOParent.GetComponent<XRGrabInteractable>();
            grabSqueezeBottle.interactionLayerMask = 0;
            SqueezeBottleRB = GOParent.GetComponent<Rigidbody>();
            SqueezeBottleRB.constraints = RigidbodyConstraints.FreezeAll;

            GOParent.transform.rotation = squeezeBottleRot;
            GOParent.transform.position = squeezeBottlePos;
            GOParent.transform.rotation = squeezeBottleRot;
        }

        if (!beakerScript.processCompleteCleaning && beakerScript.cleanBeakerUnderProbe)
        {
            if (GOParent.GetComponent<XRGrabInteractable>() == null)

            {
                grabSqueezeBottle = GOParent.AddComponent<XRGrabInteractable>();
            }

            grabSqueezeBottle = GOParent.GetComponent<XRGrabInteractable>();
            grabSqueezeBottle.interactionLayerMask = -1;
            SqueezeBottleRB = GOParent.GetComponent<Rigidbody>();
            SqueezeBottleRB.constraints = RigidbodyConstraints.None;
        }

        if(startPlaying)
        {
            probeAS.Play();
        }

        else if (!startPlaying)
        {
            probeAS.Pause();
        }

        }

    void cleanProbe()
    {
        Debug.Log("LEE");

        if (!beakerScript.processCompleteCleaning)
        {
            if (GOParent.GetComponent<XRGrabInteractable>() == null)

            {
                grabSqueezeBottle = GOParent.AddComponent<XRGrabInteractable>();
            }

            if (rotDiffX > 25f && rotDiffX < 130)
            {
                if (cleanerPourPoint.GetComponent<ParticleSystem>() == null)
                {
                    squeezeBottleParticleSystem = cleanerPourPoint.AddComponent<ParticleSystem>();
                }


                squeezeBottleParticleSystem = cleanerPourPoint.GetComponent<ParticleSystem>();

                squeezeBottleTModule = squeezeBottleParticleSystem.trigger;
               // squeezeBottleTModule.

                squeezeBottleParticleCollision = squeezeBottleParticleSystem.collision;
                squeezeBottleParticleCollision.enabled = true;
                squeezeBottleParticleCollision.lifetimeLoss = 1;
                squeezeBottleParticleCollision.type = ParticleSystemCollisionType.World;
                squeezeBottleParticleCollision.mode = ParticleSystemCollisionMode.Collision3D;
                squeezeBottleParticleCollision.collidesWith = -1;
                    
                    //1 << 9;

                squeezeBottleParticleCollision.sendCollisionMessages = true;

                squeezeBottleLiquidShapeSettings = squeezeBottleParticleSystem.shape;
                squeezeBottleLiquidShapeSettings.shapeType = ParticleSystemShapeType.Cone;
                squeezeBottleLiquidShapeSettings.angle = 0.18f;
                squeezeBottleLiquidShapeSettings.radius = 0.001f;
                squeezeBottleLiquidShapeSettings.radiusThickness = 1;
                squeezeBottleLiquidShapeSettings.arc = 360;
                squeezeBottleLiquidShapeSettings.position = new Vector3(0, 0, 0.11f);

                liquidEmissionSettingsSqueezeBottle = squeezeBottleParticleSystem.emission;
                liquidEmissionSettingsSqueezeBottle.rateOverTime = 1510.48f;

                liquidParticleSettingsSqueezeBottle = cleanerPourPoint.GetComponent<ParticleSystem>().main;
                liquidParticleSettingsSqueezeBottle.duration = 0.91f;
                liquidParticleSettingsSqueezeBottle.loop = true;
                liquidParticleSettingsSqueezeBottle.startLifetime = 0.25f;
                liquidParticleSettingsSqueezeBottle.startSpeed = 12.51f;
                liquidParticleSettingsSqueezeBottle.startSize = 0.01f;
                liquidParticleSettingsSqueezeBottle.maxParticles = 2914;
                liquidParticleSettingsSqueezeBottle.gravityModifier = 7.18f;

                liquidRendererSqueezeBottle = cleanerPourPoint.GetComponent<ParticleSystemRenderer>();
                liquidRendererSqueezeBottle.material = phSolutionColorSB;

                liquidGravitySqueezeBottle = gameObject.GetComponent<ParticleSystem>().forceOverLifetime;
                liquidGravitySqueezeBottle.enabled = true;

            }

        }

    }

    private void OnParticleCollision(GameObject other)
    {
      

    
        if(other.gameObject.name == "Probe" && cleaningBeakerFluidScaleZ < beakerScript.liquidCleanerLevel)
       
        {

            cleaningBeakerFluidScaleZ = cleaningBeakerFluidScaleZ + (.20f * Time.deltaTime);
            cleaningBeakerFluid.transform.localScale = new Vector3(cleaningBeakerFluid.transform.localScale.x, cleaningBeakerFluid.transform.localScale.y, cleaningBeakerFluidScaleZ);
            probeAS.Play();


        }

        if (cleaningBeakerFluidScaleZ >= beakerScript.liquidCleanerLevel)
        {
            beakerScript.processCompleteCleaning = true;

            if(AudioScript.audioClipNumber == 5)
            {
                AudioScript.actionCompleted = true;
            }

            else if (AudioScript.audioClipNumber > 5 && AudioScript.audioClipNumber <= 7)
            {
                AudioScript.audioClipNumber = 7;
                AudioScript.actionCompleted = true;

                // THESE TWO IF STATEMENTS ARE IF IN CASE THE USER GOES TOO FAST THE AUDIO WILL CATCH UP TO THE CURRENT SECTION THEY ARE AT. HENCE WHY I FIRST SET THE AUDIO BEFORE STATING THAT IT CAN BE PLAYED
            }

            else if (AudioScript.audioClipNumber > 7 && AudioScript.audioClipNumber <= 9)
            {
                AudioScript.audioClipNumber = 9;
                AudioScript.actionCompleted = true;
            }

            else if(AudioScript.audioClipNumber >9 && AudioScript.audioClipNumber <=10)
            {
                AudioScript.audioClipNumber = 10;
                AudioScript.actionCompleted = true;
            }

            // AudioScript.actionCompleted = true;


            //startTimerForCleaning = false;
            beakerScript.processCompleteReading = false;
        }

      
    }

  
}
