using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

public class probeScript : MonoBehaviour
{
    [SerializeField] TextMeshPro phReadingValueOnMachine;

    [SerializeField] string stringToPassToFunction;

    [SerializeField] GameObject solutionCleaner;

    public static bool fillCleaningBeaker;

    [SerializeField] AudioSource probeConfirmationReading;



    System.Random phReadingToGenerate = new System.Random();

    float firstPingPongNumber;

    public float secondPingPongNumber;

    Decimal roundNumber;

    bool startTimer;

    float time;

   

    bool startTimerForCleaning;
    float cleanTime;

    [SerializeField] public static bool probeCleaned; // this bool is used for cleaning the probe with a cleaning solution. When this is false, the user can pick up the squeeze bottle and rinse the probe.

    bool startProbeTimer;

    [SerializeField] public static GameObject cleaningBeakerFluid;

    float cleaningBeakerFluidScaleZ;

    bool reachedLevel;

    float fluidLevelComparison;


    // Start is called before the first frame update
    void Start()
    {
        time = 0;
       
        startTimerForCleaning = false;
        cleanTime = 0;

        reachedLevel = false;


    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(probeCleaned + " " + "He Fei");
        
        if(cleaningBeakerFluid!=null) { Debug.Log(cleaningBeakerFluid + " " + "Molly"); cleaningBeakerFluidScaleZ = cleaningBeakerFluid.transform.localScale.z; }

        fluidLevelComparison = beakerScript.liquidCleanerLevel;

        Debug.Log(fluidLevelComparison + "  " + "SAFETY" + " " + beakerScript.liquidCleanerLevel);
        if (startTimer)
        { // This section of code is for the ph reading machine. Rather than getting the reading right away, this gets the reading and makes it seem like the machine is calibrating the result for a few seconds before getting the result. I feel that this makes it more authentic.
            time += Time.deltaTime;

            firstPingPongNumber = Mathf.PingPong(firstPingPongNumber, secondPingPongNumber);

            //2 * Time.deltaTime / 4, secondPingPongNumber);
            float randNumber = UnityEngine.Random.Range(.05f, .60f);

            Debug.Log(randNumber + "RANDY");

            roundNumber = (decimal)firstPingPongNumber + (decimal)randNumber;

            // + (decimal)-.10f + (decimal)-Time.deltaTime;

            roundNumber = Math.Round(roundNumber, 2);
            phReadingValueOnMachine.text = roundNumber.ToString() + " " + "ph";
            phReadingValueOnMachine.fontSize = 12.7f;
          
            probeConfirmationReading.Play();

        }

        if (time > 3)
        {
            startTimer = false;

            if (AudioScript.audioClipNumber <= 6)
            {
                AudioScript.audioClipNumber = 6;
                AudioScript.actionCompleted = true;
            }

            else if (AudioScript.audioClipNumber > 6 && AudioScript.audioClipNumber <= 8)
            {
                AudioScript.audioClipNumber = 8;
                AudioScript.actionCompleted = true;

                //These if statements set the audio to its proper position. For example if the user is going through the simulation faster than the audio can keep up, these if statements reset the audio to its proper place. 

            }

            time = 0;
            beakerScript.takingReading = false;
            beakerScript.processCompleteReading = true;
            //probeCleaned = true;
            
        }

if(startTimerForCleaning)
        {
            Debug.Log("BANE");
            fillCleaningBeaker = true;
            cleanTime += Time.deltaTime;

            if(cleanTime >3)
            {
                Debug.Log("WAYNE");
             // probeCleaned = true;
                cleanTime = 0;
                beakerScript.processCompleteCleaning = true;
                startTimerForCleaning = false;
                beakerScript.processCompleteReading = false;
                fillCleaningBeaker = false;

            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {


        if (other.gameObject.name.Contains("Beaker") && probeCleaned && beakerScript.PartTwoComplete && !beakerScript.processCompleteReading && other.gameObject.tag !="beenRead")  // This if statement prevents the user from taking a ph Reading unless they have completed Part 2, pouring all the substances into the beaker and if there isn't already another beaker being read.

        {
            string nameOfBeaker = other.gameObject.name;


            Debug.Log("PATTY" + " " + nameOfBeaker);



            if (nameOfBeaker.Contains("10"))
            {
                stringToPassToFunction = GetphNumberFromString(nameOfBeaker, "10");

                ComputePhReading(stringToPassToFunction);


            }

            else if (nameOfBeaker.Contains("7"))
            {
                stringToPassToFunction = GetphNumberFromString(nameOfBeaker, "7");

                ComputePhReading(stringToPassToFunction);
            }

            else if (nameOfBeaker.Contains("4"))
            {
                stringToPassToFunction = GetphNumberFromString(nameOfBeaker, "4");

                ComputePhReading(stringToPassToFunction);
            }



        }

    }


    string GetphNumberFromString(string phNumberToGet, string phNumberGrabbed)
    {
        int substringContainingNumber = phNumberToGet.IndexOf(phNumberGrabbed, 0);

        Debug.Log(substringContainingNumber + "ROB");

        if (phNumberGrabbed.Length == 2)
        {

            string phNumber = phNumberToGet.Substring(substringContainingNumber, 2);

            Debug.Log(phNumber + "TODD");

            return phNumber;
        }

        else
        {
            string phNumber = phNumberToGet.Substring(substringContainingNumber, 1);

            Debug.Log(phNumber + "TODD");

            return phNumber;
        }
    }

    void ComputePhReading(string phStringToConvertToNumber)
    {
        int numberUsedinComputing = int.Parse(phStringToConvertToNumber);

        double phNumber = phReadingToGenerate.NextDouble();

        float phValue = (float)numberUsedinComputing;

        firstPingPongNumber = phValue;
        secondPingPongNumber = firstPingPongNumber + .70f;

        startTimer = true;

    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("TEST1" + collision.gameObject.name);
    }

    private void OnCollisionStay(Collision collision)
    {  
        Debug.Log("TEST2" + " " + collision.gameObject.name);
    }

}
