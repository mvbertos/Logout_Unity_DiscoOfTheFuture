using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveSystem : MonoBehaviour
{
    private const string SAVE_EXTENSION = "txt";

    private static readonly string SAVE_FOLDER = Application.dataPath + "/Saves/";
    private static bool isInit = false;

    public static void Init()
    {
        if (!isInit)
        {
            //test if save folder exists
            isInit = true;
            if (!Directory.Exists(SAVE_FOLDER))
            {
                //Create save folder
                Directory.CreateDirectory(SAVE_FOLDER);
            }
        }
    }
    public static void Save(string fileName, string saveString, bool overWrite)
    {
        Init();
        string saveFile = fileName;
        if (!overWrite)
        {
            //make sure the save number is unique so it doesnt overwrite a previous save file
            int saveNumber = 1;
            while (File.Exists(SAVE_FOLDER + "_" + saveNumber))
            {
                saveNumber++;
                saveFile = fileName + "_" + saveNumber;
            }
            // savefilename is unique
        }
        File.WriteAllText(SAVE_FOLDER + saveFile + "." + SAVE_EXTENSION, saveString);
    }

    public static string Load(string fileName)
    {
        Init();
        if (File.Exists(SAVE_FOLDER + fileName + "." + SAVE_EXTENSION))
        {
            string saveString = File.ReadAllText(SAVE_FOLDER + fileName + "." + SAVE_EXTENSION);
            return saveString;
        }
        return null;
    }

    public static string LoadMostRecentFile()
    {
        Init();
        DirectoryInfo directoryInfo = new DirectoryInfo(SAVE_FOLDER);
        //get all save files
        FileInfo[] saveFiles = directoryInfo.GetFiles("*." + SAVE_EXTENSION);
        //Cycle through all save files and identify the most recent one
        FileInfo mostRecentfile = null;
        foreach (FileInfo fileInfo in saveFiles)
        {
            if (mostRecentfile == null)
            {
                mostRecentfile = fileInfo;
            }
            else
            {
                if (fileInfo.LastWriteTime > mostRecentfile.LastWriteTime)
                {
                    mostRecentfile = fileInfo;
                }
            }
        }
        if (mostRecentfile != null)
        {
            string saveString = File.ReadAllText(mostRecentfile.FullName);
            return saveString;
        }
        else
        {
            return null;
        }
    }

    public static void SaveObject(object saveObject)
    {
        SaveObject("save", saveObject, false);
    }
    public static void SaveObject(string fileName, object saveObject, bool overWrite)
    {
        Init();
        string json = JsonUtility.ToJson(saveObject);
        Save(fileName, json, overWrite);
    }

    public static TSaveObject LoadMostRecentObject<TSaveObject>()
    {
        Init();
        string saveString = LoadMostRecentFile();
        if (saveString != null)
        {
            TSaveObject saveObject = JsonUtility.FromJson<TSaveObject>(saveString);
            return saveObject;
        }
        else
        {
            return default(TSaveObject);
        }
    }

    public static TSaveObject LoadObject<TSaveObject>(string fileName)
    {
        Init();
        string saveString = Load(fileName);
        if (saveString != null)
        {
            TSaveObject saveObject = JsonUtility.FromJson<TSaveObject>(saveString);
            return saveObject;

        }
        else
        {
            return default(TSaveObject);
        }
    }
}
