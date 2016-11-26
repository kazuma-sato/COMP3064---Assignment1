using UnityEngine;
using System.Collections;

// COMP3064 CRN13018 Assignment 1 
// Thursday, November 24, 2016
// Instructor: Przemyslaw Pawluk
// Kazuma Sato 100 948 212 kazuma.sato@georgebrown.ca

public class Player : MonoBehaviour {

	// Properties //

	// If true, the player doesn't take damage.
	public bool godMode = false;

	[SerializeField]
	public int health;
	[SerializeField]
    public int maxHealth;
	[SerializeField]
	public int shield;
	[SerializeField]
	public int maxShield;
	[SerializeField]
	private int life;

	public string weapon = "Gun1";
    private static Player instance;

    // Accessors & Mutators //

    // Stops values from becoming negative or larger than maxHealth
    public int Health {

    	get { return health; }
    	set {
            if(value < 0) {
                health = 0;
            } else if(value > maxHealth) {
                health = maxHealth;
            } else {
                health = value;
            }
    	}
    }

    // Stops values from becoming negative or larger than maxShield
    public int Shield {

    	get { return shield; }
    	set {
            if(value < 0) {
                shield = 0;
            } else if(value > maxShield) {
                shield = maxShield;
            } else {
                shield = value;
            }
    	}
    }

    // Triggers appropriate functions for when the player dies.
    public int Life {

    	get { return life; }
    	set {
			bool dead = life > value;
			if(value < 0){
				life = 0;
				GameObject.Find("HUD").GetComponent<HUDController>().GameOver = true;
			} else {
				life = value;
			}
			if(dead){
				health = 100;
				shield = 0;
				gameObject.GetComponent<PlayerController>().IsDead = true;
			}
		}
	}

	// Methods //

    void Start() {
	
        health = maxHealth;
	}

	public void addHealth(int health){

		Health += health;
		if (Health > maxHealth){
			Health = maxHealth;
		}
	}

	public void addShield(int shield){

		Shield += shield;
		if (Shield > maxShield) {
			Shield = maxShield;
		}
	}

	// Method to determine if the damage should be deducted from shield, health 
	// or just cause the player to die.
	public void addDamage(int damage){
		
		// If godMode is true, no damage is taken
		if(godMode) return;

		if(Shield > 0) {
            if(Shield > damage) {
                Shield -= damage;
            } else {
				damage -= Shield;
                Shield = 0;
				Health -= damage;
			}
        } else if(Health > 0) {
			Health -= damage;
		}
        if(Health <= 0) {
            Health = 0;
            Life--;
		}
	}
}