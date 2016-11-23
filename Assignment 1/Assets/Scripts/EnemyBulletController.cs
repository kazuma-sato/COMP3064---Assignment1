using UnityEngine;
using System.Collections;

public class EnemyBulletController : MonoBehaviour {

    [SerializeField]
    public float speed;
    [SerializeField]
    private GameObject explosion;
    [SerializeField]
    public int damage;

    private Transform _transform;

    private float _xBounds;
    private float _yBounds;

    // Getting the bounds of the Camera.main 
    // & this Transform component object
    void Awake() {

        _xBounds = Camera.main.orthographicSize * Camera.main.aspect;
        _yBounds = Camera.main.orthographicSize;
        _transform = GetComponent<Transform>();
    }
        
    void Update() {

        _transform.Translate(Vector3.up * speed);
        if(Mathf.Abs(transform.position.x) > _xBounds ||
            Mathf.Abs(transform.position.y) > _yBounds)
            Destroy(gameObject);
    }

    // When a bullet hits the Player, the Player takes damage and bullet is destroyed.
    void OnTriggerEnter2D(Collider2D other) {

        if (other.gameObject.layer == LayerMask.NameToLayer("Player") 
                && other.tag == "Ship") {
            Player player = other.gameObject.GetComponent<Player>();
            player.addDamage(damage);
            Instantiate(
                    explosion, _transform.position, _transform.rotation);
            Camera.main.GetComponent<SFXController>().PlaySound(5, _transform.position);
            Destroy(gameObject);

        }
    }

    // Just in case OnTriggerEnter2D() doesn't invoke on enter
    void OnTriggerStay2D(Collider2D other) {

        OnTriggerEnter2D(other);
    }
}
