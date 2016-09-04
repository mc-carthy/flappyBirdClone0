using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GamePlayController : MonoBehaviour {

	public static GamePlayController instance;

	[SerializeField]
	private Text scoreText, endScore, bestScore, gameOverText;

	[SerializeField]
	private Button restartGameButton, instructionsButton;

	[SerializeField]
	private GameObject pausePanel;

	[SerializeField]
	private GameObject[] birds;

	[SerializeField]
	private Sprite[] medals;

	[SerializeField]
	private Image medalImage;

	private void Awake () {
		MakeInstance ();
		Time.timeScale = 0f;
	}

	private void MakeInstance () {
		if (instance == null) {
			instance = this;
		}
	}
		
	public void PauseGame () {
		if (PlayerController.instance != null) {
			if (PlayerController.instance.isAlive) {
				pausePanel.SetActive (true);
				gameOverText.gameObject.SetActive (false);
				endScore.text = PlayerController.instance.score.ToString ();
				bestScore.text = GameController.instance.GetHighScore ().ToString();
				Time.timeScale = 0f;
				restartGameButton.onClick.RemoveAllListeners ();
				restartGameButton.onClick.AddListener (() => ResumeGame ());
			}
		}
	}

	public void ResumeGame () {
		pausePanel.SetActive (false);
		Time.timeScale = 1f;
	}

	public void RestartGame () {
		Application.LoadLevel (Application.loadedLevel);
	}

	public void GoToMenuButton () {
		SceneFader.instance.FadeIn ("menu");
	}

	public void PlayGame () {
		scoreText.gameObject.SetActive (true);
		birds [GameController.instance.GetSelectedBird ()].SetActive (true);
		instructionsButton.gameObject.SetActive (false);
		Time.timeScale = 1f;
	}

	public void SetScore (int score) {
		scoreText.text = score.ToString ();
	}

	public void PlayerDiedSetScore (int score) {
		pausePanel.SetActive (true);
		gameOverText.gameObject.SetActive (true);
		scoreText.gameObject.SetActive (false);
		endScore.text = score.ToString ();

		if (score > GameController.instance.GetHighScore ()) {
			GameController.instance.SetHighScore (score);
		}
		bestScore.text = GameController.instance.GetHighScore ().ToString ();

		if (score <= 20) {
			medalImage.sprite = medals [0];
		} else if (score <= 40) {
			medalImage.sprite = medals [1];
			if (GameController.instance.IsGreenBirdUnlocked() == 0) {
				GameController.instance.UnlockGreenBird ();
			}
		} else {
			medalImage.sprite = medals [2];
			if (GameController.instance.IsGreenBirdUnlocked() == 0) {
				GameController.instance.UnlockGreenBird ();
			}
			if (GameController.instance.IsRedBirdUnlocked() == 0) {
				GameController.instance.UnlockRedBird ();
			}
		}
		restartGameButton.onClick.RemoveAllListeners ();
		restartGameButton.onClick.AddListener (() => RestartGame ());
	}


}
