using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {
    FloorCreation floorCreation;
    #region VariablesMainMenu
    [SerializeField] GameObject settingsPanel;
    [SerializeField] GameObject upgradesPanel;
    CannonBehavior cannonBehavior;
    [SerializeField] Button[] buttons;
    [SerializeField] TextMeshProUGUI coinsText;
    GameManager gameManager;
    #endregion

    #region VariablesGame
    [SerializeField] Button shootButton;
    [SerializeField] Button resetButton;
    #endregion
    void OnEnable() {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        if (scene.name == "Game") {
            shootButton = GameObject.Find("ShootButton").GetComponent<Button>();
            resetButton = GameObject.Find("ResetButton").GetComponent<Button>();



            cannonBehavior = FindFirstObjectByType<CannonBehavior>();

            shootButton.onClick.AddListener(() => ShootButton(shootButton, cannonBehavior.shootPoint, cannonBehavior.meal));
            resetButton.onClick.AddListener(() => EndShot());
            resetButton.gameObject.SetActive(false);
        }
        else if (scene.name == "MainMenu") {
            settingsPanel = GameObject.Find("SettingsPanel");
            upgradesPanel = GameObject.Find("UpgradesPanel");

            buttons = new Button[5];

            gameManager = FindFirstObjectByType<GameManager>();

            cannonBehavior = FindFirstObjectByType<CannonBehavior>();
            buttons[0] = GameObject.Find("PlayButton").GetComponent<Button>();
            buttons[0].onClick.AddListener(() => PlayButton());
            buttons[1] = GameObject.Find("UpgradesButton").GetComponent<Button>();
            buttons[1].onClick.AddListener(() => UpgradesButton());
            buttons[2] = GameObject.Find("SettingsButton").GetComponent<Button>();
            buttons[2].onClick.AddListener(() => SettingsButton());
            buttons[3] = GameObject.Find("ReturnUpgrades").GetComponent<Button>();
            buttons[3].onClick.AddListener(() => CloseUpgrade());
            buttons[4] = GameObject.Find("ReturnSettings").GetComponent<Button>();
            buttons[4].onClick.AddListener(() => CloseSettings());

            coinsText = GameObject.Find("CoinsText").GetComponent<TextMeshProUGUI>();
            UpdateCoinsText(gameManager.data.coins);

            settingsPanel.SetActive(false);
            upgradesPanel.SetActive(false);

        }
    }

    #region MainMenuFunctions
    public void PlayButton() {
        SceneManager.LoadScene("Game");
    }
    public void SettingsButton() {
        settingsPanel.SetActive(true);
        for (int i = 0; i < 3; i++) {
            buttons[i].gameObject.SetActive(false);
        }
    }
    public void CloseSettings() {
        for (int i = 0; i < 3; i++) {
            buttons[i].gameObject.SetActive(true);
        }
        settingsPanel.SetActive(false);
    }
    public void UpgradesButton() {
        upgradesPanel.SetActive(true);
        for (int i = 0; i < 3; i++) {
            buttons[i].gameObject.SetActive(false);
        }
    }
    public void CloseUpgrade() {
        for (int i = 0; i < 3; i++) {
            buttons[i].gameObject.SetActive(true);
        }
        upgradesPanel.SetActive(false);
    }
    public void UpdateCoinsText(int coins) {
        coinsText.text = "Tuna: " + coins.ToString();
    }
    #endregion

    #region GameFunctions
    public void ShootButton(Button shootButton, Transform shootPoint, GameObject meal) {
        shootButton.gameObject.SetActive(false);
        GameObject newMeal = Instantiate(meal, shootPoint.position, Quaternion.identity);
        FindFirstObjectByType<FloorCreation>().meal = newMeal.transform;
        FindFirstObjectByType<FloorBehavior>().meal = newMeal.transform;

    }
    public void EndShot() {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
    public void ShowResetButton() {
        resetButton.gameObject.SetActive(true);
    }
    #endregion


}
