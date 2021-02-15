using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EventSystemP1 : MonoBehaviour
{
    [SerializeField] GameObject partOneGOActiveStart;
    [SerializeField] GameObject partOneGOInactiveStart;
    [SerializeField] Camera sceneBackground;
    Color testing;
    [SerializeField] List<GameObject> acidicGO;
    [SerializeField] List<GameObject> baseGO;
    [SerializeField] List<MeshRenderer> acidicMAT;
    [SerializeField] List<MeshRenderer> baseMat;
    MeshRenderer currentMR;
    [ColorUsage(true, true)] Color test3;
   
    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(testing + " " + "COLOR");
       
        if (Input.GetKeyDown(KeyCode.K))
        {

            for (int i = 0; i < acidicGO.Count; i++)
            {
                GameObject test = acidicGO[i];

                currentMR = test.GetComponent<MeshRenderer>();
                currentMR.material.EnableKeyword("_EMISSION");
                currentMR.material.SetColor("_EmissionColor", acidicMAT[i].material.color);

                //Color.blue);


                test3 = currentMR.material.color;




                currentMR.material.globalIlluminationFlags = MaterialGlobalIlluminationFlags.BakedEmissive; // This allows you to alter emission settings on the shader in each of the ph gameObjects.

                currentMR.material.SetColor("_EmissionColor", acidicMAT[i].material.color);

                //Color.blue);


                // currentMR.material.EnableKeyword("_EmissionColor");

                Debug.Log("I'm tired" + " " + i);

            }


         

        }

        if (!beakerScript.PartOneComplete)
        {
            partOneGOActiveStart.SetActive(true);
            partOneGOInactiveStart.SetActive(false);
            sceneBackground.clearFlags = CameraClearFlags.SolidColor;
            sceneBackground.backgroundColor = Color.black;
            
        }

        else if (beakerScript.PartOneComplete)
        {
            partOneGOActiveStart.SetActive(false);
            partOneGOInactiveStart.SetActive(true);
            sceneBackground.clearFlags = CameraClearFlags.Skybox;
        }

        if(Input.GetKeyDown(KeyCode.G))
        {
            beakerScript.PartOneComplete = true;
        }

        else if(Input.GetKeyDown(KeyCode.T))
        {
            beakerScript.PartOneComplete = false;
        }
    }
}
