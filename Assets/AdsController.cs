using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class AdsController : MonoBehaviour {

	public static AdsController instance;

	private const string SDK_KEY = "O4BLxNskdlPeu6mFM2hQVQixnKBgiLn6QyRMJI4y1UavaL9yOsNuG4q2P_i6LTCnQe7lxBgygFsFb_C-sZs7ef";

	private void Awake () {
		MakeSingleton ();
	}

	private void Start () {
		AppLovin.SetSdkKey (SDK_KEY);
		AppLovin.InitializeSdk ();
		AppLovin.SetUnityAdListener (this.gameObject.name);
		StartCoroutine (CallAds ());
	}

	private void MakeSingleton () {
		if (instance == null) {
			instance = this;
			DontDestroyOnLoad (gameObject);
		} else {
			Destroy (gameObject);
		}
	}

	private void OnLevelWasLoaded () {
		if (SceneManager.GetActiveScene ().name == "menu") {
			int random = Random.Range (0, 10);
			if (random > 4) {
				ShowInterstital ();
			} else {
				ShowVideo ();
			}
		}
	}

	private IEnumerator CallAds () {
		yield return StartCoroutine (MyCoroutine.WaitForRealSeconds (3f));
		LoadInterstitial ();
		LoadVideo ();
		AppLovin.ShowAd (AppLovin.AD_POSITION_TOP, AppLovin.AD_POSITION_CENTER);
	}

	public void LoadInterstitial () {
		AppLovin.PreloadInterstitial ();
	}

	public void ShowInterstital () {
		if (AppLovin.HasPreloadedInterstitial()) {
			AppLovin.ShowInterstitial();
		} else {
			LoadInterstitial ();
		}
	}

	public void LoadVideo () {
		AppLovin.LoadRewardedInterstitial ();
	}

	public void ShowVideo () {
		AppLovin.ShowRewardedInterstitial ();
	}

	private void onAppLovinEventReceived(string ev){
		if(ev.Contains("DISPLAYEDINTER")) {
			// An ad was shown.  Pause the game.
		}
		else if(ev.Contains("HIDDENINTER")) {
			// Ad ad was closed.  Resume the game.
			// If you're using PreloadInterstitial/HasPreloadedInterstitial, make a preload call here.
			AppLovin.PreloadInterstitial();
		}
		else if(ev.Contains("LOADEDINTER")) {
			// An interstitial ad was successfully loaded.
		}
		else if(string.Equals(ev, "LOADINTERFAILED")) {
			// An interstitial ad failed to load.
		}
	}

	/*
	private void onAppLovinEventReceived(string ev){
		if(ev.Contains("REWARDAPPROVEDINFO")){

			// The format would be "REWARDAPPROVEDINFO|AMOUNT|CURRENCY" so "REWARDAPPROVEDINFO|10|Coins" for example
			string delimeter = "|";

			// Split the string based on the delimeter
			string[] split = ev.Split(delimeter);

			// Pull out the currency amount
			double amount = double.Parse(split[1]);

			// Pull out the currency name
			string currencyName = split[2];

			// Do something with the values from above.  For example, grant the coins to the user.
			updateBalance(amount, currencyName);
		}
		else if(ev.Contains("LOADEDREWARDED")) {
			// A rewarded video was successfully loaded.
		}
		else if(ev.Contains("LOADREWARDEDFAILED")) {
			// A rewarded video failed to load.
		}
		else if(ev.Contains("HIDDENREWARDED")) {
			// A rewarded video was closed.  Preload the next rewarded video.
			AppLovin.LoadRewardedInterstitial();
		}
	}
	*/
}
