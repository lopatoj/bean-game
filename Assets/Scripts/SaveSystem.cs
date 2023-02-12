using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class SaveSystem
{
    public static void SaveGlobals(Global g)
    {
        var path = Application.dataPath + "/globals.txt";
        string[] textLines = {g.volume + "", g.fov + "", g.sensitivity + ""};

        File.CreateText(path);
        var sw = new StreamWriter(path);

        foreach (var line in textLines) sw.WriteLine(line);

        sw.Close();
    }

    public static Global LoadGlobals()
    {
        var path = Application.persistentDataPath + "/globals.txt";
        var lines = new List<string>();
        var line = "";

        StreamReader sr;

        try
        {
            sr = new StreamReader(path);
        }
        catch (FileNotFoundException e)
        {
            File.CreateText(path);
            return ScriptableObject.CreateInstance<Global>();
        }

        while ((line = sr.ReadLine()) != null) lines.Add(line);

        sr.Close();

        return ScriptableObject.CreateInstance<Global>();
    }
}