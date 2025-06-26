using UnityEngine;

public class FloorBehavior : MonoBehaviour {

    #region Variables
    public Transform meal;
    public const float mealDistance = 50f;
    #endregion

    // Update is called once per frame
    void Update() {
        if (meal == null) return;
        if (Vector3.Distance(meal.position, transform.position) > mealDistance) {
            Destroy(gameObject);
        }
    }
}
