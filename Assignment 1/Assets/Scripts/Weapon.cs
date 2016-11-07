using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

	[SerializeField]
	private float fireRate;
	[SerializeField]
	private float damage;
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

	private float timeToFire;
	private Player player;
    public float beamEffectUntil;

	void Awake() {

		player = gameObject.GetComponent<Player> ();
		firePoint = transform.FindChild("FirePoint");
        beamEffectUntil = Time.time;
	}
	
	// Update is called once per frame
 	void Update() {

        GameObject currentProjectile = bullet;
        if(beamEffectUntil < Time.time) {
            player.weapon = "Gun1";
        }
		if (player.weapon == "Gun1") {
			currentProjectile = bullet;
			if (fireRate == 0) {
				if (Input.GetAxisRaw ("Fire1") > 0)
					shootBullets (currentProjectile);
			} else if ((Input.GetAxisRaw ("Fire1") > 0) && Time.time > timeToFire) {
				timeToFire = Time.time + 1 / fireRate;
				shootBullets (currentProjectile);
                Camera.main.GetComponent<SFXController>().PlaySound(0, firePoint.position);
			}
		} else if (player.weapon == "Beam1") {
			if (Input.GetButtonDown ("Fire1")) {
				currentProjectile = beamTop;
			} else if (Input.GetButton ("Fire1")) {
				currentProjectile = beamMid;
			} else if (Input.GetButtonUp ("Fire1")) {
				currentProjectile = beamEnd;
				shootBeams (currentProjectile);
			}
			if(Input.GetAxisRaw ("Fire1") > 0) 
                shootBeams (currentProjectile);
            if(currentProjectile == beamTop) 
                Camera.main.GetComponent<SFXController>().PlaySound(1, firePoint.position);
		}
	}

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
		bulletInstance.GetComponent<BulletController> ().damage = damage;
	}
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
		beamInstance.GetComponent<BulletController> ().damage = damage;
		beamInstance.transform.parent = gameObject.transform;
	}
}