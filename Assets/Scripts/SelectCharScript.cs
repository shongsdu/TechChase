using UnityEngine;
using System.Collections;

// Save selected character and load in play scene

public class SelectCharScript : MonoBehaviour {

	public string selectedChar;

	// Use this for initialization
	void Start () {
		
	}

	void Awake () {
		DontDestroyOnLoad(this);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
