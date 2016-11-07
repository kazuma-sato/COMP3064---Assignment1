using UnityEngine;
using System.Collections;

public class ItemController : MonoBehaviour {

	[SerializeField]
	private float speed;

    [SerializeField]
    private GameObject hitEffect;

	private Transform _transform;
	private Vector2 _currentPosition;
	private float _xBounds;
	private float _yBounds;

	// Use this for initialization
	void Awake () {
        
		_transform = gameObject.GetComponent<Transform>();
		_currentPosition = _transform.position;

		//Get the bounds of the Camera
		_xBounds = Camera.main.orthographicSize * Camera.main.aspect;
		_yBounds = Camera.main.orthographicSize;
        transform.rotation = Quaternion.Euler(Vector3.zero);
	}

	// Update is called once per frame
	void Update () {

        _currentPosition = _transform.position;
        transform.Translate(Vector3.right * speed);

		//Check to if enemy is out of bounds
		if (_currentPosition.x > _xBounds ||
		   Mathf.Abs (_currentPosition.y) > _yBounds)
			Destroy (gameObject);
	}

	void OnTriggerEnter2D(Collider2D other) {

        Debug.Log("The item hit " + LayerMask.LayerToName(other.gameObject.layer));
        Debug.Log(gameObject.tag + "item effect should happen: " + (other.gameObject.layer == LayerMask.NameToLayer ("Player") && other.tag == "Ship"));
		if (other.gameObject.layer == LayerMask.NameToLayer ("Player") && other.tag == "Ship") {
			Player player = other.gameObject.GetComponent<Player> ();
			if (gameObject.tag == "ShieldItem")
				shieldItemEffect(player);
			if (gameObject.tag == "WeaponUpgradeItem")
				weaponUpgradeEffect(player);
            Instantiate(hitEffect, transform);
            Destroy(gameObject);
		}
	}

	//Just in case OnTriggerEnter2D() doesn't invoke on enter
	void OnTriggerStay2D(Collider2D other){

		OnTriggerEnter2D(other);
	} 

    private void shieldItemEffect(Player player){
        
		player.addShield(50f);
	}

	private void weaponUpgradeEffect (Player player){
        
		player.weapon = "Beam1";
        player.GetComponentInParent<Weapon>().beamEffectUntil = Time.time + 4;
	}
}