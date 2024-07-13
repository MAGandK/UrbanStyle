using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveGameSerealize : MonoBehaviour
{
    private int _intSave;
    private float _fliatSave;
    private bool _boolToSave;
    
    private void OnGUI()
    {
        if (GUI.Button(new Rect(0,0,200,100),"Изменить int"))
        {
            _intSave++;
        }
        if (GUI.Button(new Rect(0,150,200,100),"Изменить float"))
        {
            _fliatSave+=0.1f;
        }

        if (GUI.Button(new Rect(0,300,200,100),"Изменить bool"))
        {
            _boolToSave = _boolToSave ? _boolToSave = false : _boolToSave = true;
        }
        
        GUI.Label(new Rect(375,0,200,100),"значение int" + _intSave);
        GUI.Label(new Rect(375,150,200,100),"значение int" + _fliatSave.ToString("F1"));
        GUI.Label(new Rect(375,300,200,100),"значение string" + _boolToSave);

        if (GUI.Button(new Rect(750,0,125,50), "Save"))
        {
            SavesGame();
        }
        if (GUI.Button(new Rect(750,100,125,50), "Save"))
        {
            LoadGame();
        }
        if (GUI.Button(new Rect(750,200,125,50), "Save"))
        {
            RestartGame();
        }
        
        
    }

    private void RestartGame()
    {
        if (File.Exists(Application.persistentDataPath + "SaveFile.dat"))
        {
            File.Delete(Application.persistentDataPath + "SaveFile.dat");
            _intSave = 0;
            _fliatSave = 0;
            _boolToSave = false;
        }
        else
        {
            Debug.LogError("Error");
        }
    }

    private void LoadGame()
    {
        if (File.Exists(Application.persistentDataPath + "SaveFile.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "SaveFile.dat", FileMode.Open);
            SaveDataSerialize data = (SaveDataSerialize)bf.Deserialize(file);
            file.Close();
            _intSave = data.IntSave;
            _fliatSave = data.FliatSave;
            _boolToSave = data.BoolToSave;
            
            Debug.Log("Donloading");
        }
        else
        {
            Debug.LogError("File not find");
        }
    }

    private void SavesGame()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/SaveFile.dat");
        SaveDataSerialize data = new SaveDataSerialize();
        data.IntSave = _intSave;
        data.FliatSave = _fliatSave;
        data.BoolToSave = _boolToSave;
        
        bf.Serialize(file,data);
        file.Close();
        Debug.Log("Saving");
    }
    
    

    [Serializable]
    class SaveDataSerialize
    {
        public int IntSave;
        public float FliatSave;
        public bool BoolToSave;
    }
}
