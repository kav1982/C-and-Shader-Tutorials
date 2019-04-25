using System.Collections;
using System.Collections.Generic;
//using System.IO;
using UnityEngine;

public class Game : PersistableObject
{
    //public PersistableObject prefab;
    public ShapeFactory shapeFactory;
    public PersistentStorage storage;
    public KeyCode createKey = KeyCode.C;
    public KeyCode newGameKey = KeyCode.N;
    public KeyCode saveKey = KeyCode.S;
    public KeyCode loadKey = KeyCode.L;

    List<Shape> shapes;
    //string savePath;

    void Awake ()
    {
        shapes = new List<Shape>();
        //savePath = Path.Combine(Application.persistentDataPath, "saveFile");
    }

        void Update ()
    {
        if (Input.GetKeyDown(createKey))
        {
            CreateShape ();
        }
        else if (Input.GetKey(newGameKey))
        {
            BeginNewGame();
        }
        else if (Input.GetKeyDown(saveKey))
        {
            storage.Save(this);
            //Save();
        }
        else if (Input.GetKeyDown(loadKey))
        {
            BeginNewGame();
            storage.Load(this);
            //Load();
        }
    }


    void CreateShape ()
    {
        //PersistableObject o = Instantiate(prefab);
        Shape instance = shapeFactory.GetRandom();
        Transform t = instance.transform;
        t.localPosition = Random.insideUnitSphere * 5f;
        t.localRotation = Random.rotation;
        t.localScale = Vector3.one * Random.Range(0.1f, 1f);
        shapes.Add(instance);
    }

    void BeginNewGame()
    {
        for (int i = 0; i < shapes.Count; i++)
        {
            Destroy(shapes[i].gameObject);
        }
        shapes.Clear();
    }

    public override void Save (GameDataWriter writer)
    {
        writer.Write(shapes.Count);
        for(int i = 0; i < shapes.Count; i++)
        {
            shapes[i].Save(writer);
        }
    }

    public override void Load(GameDataReader reader)
    {
        int count = reader.ReadInt();
        for (int i = 0; i< count; i++)
        {
            //PersistableObject o = Instantiate(prefab);
            Shape instance =shapeFactory.Get(0);
            instance.Load(reader);
            shapes.Add(instance);
        }
    }

    // void Save()
    // {
    //     using(var writer = new BinaryWriter(File.Open(savePath,FileMode.Create)))
    //     {
    //         writer.Write(objects.Count);
    //         for (int i= 0; i< objects.Count; i++)
    //         {
    //             Transform t =objects[i];
    //             writer.Write(t.localPosition.x);
    //             writer.Write(t.localPosition.y);
    //             writer.Write(t.localPosition.z);
    //         }
    //     }
    // }

    // void Load()
    // {
    //     BeginNewGame();
    //     using(var reader = new BinaryReader(File.Open(savePath, FileMode.Open)))
    //     {
    //         int count = reader.ReadInt32();
    //         for (int i = 0; i<count; i++)
    //         {
    //             Vector3 p;
    //             p.x = reader.ReadSingle();
    //             p.y = reader.ReadSingle();
    //             p.z = reader.ReadSingle();
    //             Transform t = Instantiate(prefab);
    //             t.localPosition = p;
    //             objects.Add(t);
    //         }
    //     }
    // }
}
