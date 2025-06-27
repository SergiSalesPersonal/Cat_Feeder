using UnityEngine;
using UnityEngine.SceneManagement;
public class FloorCreation : MonoBehaviour {

    #region Variables
    public GameObject floorPrefab;
    public float blockWidth;
    public Transform meal;
    public const float mealDistance = 40f;
    private float nextBlockX;

    #endregion
    void OnEnable() {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        if (scene.name == "Game") {
            floorPrefab = Resources.Load("Prefabs/Floor") as GameObject;
            nextBlockX = -15f;
            blockWidth = 20f;

            for (int i = 0; i < 3; i++) {
                CreateBlock();
            }
        }
    }

    void Update() {
        if (meal == null) return;

        if (meal.position.x + mealDistance > nextBlockX) {
            CreateBlock();
        }
    }

    private void CreateBlock() {
        Vector3 pos = new Vector3(nextBlockX, -10.2f, 0f);
        Instantiate(floorPrefab, pos, Quaternion.identity);
        nextBlockX += blockWidth;
    }

}
