using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class HUDController : MonoBehaviour {

    private int currentHealth;
    private int currentShield;
    private int currentLife; 
    private static int topScore;
    public static int currentScore;

    [SerializeField]
    private Text healthText;
    [SerializeField]
    private Text shieldText;
    [SerializeField]
    private Text lifeText;
    [SerializeField]
    private Text scoreText;

    private bool gameOver;
    private Player player;
    private float flashUntil;
    public MenuController menu;
    private SpriteRenderer playerSpriteRenderer;

    public static int TopScore{
        get { return topScore; }
        set { 
            if (value > topScore){
                topScore = value;
                PlayerPrefs.SetInt("TopScore", topScore);
            }
        }
    }

    public bool GameOver{
        get { return gameOver; }
        set { 
            gameOver = value;

            if(gameOver) {
                flashUntil = Time.time + 5;
                StopCoroutine("GameOverCoroutine");
                StartCoroutine("GameOverCoroutine", gameOver);
                MenuController.Instance.menuText.text = "GameOver";
                MenuController.Instance.buttonText.text = "Retry?";
                MenuController.Instance.enabled = true;
                SceneManager.LoadScene("Main");
            }
            else {
                StopCoroutine("GameOverCoroutine");
            }
        }
    }

    void Awake() {

        player = GameObject.Find("Player/Player_ship").GetComponent<Player>();
        menu = GameObject.Find("MenuCanvas").GetComponent<MenuController>();
        playerSpriteRenderer = player.GetComponent<SpriteRenderer>();
        currentScore = 0;
        TopScore = PlayerPrefs.GetInt("TopScore");
    }
	
    void Update() {
        
        healthText.text = formatStatusText("Health:", player.health);
        shieldText.text = formatStatusText("Shield:", player.shield);
        lifeText.text = formatStatusText("Life:", player.Life);
        scoreText.text =  "Score:".PadRight(10) + currentScore.ToString().PadLeft(11);
         
        if(currentScore > topScore) {
            topScore = currentScore;
        }

        if(Input.GetButtonDown("Cancel")) {
            menuScreen("Paused", "Resume");
        }
	}

    string formatStatusText(string key, int value){
        
        return key.PadRight(7) + value.ToString().PadLeft(3);
    }
    void menuScreen(string menuType, string buttonText){

        menu.menuText.text = menuType +
            "\nScore:" + currentScore.ToString().PadLeft(11) +
            "\nTop Score: " + topScore.ToString().PadLeft(7);
        menu.gamePaused = !menu.gamePaused;
        menu.buttonText.text = buttonText;
        menu.StopGame(menu.gamePaused);
    }

    IEnumerator GameOverCoroutine(bool gameOver){

        while(gameOver) {
            if(flashUntil > Time.time) {
                playerSpriteRenderer.enabled = !playerSpriteRenderer.enabled;
                yield return new WaitForSeconds(0.2f);
            } else {
                GameObject enemies = GameObject.Find("Enemies");
                List<GameObject> children = new List<GameObject>();
                foreach(Transform child in enemies.transform) {
                    children.Add(child.gameObject);
                }
                children.ForEach(child => Destroy(child));

                foreach(SpawnController SC in enemies.GetComponents<SpawnController>()) {
                    SC.spawnFrequency = 2;
                }
            }
        }
    }
}
