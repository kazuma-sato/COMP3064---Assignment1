using UnityEngine;
using System.Collections;

// COMP3064 CRN13018 Assignment 1 
// Thursday, November 24, 2016
// Instructor: Przemyslaw Pawluk
// Kazuma Sato 100 948 212 kazuma.sato@georgebrown.ca

public class ShieldController : MonoBehaviour {

	// Properties //

	[SerializeField]
	private GameObject maxShield;
	[SerializeField]
	private GameObject damagedShield;

	private bool isShieldMax;
    private Player player;

    // Methods //

	void Start() {
        player = GetComponentInParent<Player> ();
	}

	void Update() {
		setShield();
	}

	// If shield is greater than half of maxShield, PlayerShieldMax is enabled.
	// If it is less than half, PlayerShieldDamaged is enabled.
	// If shield is 0, both gameObjects will be 0.
	void setShield(){

		maxShield.SetActive(false);
		damagedShield.SetActive(false);

		if(player.shield == 0) return; 
			
        if(player.shield > player.maxShield / 2) {
            maxShield.SetActive(true);
        } else {
            damagedShield.SetActive(true);
        }
	}
}