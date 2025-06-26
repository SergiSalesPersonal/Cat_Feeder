using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData {
    public int coins = 0;
    public int MaxVelocity = 20; //Mejoras +5
    public float powerShot = 1.0f;
    public float airFriction = 0f; //Mejoras +0.05f
    public float groundFriction = 0f; //Mejoras +0.1f
    public float enemyFriction = 0f; //Mejoras +0.1f
    public int levEnemies = 1;
    public int levCoins = 1;
    public int propeller = 0;
    public int levPropeller = 0;
}

public class GameManager : MonoBehaviour {

    public GameData data = new GameData();
    private static GameManager instance;

    void Awake() {
        if (instance == null) {
            instance = this;

            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }
    void Start() {
        if (PlayerPrefs.HasKey("GameData")) {
            string json = PlayerPrefs.GetString("GameData");
            data = JsonUtility.FromJson<GameData>(json);
        }
        else {
            SaveGame();
        }
    }
    void OnApplicationQuit() {
        SaveGame();
    }

    public void SaveGame() {
        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString("GameData", json);
        PlayerPrefs.Save();
    }

}
