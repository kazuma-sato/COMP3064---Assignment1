using UnityEngine;
using System.Collections;

// COMP3064 CRN13018 Assignment 1 
// Thursday, November 24, 2016
// Instructor: Przemyslaw Pawluk
// Kazuma Sato 100 948 212 kazuma.sato@georgebrown.ca

public class Enemy : MonoBehaviour {

	// Properties //

	[SerializeField]
	private int health;

	// Methods //

	// Damage deducts health. No health will distroy the gameObject.
	public void addDamage(int damage) {
        
		health -= damage;
        HUDController.CurrentScore += damage * 10;

		if(health <= 0) {
            Destroy(gameObject);
		}
	}
}
