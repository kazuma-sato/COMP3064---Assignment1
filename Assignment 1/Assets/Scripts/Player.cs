using UnityEngine;
using System.Collections;

// COMP3064 Assignment 1
// Due Date: Sunday, October 30, 2016
// Instructor: Przemyslaw Pawluk
// Kazuma Sato 100 948 212

public class Player : MonoBehaviour {

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

    public int Health{
    	get { return health; }
    	set {
    		if(value <= 0){
    			health = 0;
    		} else {
    			health = value;
    		}
    	}
    }

    public int Shield{
    	get { return shield; }
    	set {
    		if(value < 0) {
    			shield = 0;
    		} else {
    			shield = value;
    		} 

    	}
    }

    public int Life{
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

    void Awake() {
	
        health = maxHealth;
	}

	public void addHealth(int health){

		this.health += health;
		if (this.health > maxHealth) 
			this.health = maxHealth;
	}

	public void addShield(int shield){

		this.shield += shield;
		if (this.shield > maxShield) 
			this.shield = maxShield;
	}

	public void addDamage(int damage){
		
		if(godMode) return;

		if(Shield > 0) {
            if(shield > damage) {
                Shield -= damage;
            } else {
				damage -= shield;
                Shield = 0;
				Health -= damage;
			}
        } else if (health > 0){
			health -= damage;
        } else if (health <= 0) {
            health = 0;
            Life--;
		}
	}
}