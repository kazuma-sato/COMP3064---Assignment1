using UnityEngine;
using System.Collections;

// COMP3064 Assignment 1
// Due Date: Sunday, October 30, 2016
// Instructor: Przemyslaw Pawluk
// Kazuma Sato 100 948 212

public class PlayerController : MonoBehaviour {

	[SerializeField]
	private float speed;

	private Transform _transform;
	private Vector2 _currentPosition;
	private float _playerInput;
	private float _yBounds;

	void Start(){
		_transform = gameObject.GetComponent<Transform>();
		_currentPosition = _transform.position;
		_yBounds = Camera.main.orthographicSize;
	}

	void Update(){

		_playerInput = Input.GetAxis ("Vertical");
		_currentPosition = _transform.position;


		//Rotate Player towards mouse pointer
		transform.rotation = Quaternion.LookRotation(Vector3.forward, 
			Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);

		//move up
		if (_playerInput > 0) 
			_currentPosition += new Vector2 (0, speed);
		//move down
		if (_playerInput < 0) {
			_currentPosition -= new Vector2 (0, speed);
		}

		//fix bounds
		if(_currentPosition.y > _yBounds)
			_currentPosition.y = _yBounds;
		if(_currentPosition.y < -_yBounds)
			_currentPosition.y = -_yBounds;

		_transform.position = _currentPosition;
	}
}
