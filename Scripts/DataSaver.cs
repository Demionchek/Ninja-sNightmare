using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.UI;


public class DataSaver : MonoBehaviour
{
    [SerializeField] private GameObject _bckgrndSlider;
    [SerializeField] private GameObject _effectsSlider;

    private ResultCounter _resultCounter;
    private float _savedDistance;
    private float _newDistance;
    private int _savedScore;
    private int _newScore;
    private float _bckgrndVolume;
    private float _effectsVolume;

    public int NewScore { get { return _newScore; } }
    public int SavedScore { get { return _savedScore; } }
    public float NewDistance { get { return _newDistance; } }
    public float SavedDistance { get { return _savedDistance; } }

    private void Awake()
    {
        _resultCounter = GetComponent<ResultCounter>();
    }

    private void Start()
    {
        LoadPlayerPrefs();
    }

    public void NewGameData()
    {
        _newScore = _resultCounter.Score;
        _newDistance = _resultCounter.Distance;
    }

    public void SavePlayerPrefs()
    {
        if (_effectsSlider != null & _bckgrndSlider != null)
        {
            _bckgrndVolume = _bckgrndSlider.GetComponent<Slider>().value;
            _effectsVolume = _effectsSlider.GetComponent<Slider>().value;
            PlayerPrefs.SetFloat("SavedBckgrndVol", _bckgrndVolume);
            PlayerPrefs.SetFloat("SavedEffectsVol", _effectsVolume);
            PlayerPrefs.Save();
        }
    }

    private void LoadPlayerPrefs()
    {
        if (_effectsSlider != null & _bckgrndSlider != null)
        {
            if (PlayerPrefs.HasKey("SavedEffectsVol"))
            {
                _effectsVolume = PlayerPrefs.GetFloat("SavedEffectsVol");
                _bckgrndVolume = PlayerPrefs.GetFloat("SavedBckgrndVol");
                _effectsSlider.GetComponent<Slider>().value = _effectsVolume;
                _bckgrndSlider.GetComponent<Slider>().value = _bckgrndVolume;
            }
        }
    }

    public void SaveScore()
    {
        _savedScore = _newScore;
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath
            + "/MySaveDataScore.dat");
        SaveData data = new SaveData();
        data.SavedScore = _savedScore;
        bf.Serialize(file, data);
        file.Close();
    }

    public void SaveDistance()
    {
        _savedDistance = _newDistance;
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath
            + "/MySaveDataDistance.dat");
        SaveData data = new SaveData();
        data.SavedDistance = _savedDistance;
        bf.Serialize(file, data);
        file.Close();
    }

    public void LoadScoreData()
    {
        if (File.Exists(Application.persistentDataPath
            + "/MySaveDataScore.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file =
                File.Open(Application.persistentDataPath
            + "/MySaveDataScore.dat", FileMode.Open);
            SaveData data = (SaveData)bf.Deserialize(file);
            file.Close();
            _savedScore = data.SavedScore;
        }
    }

    public void LoadDistanceData()
    {
        if (File.Exists(Application.persistentDataPath
            + "/MySaveDataDistance.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file =
                File.Open(Application.persistentDataPath
            + "/MySaveDataDistance.dat", FileMode.Open);
            SaveData data = (SaveData)bf.Deserialize(file);
            file.Close();
            _savedDistance = data.SavedDistance;
        }
    }

    public void ClearData()
    {
        if (File.Exists(Application.persistentDataPath
           + "/MySaveDataDistance.dat"))
        {          
            File.Delete(Application.persistentDataPath
           + "/MySaveDataDistance.dat");
            _savedDistance = 0f;
        }

        if (File.Exists(Application.persistentDataPath
           + "/MySaveDataScore.dat"))
        {
            File.Delete(Application.persistentDataPath
              + "/MySaveDataScore.dat");
            _savedScore = 0;            
        }
    }
}

[Serializable]
class SaveData
{
    public float SavedDistance;
    public int SavedScore;

}