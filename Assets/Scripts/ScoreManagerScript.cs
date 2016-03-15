using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreManagerScript : MonoBehaviour {

	private GameObject selectedChar;
	private GameObject canvas;
	private GameObject resetPopUp;
	private GameObject[] iconArray;
	private string highScoreKey = "HScore";
	private string nameScoreKey = "Name";
	private string character;

	// Use this for initialization
	void Start () {
		selectedChar = GameObject.FindGameObjectWithTag("SelectedChar");
		character = selectedChar.GetComponent<SelectCharScript>().selectedChar;

		iconArray = new GameObject[11];
		canvas = GameObject.Find("Canvas");
		populateHighScore ();
		resetPopUp = GameObject.Find ("ResetGroup");
		resetPopUp.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) { 
			backPressed(); 
		}
	}

	private void populateHighScore(){
		GameObject score;
		GameObject name;
		GameObject stockIcon;
		GameObject newIcon;
		for(int i=1; i<=10; i++){
			score = GameObject.Find("ScoreText" + i);
			name = GameObject.Find("NameText" + i);
			stockIcon = GameObject.Find(character + "Icon");
			score.GetComponent<Text>().text = PlayerPrefs.GetInt(character + highScoreKey + i, 0).ToString();
			name.GetComponent<Text>().text = PlayerPrefs.GetString(character + nameScoreKey + i, "AAA");
			newIcon = (GameObject)Instantiate (stockIcon, new Vector3(-23, 1184-(i*200), 0), Quaternion.identity);
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
		for(int i=1; i<=10; i++){
			PlayerPrefs.DeleteKey(character + highScoreKey + i);
			PlayerPrefs.DeleteKey(character + nameScoreKey + i);
		}
		deleteIcons();
		populateHighScore ();
	}

	public void backPressed() {
		GameObject.Destroy(selectedChar);
		SceneManager.LoadScene ("CharHighScores");
	}
}
