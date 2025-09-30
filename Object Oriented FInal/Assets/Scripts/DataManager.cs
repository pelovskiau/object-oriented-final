using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] public string PlayerName; //name
    [SerializeField] public float VolumeSetting; //volume option
    [SerializeField] public float TimeScore; //best score
    public int Difficulty;
    public static DataManager Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        Load();
        DontDestroyOnLoad(gameObject); //as per tut, if it exists, remove duplication.

    }
    [System.Serializable]
    class SaveData
    { 
        public string Name;
        public float Volume;
        public float Time;
    }
    public void Save()
    { 
        SaveData data = new SaveData();
        data.Name = PlayerName; //copied over from save functions, so we can keep the data.
        data.Volume = VolumeSetting;
        data.Time = TimeScore;
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/playersave.json", json);
    }
    public void Load()
    {
        string path = Application.persistentDataPath + "/playersave.json";
        if (File.Exists(path))
        { 
            string json = File.ReadAllText(path);
            SaveData data= JsonUtility.FromJson<SaveData>(json);
            PlayerName = data.Name;
            VolumeSetting = data.Volume;
            TimeScore = data.Time;
        }
    }

}
