using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using Soomla;
using Soomla.Profile;

public class SocialMediaController : MonoBehaviour {

	public static SocialMediaController instance;

	private void Awake () {
		MakeSingleton ();
	}

	private void Start () {
		SoomlaProfile.Initialize ();
	}

	private void MakeSingleton () {
		if (instance == null) {
			instance = this;
			DontDestroyOnLoad (gameObject);
		} else {
			Destroy (gameObject);
		}
	}

	public void LogInOrLogOutTwitter () {
		if (SoomlaProfile.IsLoggedIn (Provider.TWITTER)) {
			SoomlaProfile.Logout (Provider.TWITTER);
		} else {
			SoomlaProfile.Login (Provider.TWITTER);
		}
	}

	public void ShareOnTwitter () {
		if (SoomlaProfile.IsLoggedIn (Provider.TWITTER)) {
			SoomlaProfile.UpdateStory (
				Provider.TWITTER, 
				"I'm playing this awesome game!",
				null,
				null,
				null,
				"www.mccarthy.tech",
				null,
				null,
				null

			);
		} else {
			if (SceneManager.GetActiveScene ().name == "menu") {
				MenuController.instance.NotificationMessage ("Please Connect In Order To Post");
			}
		}
	}

	public void OnEnable() {
		ProfileEvents.OnLoginFinished += onLoginFinished;
		ProfileEvents.OnLoginFailed += onLoginFailed;
		ProfileEvents.OnLoginCancelled += onLoginCancelled;
		ProfileEvents.OnLogoutFinished += onLogoutFinished;
		ProfileEvents.OnLogoutFailed += onLogoutFailed;
		ProfileEvents.OnSocialActionFinished += onSocialActionFinished;
		ProfileEvents.OnSocialActionFailed += onSocialActionFailed;
		ProfileEvents.OnSocialActionCancelled += onSocialActionCancelled;
	}

	public void OnDisable() {
		ProfileEvents.OnLoginFinished -= onLoginFinished;
		ProfileEvents.OnLoginFailed -= onLoginFailed;
		ProfileEvents.OnLoginCancelled -= onLoginCancelled;
		ProfileEvents.OnLogoutFinished -= onLogoutFinished;
		ProfileEvents.OnLogoutFailed -= onLogoutFailed;
		ProfileEvents.OnSocialActionFinished -= onSocialActionFinished;
		ProfileEvents.OnSocialActionFailed -= onSocialActionFailed;
		ProfileEvents.OnSocialActionCancelled -= onSocialActionCancelled;
	}

	private void onLoginFinished (UserProfile userProfileJson, bool boolean, string payload) {
		if (SceneManager.GetActiveScene ().name == "menu") {
			MenuController.instance.NotificationMessage ("Connected");
		}
	}

	private void onLoginFailed (Provider provider, string message, bool boolean, string payload) {
		if (SceneManager.GetActiveScene ().name == "menu") {
			MenuController.instance.NotificationMessage ("Login Failed");
		}
	}

	private void onLoginCancelled (Provider provider, bool boolean, string payload) {
		if (SceneManager.GetActiveScene ().name == "menu") {
			MenuController.instance.NotificationMessage ("Login Cancelled");
		}
	}

	private void onLogoutFinished (Provider provider) {
		if (SceneManager.GetActiveScene ().name == "menu") {
			MenuController.instance.NotificationMessage ("Disconnected");
		}
	}

	private void onLogoutFailed (Provider provider, string message) {
		if (SceneManager.GetActiveScene ().name == "menu") {
			MenuController.instance.NotificationMessage ("Could Not Disconnect");
		}
	}

	private void onSocialActionFinished (Provider provider, SocialActionType action, string payload) {
		if (provider == Provider.TWITTER) {
			if (action == SocialActionType.UPDATE_STORY) {
				if (SceneManager.GetActiveScene ().name == "menu") {
					MenuController.instance.NotificationMessage ("Thank You For Sharing");
				}
			}
		}
	}

	private void onSocialActionCancelled (Provider provider, SocialActionType action, string payload) {
		if (provider == Provider.TWITTER) {
			if (action == SocialActionType.UPDATE_STORY) {
				if (SceneManager.GetActiveScene ().name == "menu") {
					MenuController.instance.NotificationMessage ("Could Not Post");
				}
			}
		}
	}

	private void onSocialActionFailed (Provider provider, SocialActionType action, string message, string payload) {
		if (provider == Provider.TWITTER) {
			if (action == SocialActionType.UPDATE_STORY) {
				if (SceneManager.GetActiveScene ().name == "menu") {
					MenuController.instance.NotificationMessage ("Connection Failed");
				}
			}
		}
	}
}
