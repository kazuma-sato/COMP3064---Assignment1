using UnityEngine;
using System.Collections;

// COMP3064 Assignment 1
// Due Date: Sunday, October 30, 2016
// Instructor: Przemyslaw Pawluk
// Kazuma Sato 100 948 212

public class EnemyController : MonoBehaviour {
	
	[SerializeField]
	private Vector2 speed = Vector2.zero;

	private Transform _transform;
	private Vector2 _currentPosition;
	private float _xBounds;

	//direction: positive when moving up, negative when moving down
	private int direction = 1;

	void Start(){
		_transform = gameObject.GetComponent<Transform>();
		_currentPosition = _transform.position;
		_xBounds = Camera.main.orthographicSize * Camera.main.aspect;
		Reset ();
	}

	void Update(){
		_currentPosition = _transform.position;
		Vector2 currSpeed = new Vector2(speed.x, speed.y * direction);
		_currentPosition += currSpeed;
		_transform.position = _currentPosition;

		//Debug.Log (_xBounds + " " + _currentPosition.y.ToString()); 
		if(_currentPosition.x <= _xBounds) {
			Reset();
		}
	}

	public void Reset(){
		//Debug.Log("Oh shit! Im resetting!!!");
		direction *= -1;
		_currentPosition = new Vector2(-_xBounds, direction * Camera.main.orthographicSize);
		//Debug.Log (_currentPosition.ToString ());
		_transform.position = _currentPosition;
	}
}