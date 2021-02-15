using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectResetPos : MonoBehaviour
{
    [SerializeField]Vector3 origPos;
    [SerializeField]float waitTime = 0;
    [SerializeField] Rigidbody objectRBody;
   [SerializeField] Quaternion objRot;
   [SerializeField] bool resetCountdown;

    // Start is called before the first frame update
    void Start()
    {
        objectRBody = gameObject.GetComponent<Rigidbody>();
        objectRBody.useGravity = true;
        origPos = this.gameObject.transform.position;
        objRot = this.gameObject.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {


        objectRBody = gameObject.GetComponent<Rigidbody>();

        waitTime += Time.deltaTime;

        if (waitTime >= 4)
        {
            waitTime = 0;
        }



    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bounds")
        {
            Debug.Log("BOWMAN");

            if (!resetCountdown)
            {
                waitTime = 0;
                resetCountdown = true;
            }


              if(waitTime > 3)
              {
                  objectRBody.freezeRotation = true;
                  this.gameObject.transform.rotation = objRot;
                  this.gameObject.transform.position = origPos;
              } 
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Bounds")
        {
            Debug.Log("Mason");
            if (waitTime > 3)
            {
                //this.gameObject.SetActive(false);
                objectRBody.freezeRotation = true;
                objectRBody.useGravity = false;
                objectRBody.velocity = new Vector3(0, 0, 0);
                this.gameObject.transform.position = origPos;
                this.gameObject.transform.rotation = objRot;
                StartCoroutine("WaitToReset");

                if (gameObject.transform.position == origPos && gameObject.transform.rotation == objRot)
                {
                    objectRBody.freezeRotation = false;
                    objectRBody.useGravity = true;
                    this.gameObject.SetActive(true);
                }

            }
        }
    }

    IEnumerator WaitToReset()
    {
        resetCountdown = false;
        yield return new WaitForSeconds(.5f);
    }
}
