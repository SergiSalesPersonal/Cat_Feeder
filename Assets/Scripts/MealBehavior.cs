using System.Collections;
using TMPro;
using UnityEngine;

public class MealBehavior : MonoBehaviour {
    #region Variables
    const int shootPower = 1000;
    const float airFriction = 0.5f;
    float velocity;
    CannonBehavior cannonBehavior;
    GameData gameData;
    Rigidbody2D rb;
    public int propellerLeft;
    float distanciaX;
    float startPosX;
    bool monedasSumadas;
    bool haVolado;
    TextMeshProUGUI distanciaText;
    GameObject img;
    bool esperandoPropeller = false;
    #endregion

    void Awake() {
        velocity = 0;
        propellerLeft = 0;
        monedasSumadas = false;
        haVolado = false;
    }
    void Start() {
        cannonBehavior = FindFirstObjectByType<CannonBehavior>();
        gameData = FindFirstObjectByType<GameManager>().data;
        distanciaText = GameObject.Find("DistanciaText").GetComponent<TextMeshProUGUI>();
        img = GameObject.Find("img");

        propellerLeft = gameData.propeller;
        startPosX = transform.position.x;

        rb = this.GetComponent<Rigidbody2D>();
        rb.AddForce(cannonBehavior.shootPoint.up * shootPower * gameData.powerShot);
        print(rb.linearVelocity.magnitude);
        rb.linearDamping = airFriction - gameData.airFriction;

        Camera.main.GetComponent<Follow>().EstablecerObjetivo(this.transform);

    }
    void FixedUpdate() {
        velocity = rb.linearVelocity.magnitude;
        distanciaX = Mathf.Abs(transform.position.x - startPosX);
        distanciaText.text = "Distancia: " + Mathf.FloorToInt(distanciaX).ToString();

        if (!haVolado && velocity > 0.5f) { // Ajusta el umbral según tu juego
            haVolado = true;
        }
        if (haVolado) {

            if (velocity < gameData.MaxVelocity) {
                velocity -= Time.fixedDeltaTime;
            }
            if (velocity <= 0) {
                if (propellerLeft > 0) {
                    Propeller();
                    haVolado = false;
                }
                else if (!monedasSumadas) {
                    gameData.coins += Mathf.FloorToInt(distanciaX / 10);
                    FindFirstObjectByType<UIManager>().ShowResetButton();
                    monedasSumadas = true;
                }
            }
        }

    }

    public void Propeller() {
        if (!esperandoPropeller) {
            StartCoroutine(WaitForScreenClick());
            esperandoPropeller = true;
        }
    }

    public IEnumerator WaitForScreenClick() {
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        print("SSSSS");
        rb.AddForce(new Vector2(0.5f, 1f) * (500 + 100 * Mathf.Pow(gameData.levPropeller, 2)));
        esperandoPropeller = false;
    }
}
