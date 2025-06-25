using UnityEngine;

public class Follow : MonoBehaviour {

    public Transform objetivo;
    public Vector3 offset = new Vector3(0, 0, -10);
    public float suavizado = 5f;

    void LateUpdate() {
        if (objetivo != null) {
            Vector3 destino = objetivo.position + offset;
            transform.position = Vector3.Lerp(transform.position, destino, suavizado * Time.deltaTime);
        }
    }

    public void EstablecerObjetivo(Transform nuevoObjetivo) {
        objetivo = nuevoObjetivo;
    }
}
