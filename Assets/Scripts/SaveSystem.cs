using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class SaveSystem
{
    private static string Path = Application.dataPath + "/globals.txt";
    
    public static void Save(Global.Global g)
    {
        string text = g.volume + "\n" + g.fov + "\n" + g.sensitivity;
        
        TextWriter sw = new StreamWriter(Path);

        sw.Write(text);

        sw.Close();
    }

    public static GameData Load()
    {
        var lines = new List<string>();
        var line = "";

        TextReader sr;

        try
        {
            sr = new StreamReader(Path);
            while ((line = sr.ReadLine()) != null) lines.Add(line);
        }
        catch (FileNotFoundException e)
        {
            return new GameData();
        }

        sr.Close();

        return new GameData(float.Parse(lines[0]), float.Parse(lines[1]), float.Parse(lines[2]));
    }
}