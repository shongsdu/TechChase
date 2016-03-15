using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OptionsManagerScript : MonoBehaviour {

	private GameObject buttonToggle;
	private GameObject swipeToggle;
	private GameObject separateToggle;
	private GameObject combinedToggle;

	// Use this for initialization
	void Start () {
		buttonToggle = GameObject.Find ("ButtonToggle");
		swipeToggle = GameObject.Find ("SwipeToggle");
		separateToggle = GameObject.Find ("SeparateToggle");
		combinedToggle = GameObject.Find ("CombinedToggle");;

		if(PlayerPrefs.HasKey("Swipe")){
			if(PlayerPrefs.GetInt("Swipe") == 1){ 
				swipeToggle.GetComponent<Toggle>().isOn = true;
				disableCombinedToggle ();
			} else {
				buttonToggle.GetComponent<Toggle>().isOn = true;
				enabledCombinedToggle ();
			}
		}

		if(PlayerPrefs.HasKey("Separate") && PlayerPrefs.GetInt("Swipe") != 1){
			if(PlayerPrefs.GetInt("Separate") == 1){ 
				separateToggle.GetComponent<Toggle>().isOn = true;
			} else {
				combinedToggle.GetComponent<Toggle>().isOn = true;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) { 
			backPressed ();
		}
	}

	public void enableButtons() {
		PlayerPrefs.SetInt("Swipe", 0);
		enabledCombinedToggle ();
	}

	public void enableSwipe() {
		PlayerPrefs.SetInt("Swipe", 1);
		disableCombinedToggle ();
	}

	public void enableSeparate() {
		PlayerPrefs.SetInt ("Separate", 1);
	}

	public void enableCombined() {
		PlayerPrefs.SetInt ("Separate", 0);
	}

	public void enabledCombinedToggle() {
		if(PlayerPrefs.HasKey("Separate")){
			if(PlayerPrefs.GetInt("Separate") == 1){ 
				separateToggle.GetComponent<Toggle>().isOn = true;
			} else {
				combinedToggle.GetComponent<Toggle>().isOn = true;
			}
		}
		separateToggle.GetComponent<Toggle> ().interactable = true;
		combinedToggle.GetComponent<Toggle>().interactable = true;
	}

	public void disableCombinedToggle() {
		separateToggle.GetComponent<Toggle> ().isOn = false;
		combinedToggle.GetComponent<Toggle> ().isOn = false;
		separateToggle.GetComponent<Toggle>().interactable = false;
		combinedToggle.GetComponent<Toggle>().interactable = false;
	}

	public void backPressed() {
		SceneManager.LoadScene ("Menu");
	}
}
