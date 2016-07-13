using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Xml.Serialization;

public class Editor_Load
{
    public string folderPath = Application.dataPath.ToString() + @"/Maps";
    public void Editor_LoadXML(string name = "Test")
    {
        // Create a new XmlSerializer instance with the type of the test class
        XmlSerializer SerializerObj = new XmlSerializer(typeof(Editor_RoomLayout));

        // Create a new file stream for reading the XML file
        string path = Application.dataPath.ToString() + @"/Maps/" + name + ".xml";
        FileStream ReadFileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);

        // Load the object saved above by using the Deserialize function
        Editor_RoomLayout LoadedObj = (Editor_RoomLayout)SerializerObj.Deserialize(ReadFileStream);

        Editor_References.instance.handler.Reset();
        Editor_References.instance.handler.Load(LoadedObj);

        // Cleanup
        ReadFileStream.Close();
    }

    public Editor_RoomLayout InGame_Load(string path)
    {
        // Create a new XmlSerializer instance with the type of the test class
        XmlSerializer SerializerObj = new XmlSerializer(typeof(Editor_RoomLayout));

        // Create a new file stream for reading the XML file
        FileStream ReadFileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);

        // Load the object saved above by using the Deserialize function
        Editor_RoomLayout LoadedObj = (Editor_RoomLayout)SerializerObj.Deserialize(ReadFileStream);

        // Cleanup
        ReadFileStream.Close();

        return LoadedObj;
    }

    public void Init()
    {

    }
}
