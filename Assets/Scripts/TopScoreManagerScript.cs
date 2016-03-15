using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TopScoreManagerScript : MonoBehaviour {

	public class HighScore {
		public int score;
		public string character;
		public string name;
		
		public HighScore (int score, string name, string character) {
			this.score = score;
			this.character = character;
			this.name = name;
		}
	}
	
	private GameObject canvas;
	private GameObject resetPopUp;
	private GameObject[] iconArray;
	private string[] characterArray;
	private string highScoreKey = "HScore";
	private string nameScoreKey = "Name";
	private HighScore[] scoreArray;
	private HighScore tempScore;
	
	// Use this for initialization
	void Start () {		
		iconArray = new GameObject[11];
		canvas = GameObject.Find("Canvas");
		characterArray = new string[]{"", "Fox", "Falco", "Sheik", "Marth", "Jigglypuff", "Peach", "CaptainFalcon", "IceClimbers"};
		scoreArray = new HighScore[9];
		sortTopScores ();
		populateHighScore ();
		resetPopUp = GameObject.Find ("ResetGroup");
		resetPopUp.SetActive (false);
	} 
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) { 
			backPressed ();
		}
	}

	private void sortTopScores() {
		for (int i=1; i<=8; i++) {
			scoreArray[i] = new HighScore(PlayerPrefs.GetInt(characterArray[i] + highScoreKey + "1", 0),
			                              PlayerPrefs.GetString(characterArray[i] + nameScoreKey + "1", "AAA"),
			                              characterArray[i]);
		}

		for (int i=1; i<=7; i++) {
			for (int j=i+1; j<=8; j++) {
				if (scoreArray[i].score < scoreArray[j].score) {
					tempScore = scoreArray[i];
					scoreArray[i] = scoreArray[j];
					scoreArray[j] = tempScore;
				}
			}
		}
	}
	
	private void populateHighScore(){
		GameObject score;
		GameObject name;
		GameObject stockIcon;
		GameObject newIcon;
		for(int i=1; i<=8; i++){
			score = GameObject.Find("ScoreText" + i);
			name = GameObject.Find("NameText" + i);

			stockIcon = GameObject.Find(scoreArray[i].character + "Icon");
			score.GetComponent<Text>().text = scoreArray[i].score.ToString();
			name.GetComponent<Text>().text = scoreArray[i].name;
			newIcon = (GameObject)Instantiate (stockIcon, new Vector3(-17, 1201-(i*230), 0), Quaternion.identity);
			newIcon.transform.SetParent(canvas.transform, false);
			newIcon.transform.SetSiblingIndex(4);
			iconArray[i] = newIcon;
		}
	}
	
	private void deleteIcons() {
		for(int i=1; i<=10; i++){
			GameObject.Destroy(iconArray[i]);
		}
	}
	
	public void hidePopUp() {
		resetPopUp.SetActive (false);
	}
	
	public void showPopUp() {
		resetPopUp.SetActive (true);
	}
	
	public void resetHighScores() {
		int swipe = PlayerPrefs.GetInt ("Swipe");
		PlayerPrefs.DeleteAll ();
		PlayerPrefs.SetInt("Swipe", swipe);
		deleteIcons();
		sortTopScores ();
		populateHighScore ();
	}

	public void backPressed() {
		SceneManager.LoadScene ("CharHighScores");
	}
}
