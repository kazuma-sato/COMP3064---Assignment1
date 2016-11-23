using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	[SerializeField]
	private int health;

	public void addDamage(int damage) {
        
		health -= damage;
        HUDController.currentScore += damage * 10;
		if(health <= 0) {
            Destroy(gameObject);
		}
	}
}
