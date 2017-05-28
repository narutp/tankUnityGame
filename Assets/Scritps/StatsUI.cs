using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class StatsUI : NetworkBehaviour {

	public GameObject owner;
	public int remainingStats = 0;
	private int currentPlayerLevel = 1;
	public int bulletDamageLv = 1;
	public int bulletSpeedLv = 1;
	public int playerSpeedLv = 1;
	public int healthLv = 1;
	public int healthRegenLv = 1;

	public Text remainingText;
	public Text bulletDamageText;
	public Text bulletSpeedText;
	public Text playerSpeedText;
	public Text healthText;
	public Text healthRegenText;

	// Use this for initialization
	void Start () {
		if (!owner.GetComponent<PlayerController> ().isLocalPlayer) {
			Destroy (remainingText);
			Destroy (bulletDamageText);
			Destroy (bulletSpeedText);
			Destroy (playerSpeedText);
			Destroy (healthText);
			Destroy (healthRegenText);

		}
	}
	
	// Update is called once per frame
	void Update () {

		if (!owner.GetComponent<PlayerController> ().isLocalPlayer) {
			return;
		}

		var tempLevel = currentPlayerLevel;
		currentPlayerLevel = owner.gameObject.GetComponent<Experience> ().lv;

		if (currentPlayerLevel > tempLevel) {
			remainingStats += currentPlayerLevel - tempLevel;

		}
		remainingText.text = "Remaining Stats: " + remainingStats.ToString ();

		if (remainingStats >= 1) {
			if (Input.GetKeyUp ("1")) {
				bulletDamageLv++;
				remainingStats--;
				bulletDamageText.text = "(1) Bullet Damage Lv: " + bulletDamageLv.ToString();
				owner.gameObject.GetComponent<PlayerController>().bulletDamage = 10 + bulletDamageLv * 3;

			}

			if (Input.GetKeyUp ("2")) {
				bulletSpeedLv++;
				remainingStats--;
				bulletSpeedText.text = "(2) Bullet Speed Lv: " + bulletSpeedLv.ToString();
				owner.gameObject.GetComponent<PlayerController>().bulletSpeed = 10 + bulletSpeedLv * 1;
			}

			if (Input.GetKeyUp ("3")) {
				playerSpeedLv++;
				remainingStats--;
				playerSpeedText.text = "(3) Player Speed Lv: " + playerSpeedLv.ToString();
				owner.gameObject.GetComponent<PlayerController>().maxSpeed = 3 + playerSpeedLv * 0.5f;
			}

			if (Input.GetKeyUp ("4")) {
				healthLv++;
				remainingStats--;
				healthText.text = "(4) Health Lv: " + healthLv.ToString();
				owner.gameObject.GetComponent<Health> ().maxHealth = 100 + healthLv * 20;
			}

			if (Input.GetKeyUp ("5")) {
				healthRegenLv++;
				remainingStats--;
				healthRegenText.text = "(5) Health Regen Lv: " + healthRegenLv.ToString();
				owner.gameObject.GetComponent<PlayerController>().healthRegen = 1 + healthRegenLv * 0.5f;
			}
		}
	}
}
