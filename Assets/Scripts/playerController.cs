using UnityEngine;
using System.Collections;

public class playerController : MonoBehaviour {

	public static playerController instance;

	public bool isAlive;

	[SerializeField]
	private Rigidbody2D rb;
	[SerializeField]
	private Animator anim;
	[SerializeField]
	private float forwardSpeed = 3f, bounceSpeed = 4f;
	private bool didFlap;

	private void Awake () {
		isAlive = true;
		MakeSingleton ();
	}

	private void FixedUpdate () {
		if (isAlive) {
			Vector3 temp = transform.position;
			temp.x += forwardSpeed * Time.fixedDeltaTime;
			transform.position = temp;

			if (didFlap) {
				didFlap = false;
				rb.velocity = new Vector2 (0, bounceSpeed);
				anim.SetTrigger ("Flap");
			}
		}
	}

	public void FlapBird () {
		didFlap = true;
	}

	private void MakeSingleton () {
		if (instance == null) {
			instance = this;
			DontDestroyOnLoad (gameObject);;
		} else {
			Destroy (gameObject);
		}
	}
}
