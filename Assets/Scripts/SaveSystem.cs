using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class SaveSystem
{
    public static void SaveGlobals(Global g)
    {
        string path = Application.dataPath + "/globals.txt";
        string[] textLines = { g.volume + "", g.fov + "", g.sensitivity + "" };

        File.CreateText(path);
        StreamWriter sw = new StreamWriter(path);

        foreach (string line in textLines)
        {
            sw.WriteLine(line);
        }

        sw.Close();
        
    }

    public static Global LoadGlobals()
    {
        string path = Application.persistentDataPath + "/globals.txt";
        List<string> lines = new List<string>();
        string line = "";

        StreamReader sr;

        try
        {
            sr = new StreamReader(path);
        }
        catch (FileNotFoundException e)
        {
            File.CreateText(path);
            return new Global(1, 90, 2);
        }

        while ((line = sr.ReadLine()) != null)
        {
            lines.Add(line);
        }

        sr.Close();

        return new Global(float.Parse(lines[0]), float.Parse(lines[1]), float.Parse(lines[2]));
    }
}
