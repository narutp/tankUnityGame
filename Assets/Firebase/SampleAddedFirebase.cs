using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;

public class SimpleAddedFirebase : MonoBehaviour {

	DatabaseReference mDatabaseRef;

	void Start() {
		// Set up the Editor before calling into the realtime database.
		FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://tanker54-8623d.firebaseio.com/");

		// Get the root reference location of the database.
		mDatabaseRef = FirebaseDatabase.DefaultInstance.RootReference;

		writeNewUser ("1112233", "mind", "patinya.y@ku.th");
	}

	private void writeNewUser(string userId, string name, string email) {
		User user = new User(name, email);
		string json = JsonUtility.ToJson(user);

		mDatabaseRef.Child("users").Child(userId).SetRawJsonValueAsync(json);
	}


}

public class User {
	public string username;
	public string email;

	public User() {
	}

	public User(string username, string email) {
		this.username = username;
		this.email = email;
	}
}