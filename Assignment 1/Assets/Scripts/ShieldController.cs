using UnityEngine;
using System.Collections;

public class ShieldController : MonoBehaviour {

	[SerializeField]
	private GameObject maxShield;

	[SerializeField]
	private GameObject damagedShield;

	private bool isShieldMax;
    private Player player;

	// Use this for initialization
	void Start () {
        player = GetComponentInParent<Player> ();
	}

	// Update is called once per frame
	void Update () {
		setShield ();
	}

	void setShield(){

		maxShield.SetActive(false);
		damagedShield.SetActive (false);

		if (player.shield == 0) 
			return; 
			
		if (player.shield == player.maxShield)
			maxShield.SetActive (true);
		else
			damagedShield.SetActive (true);
	}
}