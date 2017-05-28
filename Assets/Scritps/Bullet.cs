using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;
using Firebase;
using Firebase.Unity.Editor;

public class Bullet : MonoBehaviour {

	DatabaseReference mDatabase;
	public float speed;
	public GameObject owner;
	public float damage;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (Vector3.right * speed * Time.deltaTime);
	}

	void OnTriggerEnter(Collider col) {
		if (col.gameObject.tag == "Player") {
			var hit = col.gameObject;
			var hitPlayer = hit.GetComponent<PlayerController> ();
			var health = hit.GetComponent<Health> ();
			var xp = hit.GetComponent<Experience> ();
			if (health != null) {
				if (health.currentHealth - damage <= 0) {
					owner.GetComponent<Experience>().gainXp(xp.totalXp + 50);
					connectDatabase();
					WriteNewScore (owner.gameObject.name, (int)owner.GetComponent<Experience> ().totalXp);
					Destroy (hit);
				}
					health.TakeDamage (damage);
			}
		}
		if (col.gameObject.tag == "Food") {
			Destroy (col.gameObject);
			FoodGenerator.count--;
			owner.GetComponent<Experience> ().gainXp(10);
			connectDatabase();
			WriteNewScore (owner.gameObject.name, (int)owner.GetComponent<Experience> ().totalXp);
		}
		Destroy (gameObject);
		
	}

	void connectDatabase() {
		// Set up the Editor before calling into the realtime database.
		FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://tanker54-8623d.firebaseio.com/");

		// Get the root reference location of the database.
		mDatabase = FirebaseDatabase.DefaultInstance.RootReference;
	}

	private void WriteNewScore(string userId, int score) {
		// Create new entry at /user-scores/$userid/$scoreid and at
		// /leaderboard/$scoreid simultaneously
		//		string key = mDatabase.Child("scores").Push().Key;
		//		Debug.Log (key);
		LeaderBoardEntry entry = new LeaderBoardEntry(userId, score);
		Dictionary<string, System.Object> entryValues = entry.ToDictionary();

		Dictionary<string, System.Object> childUpdates = new Dictionary<string, System.Object>();
		//		childUpdates["/scores/" + key] = entryValues;
		//		childUpdates["/scores"] = entryValues;
		//		childUpdates["/user-scores/" + userId + "/" + key] = entryValues;
		childUpdates["/user-scores/" + userId] = entryValues;

		mDatabase.UpdateChildrenAsync(childUpdates);
	}
}
