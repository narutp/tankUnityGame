using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Experience : NetworkBehaviour {
	public const float maxXp = 100.0f;
	public int lv = 1;
	public GameObject owner;
	public Canvas xpTab;

	[SyncVar(hook = "OnChangeXp")]
	public float currentXp = 0;
	public float totalXp = 0;
	public Image xpBar;

	public void start(){
		xpBar.fillAmount = currentXp / maxXp;
	}

	public void gainXp(float amount) {

		if (!isServer) {
			return;
		}

		currentXp += amount;
		totalXp += amount;
		if (currentXp >= maxXp) {
			currentXp -= maxXp;
			lv++;
			//maxXp += 100;
		}

	}

	void OnChangeXp (float currentXp) {
		xpBar.fillAmount = currentXp / maxXp;
	}
}

