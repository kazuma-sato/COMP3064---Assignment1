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

	void FixedUpdate(){
		_playerInput = Input.GetAxis("Vertical");
		_currentPosition = _transform.position;

		transform.rotation = Quaternion.FromToRotation(
			GameObject.FindGameObjectWithTag("Player").transform.position
			, Input.mousePosition
		);
		// Camera.main.ScreenToWorldPoint (Input.mousePosition) 
		/*
		Quaternion _qRotation = Quaternion.LookRotation(
			(_transform.position + Camera.main.ScreenToWorldPoint(Input.mousePosition))
			, new Vector3(,1,0)
		);
		*/

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

		//point player towards pointer
		//Debug.Log(_transform.rotation.ToString() + "  rotating:" + _qRotation.ToString());
		//_transform.rotation = _qRotation;
		//_transform.eulerAngles = new Vector3 (0, 0, _transform.eulerAngles.z);
	}
	private void checkBounds(){

		if (_currentPosition.y < -1 * _yBounds) {
			_currentPosition.y = -1 * _yBounds;
		}
		if (_currentPosition.y > _yBounds){
			_currentPosition.y = _yBounds;
		}
	}
}
