using UnityEngine;
using System.Collections;

// game logic for play scene

public class CharController : MonoBehaviour {
	
	private Animator animator;
	private string character;
	private bool start = false;
	private bool notAnswered = false; //checks whether player has chosen a tech option during animation
	private bool animationPlaying = false;
	private bool timerOn = false;
	private bool readySetGo = false;
	private bool gameOver = false;
	private int rand = 0;
	private int stockCount = 4;
	private int timer = 0;
	private int score = 0;
	private int excellentCount = 0;
	private int greatCount = 0;
	private int goodCount = 0;
	private int okCount = 0;
	private int wrongCount = 0;
	private int slowCount = 0;
	private float buttonTime;
	private AnimatorStateInfo info;
	private UIManagerScript ui;
	
	void Start () {
		animator = GetComponent<Animator>();
		animator.speed = 1.0f;
		ui = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManagerScript> ();
		character = ui.getSelectedChar ();
	}

	void Update () {
		ui.updateScore (score.ToString ());

		if (timerOn) {
			timer--;
			if (timer == 45 && !gameOver) {
				RemoveTexts();
				animator.Play("idle", -1, 0f);
			}
			if (timer == 33 && readySetGo && !gameOver) {
				ui.hideReady();
				ui.displayGo();
			}
			if (timer == 12 && readySetGo && !gameOver) {
				ui.hideGo();
			}
			if (timer == 0 && !gameOver) {
				readySetGo = false;
				timerOn = false;
				StartTechAnimation();
			}
			if (timer <=0 && gameOver) {
				gameOver = false;
				RemoveTexts();
				ui.displayGame();
				ui.displayStart();
				checkHighScore(score);
			}
		}

		if (start) {
			start = false;
			rand = Random.Range(1, 5);
			switch (rand) {
			case 1:
				animator.Play("left", -1, 0f);
				break;
			case 2:
				animator.Play("right", -1, 0f);
				break;
			case 3:
				animator.Play("inPlace", -1, 0f);
				break;
			case 4:
				animator.Play("miss", -1, 0f);
				break;
			}
		}
	}

	public void StartTechAnimation() {
		if (!animationPlaying) { 
			start = true;
			animationPlaying = true;
			notAnswered = true;
			RemoveTexts ();
		}
	}

	public void RemoveTexts() {
		ui.hideStart();
		ui.removeCorrect();
		ui.removeWrong();
		ui.removeSlow();
	}

	public void PlayGame(int n) {
		if (n == 1) {
			stockCount = 4;
			score = 0;
			ui.displayNStock(stockCount);
			ui.hideGame();
			ui.displayReady();
			readySetGo = true;
			animator.Play("idle", -1, 0f);
		}
		timer = 85;
		timerOn = true;
		ui.hideStart();
	}

	public void LeftPressed() {
		if (notAnswered) {
			notAnswered = false;
			info = animator.GetCurrentAnimatorStateInfo (0);
			buttonTime = info.normalizedTime;
			if (info.IsName ("left") && buttonTime > 0.0 && buttonTime < 1.0) {
				displayCorrect ("roll", buttonTime);
				score += (int)((1-buttonTime)*800);
			} else {
				loseStock();
				ui.displayWrong ();
				wrongCount++;
				ui.updateWrongCount (wrongCount);
			}
		}
	}
	
	public void RightPressed() {
		if (notAnswered) {
			notAnswered = false;
			info = animator.GetCurrentAnimatorStateInfo (0);
			buttonTime = info.normalizedTime;
			if (info.IsName ("right") && buttonTime > 0.0 && buttonTime < 1.0) {
				displayCorrect ("roll", buttonTime);
				score += (int)((1-buttonTime)*800);
			} else {
				loseStock();
				ui.displayWrong ();
				wrongCount++;
				ui.updateWrongCount (wrongCount);
			}
		}
	}
	
	public void MissPressed() {
		if (notAnswered) {
			notAnswered = false;
			info = animator.GetCurrentAnimatorStateInfo (0);
			buttonTime = info.normalizedTime;
			if (info.IsName ("miss") && buttonTime > 0.0 && buttonTime < 1.0) {
				displayCorrect ("inplace", buttonTime);
				score += (int)((1-buttonTime)*1050);
			} else {
				loseStock();
				ui.displayWrong ();
				wrongCount++;
				ui.updateWrongCount (wrongCount);
			}
		}
	}
	
