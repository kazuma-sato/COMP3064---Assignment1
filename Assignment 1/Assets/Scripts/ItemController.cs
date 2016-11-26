using UnityEngine;
using System.Collections;

// COMP3064 CRN13018 Assignment 1 
// Thursday, November 24, 2016
// Instructor: Przemyslaw Pawluk
// Kazuma Sato 100 948 212 kazuma.sato@georgebrown.ca

public class ItemController : MonoBehaviour {

	// Properties //

	[SerializeField]
	private float speed;
    [SerializeField]
    private GameObject hitEffect;

	private Transform _transform;
	private Vector2 _currentPosition;
	private float _xBounds;
	private float _yBounds;

	// Methods //

	void Awake() {
        
        // Gets the bounds of the Camera.main
        _xBounds = Camera.main.orthographicSize * Camera.main.aspect;
        _yBounds = Camera.main.orthographicSize;

		_transform = gameObject.GetComponent<Transform>();
		_currentPosition = _transform.position;
        transform.rotation = Quaternion.Euler(Vector3.zero);
	}
        
    // Moves items from left to right.
	void Update() {

        _currentPosition = _transform.position;
        transform.Translate(Vector3.right * speed);

		// Gets the bounds of the Camera.main
        _xBounds = Camera.main.orthographicSize * Camera.main.aspect;
        _yBounds = Camera.main.orthographicSize;

		// Check to if enemy is out of bounds
		if(_currentPosition.x > _xBounds ||
		        Mathf.Abs(_currentPosition.y) > _yBounds)
		    Destroy(gameObject);
	}

	// This method determins which item effect becomes applied to the player 
	// as well as adds, 500 points, 
	void OnTriggerEnter2D(Collider2D other) {

        if(other.gameObject.layer == LayerMask.NameToLayer("Player") && 
        		other.tag == "Ship") {
			Player player = other.gameObject.GetComponent<Player>();
            if(gameObject.tag == "ShieldItem") {
                shieldItemEffect(player);
            }else if(gameObject.tag == "WeaponUpgradeItem") {
                weaponUpgradeEffect(player);
            }
            HUDController.CurrentScore += 500;
            Instantiate(hitEffect, transform);
            Destroy(gameObject);
		}
	}

	// Just in case OnTriggerEnter2D() doesn't invoke on enter
	void OnTriggerStay2D(Collider2D other){

		OnTriggerEnter2D(other);
	} 

    // Adds 50 to Shield and plays a sound.
    private void shieldItemEffect(Player player){
        
        player.addShield(50);
        Camera.main.GetComponent<SFXController>().PlaySound(3, _transform.position);
	}

	// Changes player.weapon to Beam as well as sets the timer variable for 
	// the duration of the item effect.
	private void weaponUpgradeEffect (Player player){
        
		player.weapon = "Beam1";
        player.GetComponentInParent<Weapon>().beamEffectUntil = Time.time + 4;
        Camera.main.GetComponent<SFXController>().PlaySound(4, _transform.position);
	}
}