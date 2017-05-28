using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scores : MonoBehaviour {

	public GameObject owner;
	public Text highscoreText;
	public Text scoreText;

	public float highscore = 0;
	public string nameHigh = "";
	public float score = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		score = owner.gameObject.GetComponent<Experience> ().totalXp;
		Debug.Log ("Score: " + score);
		highscoreText.text = "Highscore: " + highscore + " by " + nameHigh;
		scoreText.text = "Score: " + score;
	}
}
