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
	private GameObject bullet;
	[SerializeField]
	private float fireRate;
	[SerializeField]
	private float bulletDamage;
	[SerializeField]
	public int collisionDamage;
	[SerializeField]
	public GameObject explosion;

	private Transform _transform;
	private Vector2 _currentPosition;
	private float _xBounds;
	private float _yBounds;
	private float bulletTimer;

	void Start(){

		_transform = gameObject.GetComponent<Transform>();
		_currentPosition = _transform.position;

		//Get the bounds of the Camera
		_xBounds = Camera.main.orthographicSize * Camera.main.aspect;
		_yBounds = Camera.main.orthographicSize;
		bulletTimer = Time.time + fireRate;
	}

	void FixedUpdate(){

		_currentPosition = _transform.position;
		_transform.Translate(Vector3.up * Random.value * speedCurrentMax);

		if(bulletTimer <= Time.time){
			Instantiate(bullet, _currentPosition, _transform.rotation);
			bulletTimer = Time.time + fireRate;
		}

		//Check to if enemy is out of bounds
		if(_currentPosition.x > _xBounds ||
		   Mathf.Abs(_currentPosition.y) > _yBounds)
			Destroy(gameObject);
	}

	//When hit by the player, the player takes damage, explosion animates and enemy resets.
	void OnTriggerEnter2D(Collider2D other) {

		if (other.gameObject.layer == LayerMask.NameToLayer ("Player") && 
			    other.tag == "Ship") {
			other.GetComponent<Player> ().addDamage (collisionDamage);
			Instantiate (
				explosion, _transform.position, _transform.rotation);
			Destroy(gameObject);
            Camera.main.GetComponent<SFXController>().PlaySound(2, _transform.position);
        }

        if(other.gameObject.layer == LayerMask.NameToLayer("Player") &&
                other.tag == "bullet") {
            Instantiate (
                explosion, _transform.position, _transform.rotation);
            Camera.main.GetComponent<SFXController>().PlaySound(6, _transform.position);
        }
	}

	//Just in case OnTriggerEnter2D() doesn't invoke on enter
	void OnTriggerStay2D(Collider2D other){
        
		OnTriggerEnter2D(other);
	}
}