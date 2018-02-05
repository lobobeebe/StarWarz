using UnityEngine;
using LoboLabs.Utilities;

public class UnityLogWriter : LogWriter
{
    protected override void _Write(string message)
    {
        Debug.Log(message);
    }
}