	public void InPlacePressed() {
		if (notAnswered) {
			notAnswered = false;
			info = animator.GetCurrentAnimatorStateInfo (0);
			buttonTime = info.normalizedTime;
			if (PlayerPrefs.HasKey ("Swipe")) {
				if (PlayerPrefs.GetInt("Swipe") == 1 || PlayerPrefs.GetInt("Separate") == 1) {
					if (info.IsName ("inPlace") && buttonTime > 0.0 && buttonTime < 1.0) {
						displayCorrect ("inplace", buttonTime);
						score += (int)((1-buttonTime)*1050);
					} else {
						loseStock();
						ui.displayWrong ();
						wrongCount++;
						ui.updateWrongCount (wrongCount);
					}
				} else {
					if ((info.IsName ("inPlace") || info.IsName ("miss")) && buttonTime > 0.0 && buttonTime < 1.0) {
						displayCorrect ("inplace", buttonTime);
						score += (int)((1-buttonTime)*1050);
					} else {
						loseStock();
						ui.displayWrong ();
						wrongCount++;
						ui.updateWrongCount (wrongCount);
					}
				}
			}
		}
	}

	public void endOfAnimation() {
		if (notAnswered) {
			notAnswered = false;
			loseStock();
			slowCount++;
			ui.displaySlow ();
			ui.updateSlowCount (slowCount);
		}
		animationPlaying = false;

		//Check if player lost
		if (stockCount > 0) {
			PlayGame (0);
		} else {
			gameOver = true;
			timer = 50;
			timerOn = true;
		}
	}

	private void loseStock() {
		stockCount--;
		ui.removeStock (stockCount);
	}

    private void checkHighScore(int score){
		int newScore;
		newScore = score;
		for(int i=1; i<=10; i++){
			if(PlayerPrefs.HasKey(character + "HScore" + i)){
				if(PlayerPrefs.GetInt(character + "HScore" + i)<newScore){ 
					// new score is higher than the stored score
					ui.displayHScorePopup(i, score);
					ui.hideStart();
					break;
				}
			} else if (newScore > 0) {
				ui.displayHScorePopup(i, score);
				ui.hideStart();
				break;
			}
		}
		ui.updateHighScore();
	}

	private void updateHScoreList(int score, string name){
		int newScore;
		string newName;
		int oldScore;
		string oldName;
		newScore = score;
		newName = name;
		for(int i=1; i<=10; i++){
			if(PlayerPrefs.HasKey(character + "HScore" + i)){
				if(PlayerPrefs.GetInt(character + "HScore" + i)<newScore){ 
					// new score is higher than the stored score
					oldScore = PlayerPrefs.GetInt(character + "HScore" + i);
					oldName = PlayerPrefs.GetString(character + "Name" + i);
					PlayerPrefs.SetInt(character + "HScore" + i,newScore);
					PlayerPrefs.SetString(character + "Name" + i,newName);
					newScore = oldScore;
					newName = oldName;
				}
			} else {
				PlayerPrefs.SetInt(character + "HScore" + i,newScore);
				PlayerPrefs.SetString(character + "Name" + i,newName);
				newScore = 0;
				newName = "AAA";
			}
		}
		ui.updateHighScore();
	}

	private void updatePlayerName(string newName) {
		updateHScoreList (score, newName);
	}

	private void continuePlaying() {
		ui.hideHScorePopup();
		ui.displayStart ();
	}

	public void displayCorrect(string tech, float buttonTime) {
		if (tech.Equals ("roll")) {
			if (buttonTime < 0.55) {
				excellentCount++;
				ui.displayExcellent ();
				ui.updateExcellentCount (excellentCount);
			} else if (buttonTime < 0.65) {
				greatCount++;
				ui.displayGreat ();
				ui.updateGreatCount (greatCount);
			} else if (buttonTime < 0.8) {
				goodCount++;
				ui.displayGood ();
				ui.updateGoodCount (goodCount);
			} else {
				okCount++;
				ui.displayOk ();
				ui.updateOkCount (okCount);
			}
		} else {
			if (buttonTime < 0.65) {
				excellentCount++;
				ui.displayExcellent ();
				ui.updateExcellentCount (excellentCount);
			} else if (buttonTime < 0.70) {
				greatCount++;
				ui.displayGreat ();
				ui.updateGreatCount (greatCount);
			} else if (buttonTime < 0.8) {
				goodCount++;
				ui.displayGood ();
				ui.updateGoodCount (goodCount);
			} else {
				okCount++;
				ui.displayOk ();
				ui.updateOkCount (okCount);
			}
		}
	}


	private void clearStats() {
		excellentCount = 0;
		greatCount = 0;
		goodCount = 0;
		okCount = 0;
		wrongCount = 0;
		slowCount = 0;
	}
}
