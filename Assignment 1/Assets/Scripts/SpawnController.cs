using UnityEngine;
using System.Collections;

public class SpawnController : MonoBehaviour {

	[SerializeField]
	private float spawnFrequency;

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


	void Start() {

		// Measuring the bounds of the camera
		_xBounds = Camera.main.orthographicSize * Camera.main.aspect;
		_yBounds = Camera.main.orthographicSize;

		// set timer
        spawn();
		timer = Time.time + spawnFrequency;
	}
	
	// Update is called once per frame
	void Update() {
		
		if(timer <= Time.time) {
			spawn();
			if (spawnFrequency < maxSpawnFrequency){
				spawnFrequency *= spawnFrequencyIncreaceFactor;
			} else if (spawnFrequency > maxSpawnFrequency){
				spawnFrequency = maxSpawnFrequency;
			}
			timer = Time.time + spawnFrequency;
		}
	}

	void spawn() {

		_spawnPosition = new Vector2(-_xBounds, (Random.value * _yBounds * 2) - _yBounds);
        _spawnRotation = Quaternion.Euler(new Vector3(0, 0, (Random.value * 90) + 225));
        GameObject spawnedGameObject = Instantiate(spawnObject, _spawnPosition, _spawnRotation) as GameObject;
        spawnedGameObject.transform.parent = gameObject.transform;
	}
}