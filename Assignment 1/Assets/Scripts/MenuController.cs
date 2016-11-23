using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

    public GameObject[] inGameGameObjects;
    public Text menuText;
    public Button button;
    public Text buttonText;
    public bool gamePaused;
    public static MenuController Instance;
    public string menuState;

    void Awake() {

        if(Instance == null) {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if(Instance != this) {
            Destroy(gameObject);
        }
        button = GetComponentInChildren<Button>();
        buttonText = button.GetComponentInChildren<Text>();
        menuText = GetComponentInChildren<Text>();
        gamePaused = false;
        inGameGameObjects = new GameObject[]{
            GameObject.Find("HUD"),
            GameObject.Find("Level")
        };
        button.onClick.AddListener(
            () => {
                gamePaused = !gamePaused;
                StopGame(false);
            });
    }

	void Start () {

        gamePaused = true;
        menuText.text = "Space Blaster\nTop Score: " + 
                HUDController.TopScore.ToString().PadLeft(7);
        buttonText.text = "Start";
        StopGame();
	}
	
	void Update () {
        
        if(Input.GetButtonDown("Submit") ||
               (menuText.text == "Paused" && Input.GetButtonDown("Cancel"))) {
            StopGame(false);
        }
	}

    public void StopGame(bool stop = true){

        gameObject.SetActive(stop);
        foreach(GameObject obj in inGameGameObjects) {
            obj.SetActive(!stop);
        }
        Time.timeScale = stop ? 0 : 1; 
    }
}
