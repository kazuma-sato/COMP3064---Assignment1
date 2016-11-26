using UnityEngine;
using System.Collections;

// COMP3064 CRN13018 Assignment 1 
// Thursday, November 24, 2016
// Instructor: Przemyslaw Pawluk
// Kazuma Sato 100 948 212 kazuma.sato@georgebrown.ca

public class Weapon : MonoBehaviour {

	// Properties //

	[SerializeField]
	private float fireRate;
	[SerializeField]
	private int damage;
	[SerializeField]
	private LayerMask toHit;
	[SerializeField]
	private GameObject bullet;
	[SerializeField]
	private GameObject beamTop;
	[SerializeField]
	private GameObject beamMid;
	[SerializeField]
	private GameObject beamEnd;
	[SerializeField]
	private Transform firePoint;

	private	GameObject currentProjectile;
	private float timeToFire;
	private Player player;
    public float beamEffectUntil;

    // Accessors & Mutators //

	void Awake() {

		player = gameObject.GetComponent<Player>();
		firePoint = transform.FindChild("FirePoint");
        beamEffectUntil = 0;
	}
	
 	void Update() {

        // If past beamEffectUntil, weapon is changed back to the default gun.
        if(beamEffectUntil < Time.time) {
            player.weapon = "Gun1";
        } 

        // Gun1 shoots a bullet every frame if fireRate is 0. 
        // Otherwise, it will shoot once every 1/firerate of a seconds.
		if(player.weapon == "Gun1") {
			currentProjectile = bullet;
			if(fireRate == 0) {
				if(Input.GetAxisRaw("Fire1") > 0){
					shootBullets(currentProjectile);
				}
			} else if((Input.GetAxisRaw("Fire1") > 0) && Time.time > timeToFire) {
				timeToFire = Time.time + 1 / fireRate;
				shootBullets (currentProjectile);

				//Plays shooting SFX
                Camera.main.GetComponent<SFXController>().PlaySound(0, firePoint.position);
			}

		// The Beam sprite comes in 3 parts. This block allows the beam to be 
		// constructed properly.
		} else if(player.weapon == "Beam1") {
			if (Input.GetButtonDown("Fire1")) {
				currentProjectile = beamTop;
			} else if(Input.GetButton("Fire1")) {
				currentProjectile = beamMid;
			} else if(Input.GetButtonUp("Fire1")) {
				currentProjectile = beamEnd;
				shootBeams (currentProjectile);
			}
			if(Input.GetAxisRaw("Fire1") > 0) {
                shootBeams (currentProjectile);
			}
            if(currentProjectile == beamTop) {
                Camera.main.GetComponent<SFXController>().PlaySound(1, firePoint.position);
            }
		}
	}

	// Instanciates a bullet from where Firepoint is, pointing towards the mouse 
	// curser.
	void shootBullets(GameObject projectile) {
 		
		Vector2 firePointPosition = new Vector2(
				firePoint.position.x, 
				firePoint.position.y);
		GameObject bulletInstance = Instantiate(
				projectile,
				firePointPosition,
				Quaternion.LookRotation(
					Vector3.forward, 
					Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position)) 
				as GameObject;
		bulletInstance.GetComponent<BulletController>().damage = damage;
	}


	// Similar to shootBullets but, the beam instaciates as a child of gameObject.
	// So, when the player_ship moves or rotates, the beam moves and rotates with with it.
	void shootBeams(GameObject beam){
		Vector2 firePointPosition = new Vector2(
			firePoint.position.x, 
			firePoint.position.y);
		GameObject beamInstance = Instantiate(
			beam,
			firePointPosition,
			Quaternion.LookRotation(
				Vector3.forward, 
				Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position)) 
			as GameObject;
		beamInstance.GetComponent<BulletController>().damage = damage;
		beamInstance.transform.parent = gameObject.transform;
	}
}