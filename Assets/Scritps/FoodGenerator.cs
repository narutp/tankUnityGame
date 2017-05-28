using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodGenerator : MonoBehaviour {
	public GameObject Food;
	public float Speed;
	public static float count = 0;

	void Start(){
		InvokeRepeating ("Generate", 0, Speed);
	}

	void Generate(){
		if (count <= 100) {
			int x = Random.Range (-800, 800);
			int y = Random.Range (-800, 800);

			Vector3 Target = Camera.main.ScreenToWorldPoint (new Vector3 (x, y, 0));
			Target.z = 0;

			Instantiate (Food, Target, Quaternion.identity);
			count++;
		}
	}
		
		
}
