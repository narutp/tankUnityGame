using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;
using Firebase;
using Firebase.Unity.Editor;
using UnityEngine.UI;

public class StartFunction : MonoBehaviour {
	DatabaseReference mDatabase;
	public GameObject iField;
	public string myName;
	public GameObject yourButton;
	public GameObject player;

	public void Start()
	{
		// Set up the Editor before calling into the realtime database.
		FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://tanker54-8623d.firebaseio.com/");

		// Get the root reference location of the database.
		mDatabase = FirebaseDatabase.DefaultInstance.RootReference;

		//Debug.Log(iField.text);

		Button btn = yourButton.gameObject.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);

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

	public void TaskOnClick()
	{
		myName = iField.gameObject.GetComponent<InputField>().text;
		WriteNewScore (myName, 0);
		Debug.Log("You have clicked the button!");
		player.gameObject.name = myName;
		Destroy (iField);
		Destroy (yourButton);
	}
}

public class LeaderBoardEntry {
	public string uid;
	public int score = 0;

	public LeaderBoardEntry(string uid, int score) {
		this.uid = uid;
		this.score = score;
	}

	public Dictionary<string, System.Object> ToDictionary() {
		Dictionary<string, System.Object> result = new Dictionary<string, System.Object>();
		result["uid"] = uid;
		result["score"] = score;

		return result;
	}
}
