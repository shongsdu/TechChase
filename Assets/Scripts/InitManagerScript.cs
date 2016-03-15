using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class InitManagerScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if (SystemInfo.systemMemorySize <= 512) {
			QualitySettings.masterTextureLimit = 1;
			QualitySettings.SetQualityLevel(0, false);
		} else {
			QualitySettings.masterTextureLimit = 0;
			QualitySettings.SetQualityLevel(1, false);
		}
		SceneManager.LoadScene (1);
	}

	void Awake () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
