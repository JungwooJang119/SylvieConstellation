using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using static SerializationAssistant;
using System.Threading.Tasks;

public class SaveSystem : Singleton<SaveSystem>
{
    [System.Serializable]
    public class Save
    {
        public string pathName;
        public SerializableVector3 sylviePosition;
    }

    public const string DUMMY_FILE_NAME = "save0";

    // Start is called before the first frame update
    void Awake()
    {
        InitializeSingleton(gameObject);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static Save GenerateSave()
    {
        Save save = new Save();

        save.pathName = DUMMY_FILE_NAME;
        GameObject sylvie = GameObject.FindWithTag("Player");
        save.sylviePosition = sylvie.transform.position.ToSerializable();

        return save;
    }

    public static void SaveToFile(Save save)
    {
        Debug.Log($"Saving to {Application.dataPath}");

        const int BUF_SIZE = 1_000;

        BinaryFormatter bf = new BinaryFormatter();
        string realPath = Path.ChangeExtension(Path.Combine(Application.dataPath, save.pathName), ".sylvie");
        using FileStream fs = new FileStream(
            realPath,
            FileMode.Create,
            FileAccess.ReadWrite,
            FileShare.ReadWrite,
            bufferSize: BUF_SIZE,
            useAsync: true);
        bf.Serialize(fs, save);
    }

    public static Save LoadFromFile(string filename)
    {
        Debug.Log($"Loading from {Application.dataPath}");

        const int BUF_SIZE = 1_000;

        BinaryFormatter bf = new BinaryFormatter();
        string realPath = Path.ChangeExtension(Path.Combine(Application.dataPath, filename), ".sylvie");
        using FileStream fs = new FileStream(
            realPath,
            FileMode.Open,
            FileAccess.Read,
            FileShare.Read,
            bufferSize: BUF_SIZE,
            useAsync: true);
        Save save = (Save)bf.Deserialize(fs);

        return save;
    }

    public static void LoadSave(Save save)
    {
        GameObject sylvie = GameObject.FindWithTag("Player");
        sylvie.transform.position = save.sylviePosition.ToVector3();
    }
}
