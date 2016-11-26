using UnityEngine;
using System.Collections;

// COMP3064 CRN13018 Assignment 1 
// Thursday, November 24, 2016
// Instructor: Przemyslaw Pawluk
// Kazuma Sato 100 948 212 kazuma.sato@georgebrown.ca

public class SpawnController : MonoBehaviour {

	// Properties //

	[SerializeField]
    public float spawnFrequency;
	[SerializeField]
	private float maxSpawnFrequency;
	[SerializeField]
	private float spawnFrequencyIncreaceFactor;
	[SerializeField]
	private GameObject spawnObject;

	private Vector2 _spawnPosition;
    private Quaternion _spawnRotation;
	private float _xBounds;
	private float _yBounds;
	private float timer;

	// Methods //

	void Start() {

		// Measuring the bounds of the camera
		_xBounds = Camera.main.orthographicSize * Camera.main.aspect;
		_yBounds = Camera.main.orthographicSize;

		// set timer
        spawn();
		timer = Time.time + spawnFrequency;
	}
	
	void Update() {

		// Measuring the bounds of the camera
		_xBounds = Camera.main.orthographicSize * Camera.main.aspect;
		_yBounds = Camera.main.orthographicSize;
		
		if(timer <= Time.time) {
			spawn();
			if(spawnFrequency > maxSpawnFrequency){
				spawnFrequency *= spawnFrequencyIncreaceFactor;
			} 
            if(spawnFrequency < maxSpawnFrequency){
				spawnFrequency = maxSpawnFrequency;
			}
			timer = Time.time + spawnFrequency;
		}
	}

	void spawn() {

		// Spawn position is determined by, being on the left bound of the 
		// Camera.main but with a random Y coordate between, the top and bottom 
		// bounds of Camera.main
		_spawnPosition = new Vector2(-_xBounds, 
			(Random.value * _yBounds * 2) - _yBounds);

		// Spawn rotation is also random but pointing towards right bound. 
        _spawnRotation = Quaternion.Euler(new Vector3(0, 0, (Random.value * 90) + 225));

        GameObject spawnedGameObject = Instantiate(spawnObject, _spawnPosition, _spawnRotation) as GameObject;
        spawnedGameObject.transform.parent = gameObject.transform;
	}
}