using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CharScoreMangerScript : MonoBehaviour {

	private GameObject obj;

	// Use this for initialization
	void Start () {
		obj = GameObject.Find ("SelectedChar");
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) { 
			backPressed ();
		}
	}

	public void FoxPressed() {
		obj.GetComponent<SelectCharScript> ().selectedChar = "Fox";
		SceneManager.LoadScene ("HighScores");
	}
	
	public void FalcoPressed() {
		obj.GetComponent<SelectCharScript> ().selectedChar = "Falco";
		SceneManager.LoadScene ("HighScores");
	}
	
	public void MarthPressed() {
		obj.GetComponent<SelectCharScript> ().selectedChar = "Marth";
		SceneManager.LoadScene ("HighScores");
	}
	
	public void SheikPressed() {
		obj.GetComponent<SelectCharScript> ().selectedChar = "Sheik";
		SceneManager.LoadScene ("HighScores");
	}
	
	public void PeachPressed() {
		obj.GetComponent<SelectCharScript> ().selectedChar = "Peach";
		SceneManager.LoadScene ("HighScores");
	}
	
	public void JigglypuffPressed() {
		obj.GetComponent<SelectCharScript> ().selectedChar = "Jigglypuff";
		SceneManager.LoadScene ("HighScores");
	}
	
	public void CaptainFalconPressed() {
		obj.GetComponent<SelectCharScript> ().selectedChar = "CaptainFalcon";
		SceneManager.LoadScene ("HighScores");
	}
	
	public void IceClimbersPressed() {
		obj.GetComponent<SelectCharScript> ().selectedChar = "IceClimbers";
		SceneManager.LoadScene ("HighScores");
	}

	public void AllTopScoresPressed() {
		GameObject.Destroy(obj);
		SceneManager.LoadScene ("AllTopScores");
	}

	public void backPressed() {
		GameObject.Destroy(obj);
		SceneManager.LoadScene ("Menu");
	}
}
