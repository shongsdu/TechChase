using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

// menu functions

public class MenuManagerScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if(!PlayerPrefs.HasKey("Swipe")){
			PlayerPrefs.SetInt("Swipe", 0);
		}
		if(!PlayerPrefs.HasKey("Separate")){
			PlayerPrefs.SetInt("Separate", 0);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) { 
			Application.Quit();
		}
	}

	public void PlayPressed() {
		SceneManager.LoadScene ("Character");
	}

	public void OptionsPressed() {
		SceneManager.LoadScene ("Options");
	}

	public void HighScorePressed() {
		SceneManager.LoadScene ("CharHighScores");
	}

	public void HowToPressed() {
		SceneManager.LoadScene ("HowTo");
	}

	public void ExitPressed() {
		Application.Quit();
	}
}
