using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
public class savingAndLoading 
{

    public static string directory = "SaveData";
    public static string fileName = "MySave";

    public static List<string> savePathsForTutorialParts = new List<string>();

    static int fileCount = 0;

    public static int FileCount { get { return fileCount; } set { fileCount = value; } }
    public static int numberOfSaveFiles = 0;

    public static string test1;
    public static string test2;
    // Start is called before the first frame update
 
    public static void Save(List<savedObject> so)
    {
        if(fileCount<3)
        {
            if(!DirectoryExists())
            {
                Directory.CreateDirectory(Application.persistentDataPath + "/" + directory);
            }
            fileCount++;
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(GetFullPath());
            bf.Serialize(file, so);

            Debug.Log("Save Successful");



           // savePathsForTutorialParts[numberOfSaveFiles] = GetFullPath();
            numberOfSaveFiles++;

            file.Close();
        }
    }

    public static List<savedObject> Load(string filePath)
    {
        if (SaveExists())
        {
            try
            {
                BinaryFormatter bf = new BinaryFormatter();

                FileStream file = File.Open(filePath, FileMode.Open);

                List<savedObject> so = (List<savedObject>)bf.Deserialize(file);

                Debug.Log("Load Successful");

                file.Close();

                Debug.Log("Load Successful");

                return so;
            }


            catch (SerializationException)
            {
                Debug.Log("Failed to load file");
            }

         
        }

        return null;
    }

        private static bool SaveExists()
        {
            return File.Exists(GetFullPath());
        }

    private static bool DirectoryExists()
    {
        return Directory.Exists(Application.persistentDataPath + "/" + directory);
    }


    public static string GetFullPath()
    {

        string test = Application.persistentDataPath + "/" + directory + "/" + fileName + fileCount;

      /*  if (fileCount == 1) { savePathsForTutorialParts[0] = test; }

        else if (fileCount == 2) { savePathsForTutorialParts[1] = test; }

        else if (fileCount ==3) { savePathsForTutorialParts[2] = test; } */

        savePathsForTutorialParts.Add(test);

        Debug.Log(test + "JOE");

        return Application.persistentDataPath + "/" + directory + "/" + fileName + fileCount;
    }
    
}
