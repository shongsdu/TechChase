using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class HowToManagerScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) { 
			backPressed ();
		}
	}

	public void backPressed() {
		SceneManager.LoadScene ("Menu");
	}
}
