using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class buttonPressPhMeter : MonoBehaviour
{
    [SerializeField] AudioSource phButtonAudio;
    bool firstTimePressed;
   [SerializeField] Animation buttonPushDown;
    [SerializeField] TextMeshPro phMeterTxt;
    bool phTextAlphaStart;
    [SerializeField]float phAlphaTxt;
    [SerializeField]Color phMeterTxtColor;
    float loadTime;
    int dotCount;
    string origPhMeterText;
    bool continueToLoad;
    int passCount;
    public static bool textOff;
   
    // Start is called before the first frame update
    void Start()
    {
        firstTimePressed = true;
        phAlphaTxt = phMeterTxt.color.a;
        phMeterTxtColor = phMeterTxt.color;
        loadTime = 0;
        dotCount = 0;
        passCount = 0;
        textOff = true;
       
        

        
        
    }

    // Update is called once per frame
    void Update()
    {  if(!firstTimePressed)
        {
            phMeterTxt.fontSize = .22f;
        }
        Debug.Log("GUTS" + " " + textOff);
        

        phMeterTxtColor.a = phAlphaTxt;

        phMeterTxt.color = phMeterTxtColor;


        if (phTextAlphaStart)
        {
            phAlphaTxt += (.001f + (1 *Time.deltaTime));
            

            if(phAlphaTxt >=1)
            {
                phTextAlphaStart = false;
                phAlphaTxt = 1;
            }
        }
        
        
        if (continueToLoad)
        {
            loadTime += Time.deltaTime;

        }

        if(loadTime >=1f && passCount <3)
        {
            phMeterTxt.text = phMeterTxt.text + "*" + "*";
            dotCount++;
            loadTime = 0;
        }

        if(dotCount ==4)
        {
                phMeterTxt.text = origPhMeterText;
            passCount++;
                dotCount = 0;

        }

        if(passCount == 3)
        {  if (textOff == true)
            {
                phMeterTxt.text = "";
            }

            textOff = false;
        }
        
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.name == "PowerButton")
        {
            if (firstTimePressed)
            {
                buttonPushDown.Play();
                phButtonAudio.Play();
                phMeterTxt.text = "Powering" + "\n" + "On" + "\n";
                origPhMeterText = phMeterTxt.text;
                phMeterTxt.fontSize = .18f;
                Debug.Log("CLOVER");
                phTextAlphaStart = true;
                firstTimePressed = false;
                continueToLoad = true;
               
            }
        }
    }

    

  
}
