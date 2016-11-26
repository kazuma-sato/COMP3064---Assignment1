using UnityEngine;
using System.Collections;

// COMP3064 CRN13018 Assignment 1 
// Thursday, November 24, 2016
// Instructor: Przemyslaw Pawluk
// Kazuma Sato 100 948 212 kazuma.sato@georgebrown.ca

public class PlayerController : MonoBehaviour {

    // Properties //

	[SerializeField]
	private float speed;

	private Transform _transform;
	private Vector2 _currentPosition;
	private float _playerInputVertical;
	private float _playerInputHorizontal;
	private float _xBounds;
	private float _yBounds;
	private Player	playerInstance;
    private float respawnTime;
    private SpriteRenderer spriteRenderer;
    private bool isDead;

    // Accessors & Mutators //

    // When this value becomes true, Player is moved to the center of the screen,
    // DeathCoroutine is started, and stops taking damage.
    public bool IsDead{
        get { return isDead; }
        set { 
            isDead = value;

            if(value) {
                respawnTime = Time.time + 4;
                transform.position = Vector3.zero;
                playerInstance.godMode = true;
                StopCoroutine("DeathCoroutine");
                StartCoroutine("DeathCoroutine", isDead);
            } else {
                StopCoroutine("DeathCoroutine");
            }
        }
    }

    // Methods //

	void Awake() {
		
		_transform = gameObject.GetComponent<Transform>();
		_currentPosition = _transform.position;
        playerInstance = gameObject.GetComponent<Player>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
	}

    // Moves Player based on user input
	void Update() {

        _xBounds = Camera.main.orthographicSize * Camera.main.aspect;
        _yBounds = Camera.main.orthographicSize;
        _playerInputVertical = Input.GetAxis("Vertical");
        _playerInputHorizontal = Input.GetAxis("Horizontal");
        _currentPosition = _transform.position;

        //Rotate Player towards mouse pointer
        transform.rotation = Quaternion.LookRotation(
            Vector3.forward, 
            Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);

        //move up
        if(_playerInputVertical > 0)
            _currentPosition += new Vector2(0, speed);

        //move down
        if(_playerInputVertical < 0) {
            _currentPosition -= new Vector2(0, speed);
        }

        //move right
        if(_playerInputHorizontal > 0) {
            _currentPosition += new Vector2(speed, 0);
        }

        //move left
        if(_playerInputHorizontal < 0) {
            _currentPosition -= new Vector2(speed, 0);
        }

        //fix bounds
        if(Mathf.Abs(_currentPosition.y) > _yBounds) {
            _currentPosition.y = (_currentPosition.y > 0) ? _yBounds : -_yBounds;
        }
        if(Mathf.Abs(_currentPosition.x) > _xBounds) {
            _currentPosition.x = (_currentPosition.x > 0) ? _xBounds : -_xBounds;
        }
        _transform.position = _currentPosition;

    }

    // Player_ship flashes until respawnTime. 
    // When spawnTime is reached, the player starts taking damage again.
    IEnumerator DeathCoroutine(bool isDead){

        while(isDead && respawnTime > Time.time) {
            spriteRenderer.enabled = !spriteRenderer.enabled;
            yield return new WaitForSeconds(0.2f);
        }
        if(respawnTime <= Time.time) {
            spriteRenderer.enabled = true;
            playerInstance.godMode = false;
            isDead = false;
        }
    }
}