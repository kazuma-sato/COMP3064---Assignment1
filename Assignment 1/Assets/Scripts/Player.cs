using UnityEngine;
using System.Collections;

// COMP3064 Assignment 1
// Due Date: Sunday, October 30, 2016
// Instructor: Przemyslaw Pawluk
// Kazuma Sato 100 948 212

public class Player : MonoBehaviour {

	private bool godMode = false;

	[SerializeField]
	public float health;
	[SerializeField]
	public float maxHealth;
	[SerializeField]
	public float shield;
	[SerializeField]
	public float maxShield;
	[SerializeField]
	public int life;

	public string weapon = "Gun1";
	public static Player instance;

	public Player(){
	
		instance = this;
		this.health = maxHealth;
		this.shield = 0;
		this.life = 3;
	}

	public void addHealth(float health){

		this.health += health;
		if (this.health > maxHealth) 
			this.health = maxHealth;
        Debug.Log("Life: " + life.ToString ());
	}

	public void addShield(float shield){

		this.shield += shield;
		if (this.shield > maxShield) 
			this.shield = maxShield;
        Debug.Log("Shield : " + shield.ToString());

	}

	public void addDamage(float damage){
		
		if (godMode) return;

		Debug.Log("Player took + " + damage + "damage.");

		if (shield > 0) {
			if (shield > damage)
				shield -= damage;
			else {
				damage -= shield;
				shield = 0;
				health -= damage;
			}
		} else {
			health -= damage;
		}
		if (health <= 0) {
			health = 0;
			die ();
		}
        Debug.Log("Shield : " + shield.ToString());
        Debug.Log("Life: " + life.ToString ());
	}

	private void die(){
		if (life < 0) {
			life--;
			health = maxHealth;
			shield = 0;
		} else {
		
		}
        Debug.Log("Life: " + life.ToString ());
	}
}