using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public static class FileHandler
{
    public static List<T> ReadListFromResources<T>(string filenameWithoutExtension)
    {
        TextAsset jsonFile = Resources.Load<TextAsset>(filenameWithoutExtension);
        if (jsonFile == null || string.IsNullOrEmpty(jsonFile.text))
            return new List<T>();

        return JsonHelper.FromJson<T>(jsonFile.text).ToList();
    }

    public static T ReadFromResources<T>(string filenameWithoutExtension)
    {
        TextAsset jsonFile = Resources.Load<TextAsset>(filenameWithoutExtension);
        if (jsonFile == null || string.IsNullOrEmpty(jsonFile.text))
            return default(T);

        return JsonUtility.FromJson<T>(jsonFile.text);
    }

    private static string GetPath(string filename)
    {
        return Application.persistentDataPath + "/" + filename;
    }

    private static string ReadFile(string path)
    {
        if (File.Exists(path))
        {
            using (StreamReader reader = new StreamReader(path))
            {
                string content = reader.ReadToEnd();
                return content;
            }
        }
        return "";
    }
}

