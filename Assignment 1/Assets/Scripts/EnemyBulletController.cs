using UnityEngine;
using System.Collections;

// COMP3064 CRN13018 Assignment 1 
// Thursday, November 24, 2016
// Instructor: Przemyslaw Pawluk
// Kazuma Sato 100 948 212 kazuma.sato@georgebrown.ca

public class EnemyBulletController : MonoBehaviour {

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

    // Getting the bounds of the Camera.main & this Transform component object
    void Awake() {

        _transform = GetComponent<Transform>();
    }
    
    // Translates the bullet by a factor of speed property as well as checks 
    // if bullet has left the frame
    void Update() {

        _xBounds = Camera.main.orthographicSize * Camera.main.aspect;
        _yBounds = Camera.main.orthographicSize;

        _transform.Translate(Vector3.up * speed);

        if(Mathf.Abs(transform.position.x) > _xBounds ||
            Mathf.Abs(transform.position.y) > _yBounds)
            Destroy(gameObject);
    }

    // When an enemy bullet hits the Player, the Player takes damage and bullet is 
    // destroyed. An explosion animation instantiates with a sound effect.
    void OnTriggerEnter2D(Collider2D other) {

        if(other.gameObject.layer == LayerMask.NameToLayer("Player") && 
                other.tag == "Ship") {
            Player player = other.gameObject.GetComponent<Player>();
            player.addDamage(damage);
            Instantiate(explosion, _transform.position, _transform.rotation);
            Camera.main.GetComponent<SFXController>().PlaySound(5, _transform.position);
            Destroy(gameObject);
        }
    }

    // Just in case OnTriggerEnter2D() doesn't invoke on enter
    void OnTriggerStay2D(Collider2D other) {

        OnTriggerEnter2D(other);
    }
}
