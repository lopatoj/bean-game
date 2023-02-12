using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class SaveSystem
{
    // Path to settings text file
    private static string Path = Application.dataPath + "/globals.txt";
    
    // Method that creates/overwrites a text file with settings values
    public static void Save(Global.Global g)
    {
        // Text to be written to settings text file, with a newline in between each value
        string text = g.volume + "\n" + g.fov + "\n" + g.sensitivity;
        
        // Opens stream
        TextWriter sw = new StreamWriter(Path);

        // Writes text to file
        sw.Write(text);

        // Closes stream
        sw.Close();
    }
    
    // Method that creates/overwrites a text file with settings values
    public static void Save(GameData g)
    {
        // Text to be written to settings text file, with a newline in between each value
        string text = g.volume + "\n" + g.fov + "\n" + g.sensitivity;
        
        // Opens stream
        TextWriter sw = new StreamWriter(Path);

        // Writes text to file
        sw.Write(text);

        // Closes stream
        sw.Close();
    }

    // Method that loads settings values from each line of settings text file
    public static GameData Load()
    {
        // ArrayList of settings values
        var lines = new List<string>();
        var line = "";

        TextReader sr;

        // Try-catch statement used to handle possible absence of settings text file
        try
        {
            // Opens stream
            sr = new StreamReader(Path);
            
            // Adds lines from settings file until no more lines
            while ((line = sr.ReadLine()) != null) lines.Add(line);
        }
        catch (FileNotFoundException)
        {
            // Creates GameData with default settings values
            var data = new GameData();
            
            // Saves default data to new settings file
            Save(data);
            
            // Returns default data
            return data;
        }

        // Closes stream
        sr.Close();
    
        // Returns data from first three lines of settings text file
        return new GameData(float.Parse(lines[0]), float.Parse(lines[1]), float.Parse(lines[2]));
    }
}