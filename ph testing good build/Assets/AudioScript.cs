using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScript : MonoBehaviour
{
    [SerializeField] List<AudioClip> phNarration;
    [SerializeField] AudioSource mainAudioSource;
    public static bool completedActionEarly;
    public static bool proceedToNextClip;
    [SerializeField] public static bool actionCompleted;
    [SerializeField] public static int audioClipNumber;
    [SerializeField] List<GameObject> gameObjectsToHighlight;
    [SerializeField] List<GameObject> gameObjectsToAppear;
    [SerializeField] GameObject AcidicOutline;
    [SerializeField] GameObject BasicOutline;
    [SerializeField] int clipNumber;
    Outline goOutline;
    // Start is called before the first frame update
    void Start()
    {
        audioClipNumber = -1;
      
        actionCompleted = true;
        
    }

    // Update is called once per frame
    void Update()
    {  
        if(!mainAudioSource.isPlaying)
        {
            Debug.Log("GROUNDHOG DAY");
        }


        if (audioClipNumber >= 2)
        {
            beakerScript.PartOneComplete = true;
        }

        clipNumber = audioClipNumber;
        Debug.Log(mainAudioSource.time + " " + "KENNY");
      
        Debug.Log("MINDY" + beakerScript.PartOneComplete);

        if(actionCompleted)
        {
            audioClipNumber++;
            StartCoroutine("StartNextAudio");
            actionCompleted = false;
        }

        if (audioClipNumber == 0)
        {

            if (mainAudioSource.time >= 12)
            {
                itemAppear(gameObjectsToAppear[0]);
            }

            if (mainAudioSource.time >= 14)
            {
                itemAppear(gameObjectsToAppear[1]);
                itemDisappear(gameObjectsToAppear[0]);
            }

            if (mainAudioSource.time >= 22)
            {
                itemAppear(gameObjectsToAppear[2]);
                itemAppear(gameObjectsToAppear[3]);
                itemDisappear(gameObjectsToAppear[1]);
            }

            if (mainAudioSource.time >= 26)
            {
                itemAppear(gameObjectsToAppear[4]);
                itemAppear(gameObjectsToAppear[5]);
            }

            if (mainAudioSource.time >= 34)
            {
                itemAppear(gameObjectsToAppear[6]);

                itemDisappear(gameObjectsToAppear[3]);
                itemDisappear(gameObjectsToAppear[4]);
                // itemDisappear(gameObjectsToAppear[5]);
            }

            if (mainAudioSource.time >= 37)
            {
                itemAppear(gameObjectsToAppear[7]);
            }

            if (mainAudioSource.time >= 40)
            {
                itemAppear(gameObjectsToAppear[8]);
            }

            if (mainAudioSource.time >= 43)
            {
                itemAppear(gameObjectsToAppear[9]);
            }

            if (mainAudioSource.time >= 46)
            {
                itemAppear(gameObjectsToAppear[10]);   // ORANGE
            }

            if (mainAudioSource.time >= 48.5)
            {
                itemAppear(gameObjectsToAppear[11]); // MILK
            }

            if (mainAudioSource.time >= 53.5)
            {
                itemAppear(gameObjectsToAppear[12]); // SOAP
            }

            if (mainAudioSource.time >= 56)
            {
                itemAppear(gameObjectsToAppear[13]);  // BLEACH
            }

        }

        if(audioClipNumber == 1)
        {
            itemDisappear(gameObjectsToAppear[6]);
            itemDisappear(gameObjectsToAppear[7]);
            itemDisappear(gameObjectsToAppear[8]);
            itemDisappear(gameObjectsToAppear[9]);
            itemDisappear(gameObjectsToAppear[10]);
            itemDisappear(gameObjectsToAppear[11]);
            itemDisappear(gameObjectsToAppear[12]);
            itemDisappear(gameObjectsToAppear[13]);



           if(mainAudioSource.time >=7.5f)
            {
                itemAppear(gameObjectsToAppear[14]);
            }

           if (mainAudioSource.time >=15)
            {
                itemAppear(gameObjectsToAppear[15]);
            }

           if(mainAudioSource.time >=19)
            {
                itemAppear(gameObjectsToAppear[16]);
            }

           if(mainAudioSource.time >= 32.5f)
            {
                itemAppear(gameObjectsToAppear[17]);
            }

        }

        
    }

    IEnumerator StartNextAudio()
    {
        Debug.Log("MEATCANYON");
        mainAudioSource.clip = phNarration[audioClipNumber];
        mainAudioSource.Play();
        yield return new WaitForSeconds(mainAudioSource.clip.length);
       // audioClipNumber++;

       
        if(audioClipNumber < 2)
        {
            actionCompleted = true;

            //Makes sure that audio is playing for the first two clips that the user isn't doing anything. After the first two have played, when the audio plays next depends on how fast the user takes to complete an action.
        }

        if (AudioScript.audioClipNumber >= 3 && AudioScript.audioClipNumber <4 && beakerScript.PartTwoComplete)
        {
            AudioScript.actionCompleted = true;
        }

    }

    void itemAppear(GameObject GOToAppear)
    {
        
            GOToAppear.SetActive(true);
          
        
    }

    void itemDisappear(GameObject GOToDisappear)
    {
        
            GOToDisappear.SetActive(false);
        
    }
}
