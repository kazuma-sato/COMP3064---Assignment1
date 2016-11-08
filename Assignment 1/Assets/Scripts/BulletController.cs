using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {

	[SerializeField]
	public float speed;

	[SerializeField]
	private GameObject explosion;

	[SerializeField]
	public float damage;

	private Transform _transform;

	private float _xBounds;
	private float _yBounds;

	// Use this for initialization
	void Awake() {

		_xBounds = Camera.main.orthographicSize * Camera.main.aspect;
		_yBounds = Camera.main.orthographicSize;

		_transform = GetComponent<Transform> ();
	}
	
	// Update is called once per frame
	void Update () {

		_transform.Translate(Vector3.up * speed);
		if(Mathf.Abs(transform.position.x) > _xBounds ||
				Mathf.Abs (transform.position.y) > _yBounds)
			Destroy(gameObject);
	}

	//When bullet hits an Enemy, the Enemy takes damage and bullet is destroyed.
	void OnTriggerEnter2D(Collider2D other) {
		
		if (other.gameObject.layer == LayerMask.NameToLayer ("Enemy")) {
			Enemy damagedEnemy = other.gameObject.GetComponent<Enemy> ();
			damagedEnemy.addDamage(damage);
			Instantiate (
				explosion, _transform.position, _transform.rotation);
			if(gameObject.tag != "Beam1")
				Destroy (gameObject);
            Camera.main.GetComponent<SFXController>().PlaySound(6, _transform.position);
		}
	}

	//Just in case OnTriggerEnter2D() doesn't invoke on enter
	void OnTriggerStay2D(Collider2D other) {

		OnTriggerEnter2D(other);
	}
}
