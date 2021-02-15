using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fingerScript : MonoBehaviour
{
    int buttonON;
    bool buttonPressed;
    // Start is called before the first frame update
    void Start()
    {
        buttonON = 0;
        buttonPressed = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name + "JOE");

        if (other.gameObject.tag == "Finger" && buttonPressed == false)
        {
            Debug.Log("MAO");

            buttonPressed = true;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        buttonPressed = false;
    }
}
