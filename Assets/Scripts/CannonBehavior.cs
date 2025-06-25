using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CannonBehavior : MonoBehaviour {
    public Vector3 ultimaPos;
    public int rotationSpeed = 50;
    public bool rotateUp = false;
    int anguloZ;
    public Transform rotAxis;
    public GameObject cannon;
    public Transform shootPoint;
    public GameObject meal;
    public Button shootButton;



    void Start() {
        rotAxis = this.gameObject.transform.GetChild(0);
        cannon = rotAxis.gameObject.transform.GetChild(0).gameObject;
        shootPoint = cannon.transform.GetChild(0);
        meal = Resources.Load("Prefabs/Meal") as GameObject;
        shootButton = GameObject.Find("ShootButton").GetComponent<Button>();

    }
    void Update() {
        float anguloZ = rotAxis.transform.localRotation.eulerAngles.z;
        if (anguloZ > 180f) anguloZ -= 360f;
        if (rotateUp) {
            rotAxis.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
            if (anguloZ >= 10) {
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

    public void Shoot() {
        shootButton.gameObject.SetActive(false);
        GameObject newMeal = Instantiate(meal, shootPoint.position, Quaternion.identity);
        newMeal.GetComponent<Rigidbody2D>().AddForce(shootPoint.up * 500f);
        Camera.main.GetComponent<Follow>().EstablecerObjetivo(newMeal.transform);
    }
}
