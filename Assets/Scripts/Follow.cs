using UnityEngine;

public class Follow : MonoBehaviour {

    public Transform objetivo;
    public Vector3 offset1 = new Vector3(0, 0, -10);
    public float suavizado = 5f;
    private Vector3 destino;

    void LateUpdate() {
        if (objetivo != null) {
            destino = objetivo.position + offset1;
            if (objetivo.position.y < -0.7) {
                destino.y = -0.7f;
            }
            if (destino.x < 0) {
                destino.x = 0;
            }
            transform.position = Vector3.Lerp(transform.position, destino, suavizado * Time.deltaTime);
        }
    }

    public void EstablecerObjetivo(Transform nuevoObjetivo) {
        objetivo = nuevoObjetivo;
    }
}
