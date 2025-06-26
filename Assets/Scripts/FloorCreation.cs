using UnityEngine;

public class FloorCreation : MonoBehaviour {

    #region Variables
    public GameObject floorPrefab;
    public float blockWidth = 20f;
    public Transform meal;
    public const float mealDistance = 40f;
    private float nextBlockX = -15f;

    #endregion
    void Start() {
        floorPrefab = Resources.Load("Prefabs/Floor") as GameObject;

        for (int i = 0; i < 3; i++) {
            CreateBlock();
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
