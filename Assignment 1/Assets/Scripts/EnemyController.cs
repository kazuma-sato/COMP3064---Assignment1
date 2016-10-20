using UnityEngine;
using System.Collections;

// COMP3064 Assignment 1
// Due Date: Sunday, October 30, 2016
// Instructor: Przemyslaw Pawluk
// Kazuma Sato 100 948 212

public class EnemyController : MonoBehaviour {
	
	[SerializeField]
	private Vector2 speed;

	[SerializeField]
	private float speedCurrentMax = 0.2f;

	[SerializeField]
	private float speedIncreaseFactor = 1.01f;

	[SerializeField]
	private float speedGameMax = 5f;

	private Transform _transform;
	private Vector2 _currentPosition;
	private float _xBounds;
	private float _yBounds;

	//direction: positive when moving up, negative when moving down
	private int direction = 1;

	void Start(){

		_transform = gameObject.GetComponent<Transform>();
		_currentPosition = _transform.position;

		//Get the bounds of the Camera
		_xBounds = Camera.main.orthographicSize * Camera.main.aspect;
		_yBounds = Camera.main.orthographicSize;
		Reset ();
	}

	void Update(){

		_currentPosition = _transform.position;
		Vector2 currSpeed = new Vector2(speed.x, speed.y * -direction);
		_currentPosition += currSpeed;
		_transform.position = _currentPosition;

		//Check to if enemy is out of bounds
		if(_currentPosition.x > _xBounds ||
				Mathf.Abs( _currentPosition.y) > _yBounds) 
			Reset();
	}

	public void Reset(){

		//Randomly selects if enemy is coming from top or bottom
		direction *= (Random.value >= 0.5)? 1 : -1;

		//Randomly changes speed but increases possible speed as game progresses
		if(speedCurrentMax * speedIncreaseFactor < speedGameMax)
			speedCurrentMax *= speedIncreaseFactor;
		speed = new Vector2(speedCurrentMax * Random.value,
			speedCurrentMax * Random.value);

		_currentPosition = new Vector2(
			Camera.main.gameObject.transform.position.x - _xBounds, 
			direction * _yBounds);
		_transform.position = _currentPosition;
	}
}