using LoboLabs.GestureNeuralNet;
using System.IO;
using UnityEngine;

public abstract class UnityGestureIO
{
    // IO Constants
    private const string DEFAULT_DETECTOR_DIRECTORY = "data/GestureDetectors/";
    private const string DEFAULT_LEFT_NAME = "LeftHand";
    private const string DEFAULT_RIGHT_NAME = "RightHand";
    private const string DEFAULT_DETECTOR_EXT = ".gd";
    
    public static GestureDetector LoadDetector(string fileName)
    {
        GestureDetector detector = null;

        if (File.Exists(fileName))
        {
            using (BinaryReader reader = new BinaryReader(new FileStream(fileName, FileMode.Open)))
            {
                detector = new GestureDetector(reader);
            }
        }
        else
        {
            Debug.LogError("Couldn't find file name: " + fileName);
        }
        
        return detector;
    }

    public static void SaveDetector(string fileName, GestureDetector detector)
    {        
        using (BinaryWriter writer = new BinaryWriter(new FileStream(fileName, FileMode.Create)))
        {
            detector.Save(writer);
        }
    }
}
