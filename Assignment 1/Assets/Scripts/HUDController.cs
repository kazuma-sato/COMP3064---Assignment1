using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

// COMP3064 CRN13018 Assignment 1 
// Thursday, November 24, 2016
// Instructor: Przemyslaw Pawluk
// Kazuma Sato 100 948 212 kazuma.sato@georgebrown.ca

public class HUDController : MonoBehaviour {

    [SerializeField]
    private Text healthText;
    [SerializeField]
    private Text shieldText;
    [SerializeField]
    private Text lifeText;
    [SerializeField]
    private Text scoreText;

    private int currentHealth;
    private int currentShield;
    private int currentLife; 
    private static int topScore;
    private static int currentScore;
    private static bool gameOver;
    private Player player;
    private float flashUntil;
    private MenuController menu;
    private SpriteRenderer playerSpriteRenderer;
    private static HUDController Instance;

    // Accessors & Mutators //

    // Stops from TopScoredecrease. Value is saved to PlayerPrefs on change.
    public static int TopScore{

        get { return topScore; }
        set { 
            if(value > topScore) {
                topScore = value;
                PlayerPrefs.SetInt("TopScore", topScore);
            }
        }
    }

    public static int CurrentScore{

        get { return currentScore; }
        set { 
            currentScore = value;
            if(currentScore > topScore) {
                TopScore = currentScore;
            }
        }
    }

    // When this becomes true, starts GameOverCoroutine which makes the player
    // flashe for 5 seconds. 
    public static bool GameOver{

        get { return gameOver; }
        set { 
            gameOver = value;

            if(gameOver) {
                Instance.menu = MenuController.Instance;
                PlayerPrefs.Save();
                Instance.flashUntil = Time.time + 5;
                Instance.StopCoroutine("GameOverCoroutine");
                Instance.StartCoroutine("GameOverCoroutine", gameOver);
                Instance.menu.menuText.text = "GameOver"+
                    "\nScore:" + currentScore.ToString().PadLeft(11) +
                    "\nTop Score: " + topScore.ToString().PadLeft(7);
                Instance.menu.buttonText.text = "Retry?";
                Instance.menu.button.onClick.AddListener(
                    () => {
                        SceneManager.LoadScene("Main");
                    }  
                );
                Instance.menu.enabled = true;
                Instance.menu.StopGame();
            } else {
                Instance.StopCoroutine("GameOverCoroutine");
            }
        }
    }

    // Methods //

    // Initializing properties.
    void Awake() {
        
        Instance = this;
        player = GameObject.Find("Player/Player_ship").GetComponent<Player>();
        menu = GameObject.Find("MenuCanvas").GetComponent<MenuController>();
        playerSpriteRenderer = player.GetComponent<SpriteRenderer>();
        currentScore = 0;
        TopScore = PlayerPrefs.GetInt("TopScore");
    }

    // Updates the text in the Text objects in the HUD, as well keeping track 
    // of the topScore. If Cancel is pressed, the game is paused and the menu,
    // screen is brought up.
    void Update() {
        
        healthText.text = formatStatusText("Health:", player.health);
        shieldText.text = formatStatusText("Shield:", player.shield);
        lifeText.text = formatStatusText("Life:", player.Life);
        scoreText.text =  "Score:".PadRight(10) + currentScore.ToString().PadLeft(11);
         


        if(Input.GetButtonDown("Cancel")) {
            PlayerPrefs.SetInt("TopScore", topScore);
            menuScreen("Paused", "Resume");
        }
	}

    // Pads the strings for status info for continuity.
    string formatStatusText(string key, int value){
        
        return key.PadRight(7) + value.ToString().PadLeft(3);
    }

    // Generates the string for the menuText. Then changes the Text.text for 
    // the button in the menu. Followed by pausing the game.
    void menuScreen(string menuType, string buttonText){

        menu.menuText.text = menuType +
            "\nScore:" + currentScore.ToString().PadLeft(11) +
            "\nTop Score: " + topScore.ToString().PadLeft(7);
        menu.gamePaused = !menu.gamePaused;
        menu.buttonText.text = buttonText;
        menu.StopGame(menu.gamePaused);
    }

    IEnumerator GameOverCoroutine(bool gameOver){

        while(gameOver && flashUntil > Time.time) {
            playerSpriteRenderer.enabled = !playerSpriteRenderer.enabled;
            yield return new WaitForSeconds(0.2f);
        }
    }
}
