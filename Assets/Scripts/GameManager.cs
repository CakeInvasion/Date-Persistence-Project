using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public TMP_InputField nameInputField;

    public string playerName;
    public int highScore;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
        LoadHighScore();
        nameInputField.text = playerName;
    }

    public void StartNew()
    {
        playerName = nameInputField.text;
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    [Serializable]
    class GameData
    {
        public string playerName;
        public int highScore;
    }

    public void SaveHighScore()
    {
        GameData data = new GameData();
        data.playerName = playerName;
        data.highScore = highScore;
        string json = JsonUtility.ToJson(data);
        System.IO.File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadHighScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (System.IO.File.Exists(path))
        {
            string json = System.IO.File.ReadAllText(path);
            GameData data = JsonUtility.FromJson<GameData>(json);
            playerName = data.playerName;
            highScore = data.highScore;
        }
    }
}
