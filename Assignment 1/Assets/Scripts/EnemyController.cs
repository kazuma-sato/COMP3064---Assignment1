using UnityEngine;
using System.Collections;

// COMP3064 CRN13018 Assignment 1 
// Thursday, November 24, 2016
// Instructor: Przemyslaw Pawluk
// Kazuma Sato 100 948 212 kazuma.sato@georgebrown.ca

public class EnemyController : MonoBehaviour {

	// Properties //
	
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

	// Methods //

	void Start(){

		// Get the bounds of the Camera.
		_xBounds = Camera.main.orthographicSize * Camera.main.aspect;
		_yBounds = Camera.main.orthographicSize;

		_transform = gameObject.GetComponent<Transform>();
		_currentPosition = _transform.position;

		bulletTimer = Time.time + fireRate;
	}

	// Translates the enemy forward at a random speed between 0 and 
	// speedCurrentMax (inclusive).
	void Update(){

		_currentPosition = _transform.position;
		_transform.Translate(Vector3.up * Random.value * speedCurrentMax);

		//Get the bounds of the Camera.
		_xBounds = Camera.main.orthographicSize * Camera.main.aspect;
		_yBounds = Camera.main.orthographicSize;

		// Shoots bullets at a set interval.
		if(bulletTimer <= Time.time){
			Instantiate(bullet, _currentPosition, _transform.rotation);
			bulletTimer = Time.time + fireRate;
		}

		// Destroys enemies when they go out of bounds.
		if(_currentPosition.x > _xBounds ||
		   Mathf.Abs(_currentPosition.y) > _yBounds){
			Destroy(gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D other) {

		// When hit by the player, gameObject is distroyed, the player takes damage, 
		// and an explosion animation instantiate with a sound effect.
		if (other.gameObject.layer == LayerMask.NameToLayer("Player") && 
			    other.tag == "Ship") {
			other.GetComponent<Player>().addDamage(collisionDamage);
			Instantiate(explosion, _transform.position, _transform.rotation);
			Destroy(gameObject);
            Camera.main.GetComponent<SFXController>().PlaySound(2, _transform.position);
        }

        // When hit by a bullet, an explosion animates with a sound effect.
        if(other.gameObject.layer == LayerMask.NameToLayer("Player") &&
                other.tag == "bullet") {
            Instantiate(explosion, _transform.position, _transform.rotation);
            Camera.main.GetComponent<SFXController>().PlaySound(6, _transform.position);
        }
	}

	// Just in case OnTriggerEnter2D() doesn't invoke on enter.
	void OnTriggerStay2D(Collider2D other){
        
		OnTriggerEnter2D(other);
	}
}