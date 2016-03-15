using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

// character selection screen functions

public class CharManagerScript : MonoBehaviour {

	private GameObject obj;

	// Use this for initialization
	void Start () {
		obj = GameObject.Find ("SelectedChar");
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) { 
			backPressed();
		}
	}

	public void FoxPressed() {
		obj.GetComponent<SelectCharScript> ().selectedChar = "Fox";
		SceneManager.LoadScene ("Loading");
	}
	
	public void FalcoPressed() {
		obj.GetComponent<SelectCharScript> ().selectedChar = "Falco";
		SceneManager.LoadScene ("Loading");
	}
	
	public void MarthPressed() {
		obj.GetComponent<SelectCharScript> ().selectedChar = "Marth";
		SceneManager.LoadScene ("Loading");
	}
	
	public void SheikPressed() {
		obj.GetComponent<SelectCharScript> ().selectedChar = "Sheik";
		SceneManager.LoadScene ("Loading");
	}
	
	public void PeachPressed() {
		obj.GetComponent<SelectCharScript> ().selectedChar = "Peach";
		SceneManager.LoadScene ("Loading");
	}

	public void JigglypuffPressed() {
		obj.GetComponent<SelectCharScript> ().selectedChar = "Jigglypuff";
		SceneManager.LoadScene ("Loading");
	}

	public void CaptainFalconPressed() {
		obj.GetComponent<SelectCharScript> ().selectedChar = "CaptainFalcon";
		SceneManager.LoadScene ("Loading");
	}

	public void IceClimbersPressed() {
		obj.GetComponent<SelectCharScript> ().selectedChar = "IceClimbers";
		SceneManager.LoadScene ("Loading");
	}

	public void backPressed() {
		GameObject.Destroy(obj);
		SceneManager.LoadScene ("Menu");
	}
}
