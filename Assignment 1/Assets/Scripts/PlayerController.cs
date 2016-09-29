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

	void Start () {
		_transform = gameObject.GetComponent<Transform>();
		_currentPosition = _transform.position;
		_yBounds = Camera.main.orthographicSize;
	}

	void Update () {
		_playerInput = Input.GetAxis("Vertical");
		_currentPosition = _transform.position;
		//move up
		if (_playerInput > 0) {
			_currentPosition += new Vector2 (0, speed);
		}
		//move down
		if (_playerInput < 0) {
			_currentPosition -= new Vector2 (0, speed);
		}
		//fix bounds
		checkBounds ();
		_transform.position = _currentPosition;
	}
	private void checkBounds(){

		if (_currentPosition.y < -1 * _yBounds) {
			_currentPosition.y = -1 * _yBounds;
		}
		if (_currentPosition.y > _yBounds) {
			_currentPosition.y = _yBounds;
		}
	}
}
