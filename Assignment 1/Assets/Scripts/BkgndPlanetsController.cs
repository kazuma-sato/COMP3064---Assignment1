using UnityEngine;
using System.Collections;

public class BkgndPlanetsController : MonoBehaviour {

	[SerializeField]
	private float speed;

	private Transform _transform;
	private Vector2 _currentPosition;
	private float _xBounds;
	private float _yBounds;

	// Use this for initialization
	void Start() {
		_transform = gameObject.GetComponent<Transform>();
		_currentPosition = _transform.position;
		_xBounds = Camera.main.orthographicSize * Camera.main.aspect;
		_yBounds = Camera.main.orthographicSize;
		Reset ();
	}
	
	// Update is called once per frame
	void Update() {
		
		_currentPosition = _transform.position;

		_currentPosition += new Vector2(speed, 0);
		_transform.position = _currentPosition;

		if (_currentPosition.x > _xBounds) 
			Reset ();
	}

	private void Reset(){
		
		float direction = (Random.value >= 0.5)? 1 : -1;
		speed = Random.value * 0.5f;

		_transform.localScale = new Vector3 (speed * 4, speed * 4, 1);

		_transform.Rotate(0,0,
			_transform.rotation.z + ((direction == 1) ? 180 : 0));
		_transform.position = _currentPosition;
		_currentPosition = new Vector2(
			Camera.main.gameObject.transform.position.x - _xBounds, 
			direction * _yBounds);
		_transform.position = _currentPosition;
	}
}