using UnityEngine;
using System.Collections;
using System;
using System.Xml.Serialization;
using System.IO;

public class Editor_Save
{

    public void Save(string name = "Test")
    {
        //Should save the info needed to create a RoomLayout

        // Create a new XmlSerializer instance with the type of the test class
        XmlSerializer SerializerObj = new XmlSerializer(typeof(Editor_RoomLayout));

        string path = Application.dataPath.ToString() + @"/Maps/" + name + ".xml";
        // Create a new file stream to write the serialized object to a file
        TextWriter WriteFileStream = new StreamWriter(path);//(@"C:\"+ name +".xml");
        SerializerObj.Serialize(WriteFileStream, Editor_References.instance.handler.layout);

        // Cleanup
        WriteFileStream.Close();
    }
    public void Init()
    {
    }
}
