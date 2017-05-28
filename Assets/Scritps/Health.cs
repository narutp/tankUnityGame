using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Health : NetworkBehaviour {

	public float maxHealth = 100.0f;
	public bool destroyOnDeath;
	public GameObject owner;

	[SyncVar(hook = "updateHealthBar")]
	public float currentHealth;
	public Image healthBar;


	void Start() {
		currentHealth = maxHealth;
	}
	public void TakeDamage(float amount) {

		if (!isServer) {
			return;
		}

		currentHealth -= amount;
		if (currentHealth <= 0) {
			Destroy (owner.gameObject);
		}

		updateHealthBar (currentHealth);
			
	}

	public void updateHealthBar(float currentHealth) {
		healthBar.fillAmount = currentHealth / maxHealth;
	}

	[ClientRpc]
	void RpcRespawn() {

		if (isLocalPlayer) {
			transform.position = Vector3.zero;
		}
	}
}
