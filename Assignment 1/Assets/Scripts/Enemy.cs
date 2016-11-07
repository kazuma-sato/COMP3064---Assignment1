using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	[SerializeField]
	public float maxHealth;
	private float health;

	// Use this for initialization
	void Start () {

		health = maxHealth;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void addDamage(float damage){

		Debug.Log("Enemy took + " + damage + "damage.");

		health -= damage;
		if (health <= 0) {
			die ();
		}
		Debug.Log ("Current Player Status:\n\nHealth: " + health.ToString () + " /  " + maxHealth.ToString () + ".");
	}
	public void die(){

		health = maxHealth;
        Destroy(gameObject);
	}
}
