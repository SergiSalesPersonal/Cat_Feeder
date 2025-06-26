using UnityEngine;

public class CannonBehavior : MonoBehaviour {

    #region Variables
    public Vector3 ultimaPos;
    public int rotationSpeed = 50;
    public bool rotateUp = false;
    public Transform rotAxis;
    public GameObject cannon;
    public Transform shootPoint;
    public GameObject meal;

    #endregion

    void Start() {
        rotAxis = this.gameObject.transform.GetChild(0);
        cannon = rotAxis.gameObject.transform.GetChild(0).gameObject;
        shootPoint = cannon.transform.GetChild(0);
        meal = Resources.Load("Prefabs/Meal") as GameObject;


        GameManager gameManager = FindFirstObjectByType<GameManager>();

    }
    void Update() {
        float anguloZ = rotAxis.transform.localRotation.eulerAngles.z;
        if (anguloZ > 180f) anguloZ -= 360f;
        if (rotateUp) {
            rotAxis.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
            if (anguloZ >= 45) {
                rotateUp = false;
            }
        }
        else {
            rotAxis.Rotate(Vector3.back * rotationSpeed * Time.deltaTime);
            if (anguloZ <= -45) {
                rotateUp = true;
            }
        }


    }

}
