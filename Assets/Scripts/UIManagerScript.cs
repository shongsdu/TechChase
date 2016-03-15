using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// UI functions for play scene

public class UIManagerScript : MonoBehaviour {

	private GameObject obj;
	private GameObject[] stockArray;
	private GameObject selectedChar;
	private string character;
	private Component tech;
	private GameObject excellentText;
	private GameObject greatText;
	private GameObject goodText;
	private GameObject okText;
	private GameObject wrongText;
	private GameObject slowText;
	private GameObject scoreText;
	private GameObject highScoreText;
	private GameObject startButton;
	private GameObject separateButtons;
	private GameObject combinedButtons;
	private GameObject swipeUI;
	private GameObject swipeManager;
	private GameObject readyImage;
	private GameObject goImage;
	private GameObject gameImage;
	private GameObject continueButton;
	private GameObject transparentBG;
	private GameObject popUpImage;
	private GameObject popUpText;
	private GameObject inputField;
	private GameObject StatsGroup;
	private GameObject ExcellentCount;
	private GameObject GreatCount;
	private GameObject GoodCount;
	private GameObject OkCount;
	private GameObject WrongCount;
	private GameObject SlowCount;
	private string highScoreKey = "HScore1";
	
	// Use this for initialization
	void Start () {
		// load character prefab
		selectedChar = GameObject.FindGameObjectWithTag("SelectedChar");
		character = selectedChar.GetComponent<SelectCharScript>().selectedChar;
		obj = (GameObject)Instantiate(Resources.Load(character + "Sprite"));

		// hide game images
		readyImage = GameObject.FindGameObjectWithTag("Ready");
		readyImage.SetActive(false);
		goImage = GameObject.FindGameObjectWithTag("Go");
		goImage.SetActive(false);
		gameImage = GameObject.FindGameObjectWithTag("Game");
		gameImage.SetActive(false);
		excellentText = GameObject.Find("ExcellentText");
		excellentText.SetActive(false);
		greatText = GameObject.Find("GreatText");
		greatText.SetActive(false);
		goodText = GameObject.Find("GoodText");
		goodText.SetActive(false);
		okText = GameObject.Find("OkText");
		okText.SetActive(false);
		wrongText = GameObject.FindGameObjectWithTag("WrongText");
		wrongText.SetActive(false);
		slowText = GameObject.FindGameObjectWithTag("SlowText");
		slowText.SetActive(false);

		//add stock icons to an array
		stockArray = new GameObject[4];
		stockArray[0] = GameObject.FindGameObjectWithTag("StockIcon1");
		stockArray[1] = GameObject.FindGameObjectWithTag("StockIcon2");
		stockArray[2] = GameObject.FindGameObjectWithTag("StockIcon3");
		stockArray[3] = GameObject.FindGameObjectWithTag("StockIcon4");
		scoreText = GameObject.FindGameObjectWithTag("ScoreText");
//		for (int i=3; i>=0; i--) {
//			removeStock (i);
//		}

		highScoreText = GameObject.FindGameObjectWithTag("HighScoreText");
		updateHighScore();    

		startButton = GameObject.FindGameObjectWithTag("Start");

		//stats group
		StatsGroup = GameObject.Find("StatsGroup");
		ExcellentCount = GameObject.Find("ExcellentCount");
		GreatCount = GameObject.Find("GreatCount");
		GoodCount = GameObject.Find("GoodCount");
		OkCount = GameObject.Find("OkCount");
		WrongCount = GameObject.Find("WrongCount");
		SlowCount = GameObject.Find("SlowCount");
		StatsGroup.SetActive (false);

		//HighScore PopUp init
		transparentBG = GameObject.Find("TransparentBG");
		transparentBG.SetActive(false);
		popUpImage = GameObject.Find("PopUp");
		popUpImage.SetActive(false);
		popUpText = GameObject.Find("PopUpText");
		popUpText.SetActive(false);
		inputField = GameObject.Find("InputField");
		inputField.SetActive(false);

		//continue button for high scores
		continueButton = GameObject.Find("ContinueButton");
		continueButton.SetActive(false);
		//deactivateContinueButton ();

		//Tech Buttons
		separateButtons = GameObject.Find ("SeparateButtonGroup");
		combinedButtons = GameObject.Find ("CombinedButtonGroup");

		//Swipe
		swipeUI = GameObject.Find ("SwipeGroup");
		swipeManager = GameObject.Find ("SwipeManager");

		//deactivate tech buttons if swipe controls activated
		if (PlayerPrefs.HasKey ("Swipe")) {
			if (PlayerPrefs.GetInt ("Swipe") == 1) { 
				separateButtons.SetActive (false);
				combinedButtons.SetActive (false);
			} else if (PlayerPrefs.GetInt("Separate") == 1) {
				separateButtons.SetActive (true);
				combinedButtons.SetActive (false);
				swipeUI.SetActive (false);
				swipeManager.SetActive (false);
			} else {
				separateButtons.SetActive (false);
				swipeUI.SetActive (false);
				swipeManager.SetActive (false);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) { 
			backPressed ();
		}
		if (Input.GetKeyDown(KeyCode.LeftArrow)) { 
			LeftPressed ();
		}
		if (Input.GetKeyDown(KeyCode.RightArrow)) { 
			RightPressed ();
		}
		if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.UpArrow) ) { 
			InPlacePressed ();
		}
		if (Input.GetKeyDown(KeyCode.Space)) { 
			StartPressed ();
		}
	}

	public string getSelectedChar() {
		return character;
	}

	public void StartPressed() {obj.SendMessage("PlayGame", 1);}

	public void LeftPressed() {obj.SendMessage("LeftPressed");}

	public void RightPressed() {obj.SendMessage("RightPressed");}

	public void MissPressed() {obj.SendMessage("MissPressed");}

	public void InPlacePressed() {obj.SendMessage("InPlacePressed");}

	
	public void removeCorrect() {
		excellentText.SetActive(false);
		greatText.SetActive(false);
		goodText.SetActive(false);
		okText.SetActive(false);
	}

	public void displayExcellent() {excellentText.SetActive(true);}

	public void removeExcellent() {excellentText.SetActive(false);}

	public void displayGreat() {greatText.SetActive(true);}

	public void removeGreat() {greatText.SetActive(false);}

	public void displayGood() {goodText.SetActive(true);}

	public void removeGood() {goodText.SetActive(false);}

	public void displayOk() {okText.SetActive(true);}

	public void removeOk() {okText.SetActive(false);}

	public void displayWrong() {wrongText.SetActive(true);}
	
	public void removeWrong() {wrongText.SetActive(false);}

	public void displaySlow() {slowText.SetActive(true);}
	
	public void removeSlow() {slowText.SetActive(false);}

	public void displayStock(int i) {stockArray[i].SetActive(true);}

	public void displayNStock(int n) {
		for (int i=0; i<n; i++) {
			stockArray[i].SetActive(true);
		}
	}
	
	public void removeStock(int i) {stockArray[i].SetActive(false);}

	//public void updateStock(string str) {stockText.GetComponent<Text>().text = str;}

	public void displayScore() {scoreText.SetActive(true);}
	
	public void removeScore() {scoreText.SetActive(false);}

	// update Score text
	public void updateScore(string str) {scoreText.GetComponent<Text>().text = str;}

	// update HighScore text
	public void updateHighScore() {
		highScoreText.GetComponent<Text>().text = PlayerPrefs.GetInt(character + highScoreKey, 0).ToString();
	}

	public void displayStart() {startButton.SetActive(true);}
	
	public void hideStart() {startButton.SetActive(false);}

	public void displayReady() {readyImage.SetActive(true);}
	
	public void hideReady() {readyImage.SetActive(false);}

	public void displayGo() {goImage.SetActive(true);}
	
	public void hideGo() {goImage.SetActive(false);}

	public void displayGame() {gameImage.SetActive(true);}
	
	public void hideGame() {gameImage.SetActive(false);}

	public void displayContinueButton() {continueButton.SetActive(true);}

	public void hideContinueButton() {continueButton.SetActive(false);}

	public void activateContinueButton() {
		continueButton.GetComponent<Button> ().interactable = true;
	}

	public void deactivateContinueButton() {
		continueButton.GetComponent<Button> ().interactable = false;
	}

	public void continuePressed() {
		obj.SendMessage("continuePlaying");
	}

	public void nameEntered() {
		obj.SendMessage("updatePlayerName", inputField.GetComponent<InputField>().text);
	}

	public void displayTransparentBG() {transparentBG.SetActive(true);}

	public void hideTransparentBG() {transparentBG.SetActive(false);}

	public void displayPopUpImage() {popUpImage.SetActive(true);}

	public void hidePopUpImage() {popUpImage.SetActive(false);}

	public void displayPopUpText() {popUpText.SetActive(true);}

	public void hidePopUpText() {popUpText.SetActive(false);}

	public void displayInputField() {inputField.SetActive(true);}

	public void hideInputField() {inputField.SetActive(false);}

	public void displayHScorePopup(int place, int score) {
		string placing = "";
		if (place == 1) {placing = "st";} 
		else if (place == 2) {placing = "nd";}
		else if (place == 3) {placing = "rd";}
		else {placing = "th";}

		popUpText.GetComponent<Text>().text = "New High Score!\n" +
			place.ToString() + placing + " - " + score.ToString();

		displayTransparentBG ();
		displayPopUpImage ();
		displayPopUpText ();
		displayInputField ();
		displayContinueButton();

		//check if input field is already populated
		if (inputField.GetComponent<InputField> ().text != "") {
			activateContinueButton();
		}
	}

	public void displayStatsGroup() {StatsGroup.SetActive (true);}

	public void hideStatsGroup() {StatsGroup.SetActive (false);}

	public void updateExcellentCount(int count) {ExcellentCount.GetComponent<Text> ().text = count.ToString ();}

	public void updateGreatCount(int count) {GreatCount.GetComponent<Text> ().text = count.ToString ();}

	public void updateGoodCount(int count) {GoodCount.GetComponent<Text> ().text = count.ToString ();}

	public void updateOkCount(int count) {OkCount.GetComponent<Text> ().text = count.ToString ();}

	public void updateWrongCount(int count) {WrongCount.GetComponent<Text> ().text = count.ToString ();}

	public void updateSlowCount(int count) {SlowCount.GetComponent<Text> ().text = count.ToString ();}

	public void clearStats() {
		updateExcellentCount (0);
		updateGreatCount (0);
		updateGoodCount (0);
		updateOkCount (0);
		updateWrongCount (0);
		updateSlowCount (0);
		obj.SendMessage ("clearStats");
	}

	public void hideHScorePopup() {
		hideTransparentBG ();
		hidePopUpImage ();
		hidePopUpText ();
		hideInputField ();
		hideContinueButton();
	}

	public void backPressed() {
		GameObject.Destroy(selectedChar);
		SceneManager.LoadScene ("Character"); 
	}
}
