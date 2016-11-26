using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// COMP3064 CRN13018 Assignment 1 
// Thursday, November 24, 2016
// Instructor: Przemyslaw Pawluk
// Kazuma Sato 100 948 212 kazuma.sato@georgebrown.ca

public class MenuController : MonoBehaviour {

    // Properties //

    [SerializeField]
    public GameObject[] gamePlayGameObjects;

    public Button button;
    public Text buttonText;
    public Text menuText;
    public bool gamePaused;
    public string menuState;
    public static MenuController Instance;
     
    // Methods //

    // Initializes properties  
    void Awake() {

        if(Instance == null) {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        } else if(Instance != this) {
            Destroy(gameObject);
        }
        button = GetComponentInChildren<Button>();
        buttonText = button.GetComponentInChildren<Text>();
        menuText = GetComponentInChildren<Text>();
        gamePaused = false;
        gamePlayGameObjects = new GameObject[] { GameObject.Find("HUD"),
                                                 GameObject.Find("Gameplay")};
        button.onClick.AddListener(
            () => {
                gamePaused = !gamePaused;
                StopGame(false);
            });
    }


    // Starts the game with a Title menu with a top score.
	void Start() {

        gamePaused = true;
        menuText.text = "Space Blaster\nTop Score: " + 
                HUDController.TopScore.ToString().PadLeft(7);
        buttonText.text = "Start";
        StopGame();
	}
	
    // Listens for buttons to exit the menu.
	void Update() {

        if(Input.GetButtonDown("Submit") ||
               (menuText.text == "Paused" && 
                Input.GetButtonDown("Cancel"))) {
            StopGame(false);

        }
	}

    // Pauses or resumes the game by changing the Time.timeScale.
    public void StopGame(bool stop = true){

        gameObject.SetActive(stop);
        foreach(GameObject obj in gamePlayGameObjects) {
            obj.SetActive(!stop);
        }
        Time.timeScale = stop ? 0 : 1; 
    }
}