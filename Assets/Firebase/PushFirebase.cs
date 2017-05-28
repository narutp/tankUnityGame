using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;
using Firebase;
using Firebase.Unity.Editor;
using UnityEngine.UI;

public class PushFirebase : MonoBehaviour {

	DatabaseReference mDatabase;
	public InputField iField;
	public string myName;
	public Button yourButton;

	void Start() {
		// Set up the Editor before calling into the realtime database.
		FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://tanker54-8623d.firebaseio.com/");

		// Get the root reference location of the database.
		mDatabase = FirebaseDatabase.DefaultInstance.RootReference;

		Debug.Log(iField.text);
		myName = iField.text;

		Button btn = yourButton.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);

		WriteNewScore (myName, 0);

	}

	void TaskOnClick()
	{
		Debug.Log("You have clicked the button!");
	}

	private void WriteNewScore(string userId, int score) {
		// Create new entry at /user-scores/$userid/$scoreid and at
		// /leaderboard/$scoreid simultaneously
		string key = mDatabase.Child("scores").Push().Key;
		Debug.Log (key);
		LeaderBoardEntry entry = new LeaderBoardEntry(userId, score);
		Dictionary<string, System.Object> entryValues = entry.ToDictionary();

		Dictionary<string, System.Object> childUpdates = new Dictionary<string, System.Object>();
		childUpdates["/scores/" + key] = entryValues;
		childUpdates["/user-scores/" + userId + "/" + key] = entryValues;

		mDatabase.UpdateChildrenAsync(childUpdates);
	}

}