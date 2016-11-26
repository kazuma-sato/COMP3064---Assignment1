using UnityEngine;
using System.Collections;

// COMP3064 CRN13018 Assignment 1 
// Thursday, November 24, 2016
// Instructor: Przemyslaw Pawluk
// Kazuma Sato 100 948 212 kazuma.sato@georgebrown.ca

// Just a class to end the animations.
public class Explosion : MonoBehaviour {

	void destroy(){
		
		Destroy (gameObject);
	}
}
