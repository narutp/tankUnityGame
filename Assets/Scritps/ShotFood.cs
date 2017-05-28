using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotFood : MonoBehaviour {

	public string Tag;

	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == Tag) {
			Destroy (other.gameObject);
			FoodGenerator.count--;
		}
	}

}
