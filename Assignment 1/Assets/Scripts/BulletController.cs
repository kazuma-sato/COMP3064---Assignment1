using UnityEngine;
using System.Collections;

// COMP3064 CRN13018 Assignment 1 
// Thursday, November 24, 2016
// Instructor: Przemyslaw Pawluk
// Kazuma Sato 100 948 212 kazuma.sato@georgebrown.ca

public class BulletController : MonoBehaviour {

	// Properties //

	[SerializeField]
	public float speed;
	[SerializeField]
	private GameObject explosion;
	[SerializeField]
	public int damage;

	private Transform _transform;
	private float _xBounds;
	private float _yBounds;

	// Methods //

	void Start() {

		_transform = GetComponent<Transform>();
	}
	
	// Translates the bullet forwards. Distroys them after leaving the Camera.
	void Update () {

		// Get bounds of the Camera.main
		_xBounds = Camera.main.orthographicSize * Camera.main.aspect;
		_yBounds = Camera.main.orthographicSize;

		_transform.Translate(Vector3.up * speed);

		if(Mathf.Abs(transform.position.x) > _xBounds ||
				Mathf.Abs(transform.position.y) > _yBounds){
			Destroy(gameObject);
		}
	}

	//When bullet hits an Enemy, the Enemy takes damage and bullet is destroyed.
	void OnTriggerEnter2D(Collider2D other) {
		
		if (other.gameObject.layer == LayerMask.NameToLayer ("Enemy")) {
			Enemy damagedEnemy = other.gameObject.GetComponent<Enemy> ();
			damagedEnemy.addDamage(damage);
			Instantiate(
					explosion, _transform.position, _transform.rotation);
			if(gameObject.tag != "Beam1") Destroy(gameObject);
            Camera.main.GetComponent<SFXController>().PlaySound(6, _transform.position);
		}
	}

	//Just in case OnTriggerEnter2D() doesn't invoke on enter
	void OnTriggerStay2D(Collider2D other) {

		OnTriggerEnter2D(other);
	}
}
