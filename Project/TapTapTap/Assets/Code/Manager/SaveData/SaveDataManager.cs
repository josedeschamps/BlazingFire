using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveDataManager : MonoBehaviour
{

    private GameManager gm;

    private void Awake()
    {
        gm = FindObjectOfType<GameManager>();
        LoadAllData();
    }

    public void DeleteSaveData()
    {
        PlayerPrefs.DeleteAll();
    }

    public void SaveAllData()
    {
        //PlayerPrefs.SetInt("user", gm.user);
        //PlayerPrefs.SetFloat("Score", gm._score);
        //PlayerPrefs.SetFloat("Currency", gm._currency);


    }

    public void LoadAllData()
    {
        //gm.user = PlayerPrefs.GetInt("user", gm.user);
        //gm._score = PlayerPrefs.GetFloat("Score", gm._score);
        //gm._currency = PlayerPrefs.GetFloat("Currency", gm._currency);
    }

}
