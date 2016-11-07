using UnityEngine;
using System.Collections;

// COMP3064 Assignment 1
// Due Date: Sunday, October 30, 2016
// Instructor: Przemyslaw Pawluk
// Kazuma Sato 100 948 212

public class BkgndPlanetsController : MonoBehaviour {

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

	// Use this for initialization
	void Start() {
		_xBounds = Camera.main.orthographicSize * Camera.main.aspect;
		_yBounds = Camera.main.orthographicSize;
        topPlanetSpeed = 0;
        bottomPlanetSpeed = 0;
        topPlanetInstance = Reset(topPlanet);
        bottomPlanetInstance = Reset(bottomPlanet);
	}
	
	// Update is called once per frame
	void Update() {

        topPlanetInstance.transform.Translate(Vector3.down * topPlanetSpeed);
        bottomPlanetInstance.transform.Translate(Vector3.down * bottomPlanetSpeed);
        
        if(checkBound(topPlanetInstance)) {
            Destroy(topPlanetInstance);
            topPlanetInstance = Reset(topPlanet);
        }
        if(checkBound(bottomPlanetInstance)) { 
            Destroy(bottomPlanetInstance);
            bottomPlanetInstance = Reset(bottomPlanet);
        }
	}

    private GameObject Reset(GameObject planet){
        
        GameObject planetInstance;
        Vector3 spawnPosition = new Vector3(-_xBounds - planet.GetComponent<Renderer>().bounds.size.x / 2, 0, 0);
        Vector3 spawnRotation = new Vector3(0, 0, 90);

        if(planet.tag == "BottomPlanet") {
            bottomPlanetSpeed = Random.value;
            spawnPosition.y = -_yBounds;

        } else if(planet.tag == "TopPlanet") {
            topPlanetSpeed = Random.value;
            spawnPosition.y = _yBounds;
        }

        planetInstance =  Instantiate(planet, spawnPosition, Quaternion.Euler(spawnRotation)) as GameObject;
        planetInstance.transform.parent = gameObject.transform; 
        return planetInstance;
	}
    private bool checkBound(GameObject planet){

        float width = planet.GetComponent<Renderer>().bounds.size.x;
        return planet.transform.position.x - width > _xBounds;
    }
}