using GooglePlayGames;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using System.Collections;

public class LeaderboardsController : MonoBehaviour {

	public static LeaderboardsController instance;

	private const string LEADERBOARDS_SCORE = "CggIwOem-CoQAhAH";

	private void Awake () {
		MakeSingleton ();
	}

	private void Start () {
		PlayGamesPlatform.Activate ();
	}

	private void OnLevelWasLoaded () {
		ReportScore (GameController.instance.GetHighScore ());
	}

	private void MakeSingleton () {
		if (instance == null) {
			instance = this;
			DontDestroyOnLoad (gameObject);
		} else {
			Destroy (gameObject);
		}
	}

	public void ConnectOrDisconnectOnGooglePlayGames () {
		if (Social.localUser.authenticated) {
			PlayGamesPlatform.Instance.SignOut ();
		} else {
			Social.localUser.Authenticate ((bool success) => {
				if (success) {
					// Write success criteria
				} else {
					// Write failure criteria
				}
			});
		}
	}

	public void OpenLeaderboardsScore () {
		if (Social.localUser.authenticated) {
			PlayGamesPlatform.Instance.ShowLeaderboardUI (LEADERBOARDS_SCORE);
		}
	}

	private void ReportScore(int score) {
		if (Social.localUser.authenticated) {
			Social.ReportScore (score, LEADERBOARDS_SCORE, (bool success) => {
				if (success) {
					// Write success criteria
				} else {
					// Write failure criteria
				}
			});
		}
	}
}