using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuController : MonoBehaviour {

	public static MenuController instance;

	[SerializeField]
	private GameObject[] birds;
	[SerializeField]
	private Animator notificationAnim;
	[SerializeField]
	private Text notificationText;
	private bool isGreenBirdUnlocked, isRedBirdUnlocked;

	private void Awake () {
		MakeInstance ();
	}

	private void Start () {
		birds [GameController.instance.GetSelectedBird()].SetActive (true);
		CheckIfBirdsAreUnlocked ();
	}

	private void MakeInstance () {
		if (instance == null) {
			instance = this;
		}
	}

	public void ConnectOnGooglePlayGames () {
		LeaderboardsController.instance.ConnectOrDisconnectOnGooglePlayGames ();
	}

	public void OpenLeaderboardsScore () {
		LeaderboardsController.instance.OpenLeaderboardsScore ();
	}

	public void ConnectOnTwitter () {
		SocialMediaController.instance.LogInOrLogOutTwitter ();
	}

	public void ShareOnTwitter () {
		SocialMediaController.instance.ShareOnTwitter ();
	}

	public void PlayGame () {
		SceneFader.instance.FadeIn ("main");
	}

	public void ChangeBird () {
		if (GameController.instance.GetSelectedBird () == 0) {
			if (isGreenBirdUnlocked) {
				birds [0].SetActive (false);
				GameController.instance.SetSelectedBird (1);
				birds [GameController.instance.GetSelectedBird ()].SetActive (true);
			}
		} else if (GameController.instance.GetSelectedBird () == 1) {
			if (isRedBirdUnlocked) {
				birds [1].SetActive (false);
				GameController.instance.SetSelectedBird (2);
				birds [GameController.instance.GetSelectedBird ()].SetActive (true);
			} else {
				birds [1].SetActive (false);
				GameController.instance.SetSelectedBird (0);
				birds [GameController.instance.GetSelectedBird ()].SetActive (true);
			}
		} else if (GameController.instance.GetSelectedBird () == 2) {
			birds [2].SetActive (false);
			GameController.instance.SetSelectedBird (0);
			birds [GameController.instance.GetSelectedBird ()].SetActive (true);
		}
	}

	private void CheckIfBirdsAreUnlocked () {
		if (GameController.instance.IsRedBirdUnlocked () == 1) {
			isRedBirdUnlocked = true;
		}
		if (GameController.instance.IsGreenBirdUnlocked () == 1) {
			isGreenBirdUnlocked = true;
		}
	}

	public void NotificationMessage (string message) {
		StartCoroutine(AnimateNotificationPanel (message));
	}

	private IEnumerator AnimateNotificationPanel (string message) {
		notificationAnim.Play ("notificationSlideIn");
		notificationText.text = message;
		yield return StartCoroutine (MyCoroutine.WaitForRealSeconds (2f));
		notificationAnim.Play ("notificationSlideOut");
	}
}
