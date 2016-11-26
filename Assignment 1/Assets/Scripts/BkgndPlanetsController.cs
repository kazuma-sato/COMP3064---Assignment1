using UnityEngine;
using System.Collections;

// COMP3064 CRN13018 Assignment 1 
// Thursday, November 24, 2016
// Instructor: Przemyslaw Pawluk
// Kazuma Sato 100 948 212 kazuma.sato@georgebrown.ca

public class BkgndPlanetsController : MonoBehaviour {

    // Properties //

	[SerializeField]
	private float topPlanetSpeed;
    [SerializeField]
    private float bottomPlanetSpeed;
    [SerializeField]
    private GameObject topPlanet;
    [SerializeField]
    private GameObject bottomPlanet;

    private GameObject topPlanetInstance;
    private GameObject bottomPlanetInstance;
	private float _xBounds;
	private float _yBounds;

    // Methods //

    void Awake(){

        topPlanetSpeed = 0;
        bottomPlanetSpeed = 0;
    }

	void Start() {

        // Getting the bounds of the camera.
        _xBounds = Camera.main.orthographicSize * Camera.main.aspect;
        _yBounds = Camera.main.orthographicSize;

        // Instanciating and inicialzing the background planet sprites.
        topPlanetInstance = Reset(topPlanet);
        bottomPlanetInstance = Reset(bottomPlanet);
	}
	
    // Moves the planets from right to left. If the planets reach the right camera
    // bound, the planet is distroyed and reset.
	void Update() {

        topPlanetInstance.transform.Translate(Vector3.down * topPlanetSpeed);
        bottomPlanetInstance.transform.Translate(Vector3.down * bottomPlanetSpeed);

        // Getting the bounds of the camera.
        _xBounds = Camera.main.orthographicSize * Camera.main.aspect;
        _yBounds = Camera.main.orthographicSize;
        
        if(checkBound(topPlanetInstance)) {
            Destroy(topPlanetInstance);
            topPlanetInstance = Reset(topPlanet);
        }
        if(checkBound(bottomPlanetInstance)) { 
            Destroy(bottomPlanetInstance);
            bottomPlanetInstance = Reset(bottomPlanet);
        }
	}

    // Instantiate a new planet, on the left bound of the Camera.main,
    // sets a random speed, and returns planet GameObject instance.
    private GameObject Reset(GameObject planet){
        
        GameObject planetInstance;
        Vector3 spawnPosition = new Vector3(
            -_xBounds - planet.GetComponent<Renderer>().bounds.size.x / 2, 0, 0);
        Vector3 spawnRotation = new Vector3(0, 0, 90);

        if(planet.tag == "BottomPlanet") {
            bottomPlanetSpeed = Random.value;
            spawnPosition.y = -_yBounds;
        } else if(planet.tag == "TopPlanet") {
            topPlanetSpeed = Random.value;
            spawnPosition.y = _yBounds;
        }

        planetInstance = Instantiate(planet, 
            spawnPosition, Quaternion.Euler(spawnRotation)) as GameObject;
        planetInstance.transform.parent = gameObject.transform; 
        return planetInstance;
	}

    // Returns if the parameter GameObject is the out of bounds of the Camera.main 
    // the width of the GameObject.
    private bool checkBound(GameObject planet){

        float width = planet.GetComponent<Renderer>().bounds.size.x;
        return planet.transform.position.x - width > _xBounds;
    }
}