using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour {

	public static int score;
	private Text scoreText;
	
	// Use this for initialization
	void Start () {
		scoreText = GetComponent<Text>();
		Reset();
		scoreText.text = score.ToString();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Score(int points) {
		//Debug.Log("Scored Points");
		score += points;
		scoreText.text = score.ToString();
	}

	public static void Reset() {
		score = 0;
	}
}
