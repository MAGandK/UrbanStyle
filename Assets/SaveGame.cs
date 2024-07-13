using UnityEngine;

public class SaveGame : MonoBehaviour
{
    private int _intSave;
    private float _fliatSave;
    private string _stringSave = "";

    private void OnGUI()
    {
        if (GUI.Button(new Rect(0,0,250,100),"Изменить int"))
        {
            _intSave++;
        }
        if (GUI.Button(new Rect(0,100,250,100),"Изменить float"))
        {
           _fliatSave++;
        }

        _stringSave = GUI.TextField(new Rect(0, 300, 120, 50), _stringSave, 20);
        
        GUI.Label(new Rect(375,0,125,50),"значение int" + _intSave);
        GUI.Label(new Rect(375,100,125,50),"значение int" + _fliatSave);
        GUI.Label(new Rect(375,200,125,50),"значение string" + _stringSave);

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
        PlayerPrefs.DeleteAll();
        _intSave = 0;
        _fliatSave = 0;
        _stringSave = "";
    }

    private void LoadGame()
    {
        if (PlayerPrefs.HasKey("SaveInt"))
        {
            _intSave = PlayerPrefs.GetInt("SaveInt");
            _fliatSave = PlayerPrefs.GetInt("SaveFloat");
            _stringSave = PlayerPrefs.GetString("SaveString");
            Debug.Log("LOADED");
        }
        else
        {
            Debug.LogError("Error");
        }
    }

    private void SavesGame()
    {
        PlayerPrefs.SetInt("SaveInt", _intSave);
        PlayerPrefs.SetFloat("SaveFlaot", _fliatSave);
        PlayerPrefs.SetString("SaveString", _stringSave);
        PlayerPrefs.Save();
        Debug.Log("SAVE");
    }
}
