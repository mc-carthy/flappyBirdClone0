using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public static PlayerController instance;

	public bool isAlive;
	public int score;

	[SerializeField]
	private Rigidbody2D rb;
	[SerializeField]
	private Animator anim;
	[SerializeField]
	private float forwardSpeed = 3f, bounceSpeed = 4f;
	[SerializeField]
	private AudioSource audioSource;
	[SerializeField]
	private AudioClip flapClip, pointClip, dieClip;
	private bool didFlap;
	private Button flapButton;

	private void Awake () {
		isAlive = true;
		MakeInstance ();
		flapButton = GameObject.FindGameObjectWithTag ("flapButton").GetComponent<Button> ();
		//if (this.isActiveAndEnabled) {
			flapButton.onClick.AddListener (() => FlapBird ());
		//}
		SetCameraX ();
	}

	private void FixedUpdate () {

		if (isAlive) {
			Vector3 temp = transform.position;
			temp.x += forwardSpeed * Time.deltaTime;
			transform.position = temp;

			if (didFlap) {
				didFlap = false;
				rb.velocity = new Vector2 (0, bounceSpeed);
				audioSource.PlayOneShot (flapClip);
				anim.SetTrigger ("Flap");
			}

			if (rb.velocity.y >= 0) {
				float angle = 0;
				angle = Mathf.Lerp (0, 45, rb.velocity.y / 4);
				transform.rotation = Quaternion.Euler (0, 0, angle);
			} else {
				float angle = 0;
				angle = Mathf.Lerp (0, -90, -rb.velocity.y / 10);
				transform.rotation = Quaternion.Euler (0, 0, angle);
			}
		}
	}

	private void OnCollisionEnter2D (Collision2D col) {
		if (col.gameObject.tag == "ground" || col.gameObject.tag == "pipe") {
			if (isAlive) {
				Die ();
			}
		}
	}

	private void OnTriggerEnter2D (Collider2D trig) {
		if (trig.tag == "pipeHolder") {
			ScorePoint ();
		}
	}

	private void MakeInstance () {
		if (instance == null) {
			instance = this;
		}
	}


	private void SetCameraX () {
		CameraController.offsetX = (Camera.main.transform.position.x - transform.position.x) - 1;
	}

	private void Die () {
		isAlive = false;
		anim.SetTrigger("Die");
		audioSource.PlayOneShot (dieClip);
		GamePlayController.instance.PlayerDiedSetScore (score);
		AdsController.instance.ShowInterstital ();
	}

	private void ScorePoint () {
		audioSource.PlayOneShot (pointClip);
		score++;
		GamePlayController.instance.SetScore (score);
	}

	public void FlapBird () {
		if (isAlive) {
			didFlap = true;
			audioSource.PlayOneShot (flapClip);
		}	
	}

	public float GetPositionX () {
		return transform.position.x;
	}
}
