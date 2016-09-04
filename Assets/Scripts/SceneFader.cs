using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneFader : MonoBehaviour {

	public static SceneFader instance;

	[SerializeField]
	private GameObject fadeCanvas;
	[SerializeField]
	private Animator fadeAnim;

	private void Awake () {
		MakeSingleton ();
	}

	private void MakeSingleton () {
		if (instance == null) {
			instance = this;
			DontDestroyOnLoad (gameObject);
		} else {
			Destroy (gameObject);
		}
	}

	public void FadeIn (string levelName) {
		StartCoroutine (FadeInAnimation (levelName));
	}

	public void FadeOut () {
		StartCoroutine (FadeOutAnimation ());
	}

	private IEnumerator FadeInAnimation (string levelName) {
		fadeCanvas.SetActive (true);
		fadeAnim.Play ("fadeIn");
		yield return StartCoroutine(MyCoroutine.WaitForRealSeconds (0.7f));
		SceneManager.LoadScene (levelName, LoadSceneMode.Single);
		FadeOut ();
	}

	private IEnumerator FadeOutAnimation () {
		fadeAnim.Play ("fadeOut");
		yield return StartCoroutine(MyCoroutine.WaitForRealSeconds (0.2f));
		fadeCanvas.SetActive (false);
	}
}
