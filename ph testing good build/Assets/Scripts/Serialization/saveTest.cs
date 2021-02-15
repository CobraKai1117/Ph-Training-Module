
using System.Collections.Generic;
using UnityEngine;

public class saveTest : MonoBehaviour
{

    public savedObject so;
    public List<GameObject> objectsToSerialize;
    public List<savedObject> containerToHoldObj;
    public savedObject currentObj;
    public List<savedObject> test;
    [SerializeField] string nameOfObjectItem1;
    [SerializeField] string nameOfObjectItem2;

    GameObject B;
    

   // public List<GameObject> objectDataToLoadBack;

    public static string pathOfFile;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.S))
        {
           
                containerToHoldObj = transferDataToContainerForSaving(objectsToSerialize);  //The objects to serialize database contains the information about the gameObjects inside of it. It transfers this to the containerToHoldObj database. Then the containerToHoldObj transfers this to the test database.

                test = containerToHoldObj;
                nameOfObjectItem1 = test[0].nameOfObject;
                savingAndLoading.Save(test);

            Material testing = gameObject.GetComponent<Renderer>().material;

            



            

            nameOfObjectItem2 = test[0].nameOfObject;

            for(int i = 0; i < test.Count; i++)
            {
               // for(int t = 0; t < test[t].
            }
        }


        if(Input.GetKeyDown(KeyCode.L))
        {
            test = savingAndLoading.Load(savingAndLoading.savePathsForTutorialParts[0]);

            Debug.Log("CC" + savingAndLoading.savePathsForTutorialParts[0]);

            transferDataToContainerForLoading(test);
        }

         if (Input.GetKeyDown(KeyCode.G))
        {
            test = savingAndLoading.Load(savingAndLoading.savePathsForTutorialParts[1]);

            Debug.Log("DD" + savingAndLoading.savePathsForTutorialParts[1]);

            transferDataToContainerForLoading(test);
        }
    }


    [SerializeField] List<savedObject> transferDataToContainerForSaving(List<GameObject> listToSerialize)
    {
        List<savedObject> returnedItems = new List<savedObject>();

        for(int i = 0; i<listToSerialize.Count; i++)
        {
            currentObj = new savedObject();

            currentObj.objPos[0] = listToSerialize[i].transform.localPosition.x;
            currentObj.objPos[1] = listToSerialize[i].transform.localPosition.y;
            currentObj.objPos[2] = listToSerialize[i].transform.localPosition.z;

            currentObj.nameOfObject = listToSerialize[i].name;

            currentObj.objRot[0] = listToSerialize[i].transform.localRotation.x;
            currentObj.objRot[1] = listToSerialize[i].transform.localRotation.y;
            currentObj.objRot[2] = listToSerialize[i].transform.localRotation.z;
            currentObj.objRot[3] = listToSerialize[i].transform.localRotation.w;

            currentObj.childScale[0] = listToSerialize[i].transform.GetChild(0).localScale.x;
            currentObj.childScale[1] = listToSerialize[i].transform.GetChild(0).localScale.y;
            currentObj.childScale[2] = listToSerialize[i].transform.GetChild(0).localScale.z;

            currentObj.objScale[0] = listToSerialize[i].transform.localScale.x;
            currentObj.objScale[1] = listToSerialize[i].transform.localScale.y;
            currentObj.objScale[2] = listToSerialize[i].transform.localScale.z;

         

            Debug.Log(listToSerialize[i].transform.GetChild(0).name + "ALI");

            currentObj.nameOfMaterial = listToSerialize[i].GetComponentInChildren<Renderer>().material.name;

         

            

            returnedItems.Add(currentObj);
        }

        return returnedItems;
    }

    void transferDataToContainerForLoading(List<savedObject>listToLoad)
    { 
        
        for(int i = 0; i <listToLoad.Count; i++)
        {
            
            objectsToSerialize[i].name = listToLoad[i].nameOfObject;
            objectsToSerialize[i].transform.localPosition = new Vector3(listToLoad[i].objPos[0], listToLoad[i].objPos[1], listToLoad[i].objPos[2]);

            objectsToSerialize[i].transform.localRotation = new Quaternion(listToLoad[i].objRot[0], listToLoad[i].objRot[1], listToLoad[i].objPos[2], listToLoad[i].objRot[3]);

            objectsToSerialize[i].transform.GetChild(0).localScale = new Vector3(listToLoad[i].childScale[0], listToLoad[i].childScale[1], listToLoad[i].childScale[2]);

            objectsToSerialize[i].transform.localScale = new Vector3(listToLoad[i].objScale[0], listToLoad[i].objScale[1], listToLoad[i].objScale[0]);

           

          //  objectsToSerialize[i].GetComponentInChildren<Renderer>().material = (Material)Resources.Load


        }
    }
}
