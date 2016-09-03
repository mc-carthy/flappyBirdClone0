using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public static PlayerController instance;

	public bool isAlive;

	[SerializeField]
	private Rigidbody2D rb;
	[SerializeField]
	private Animator anim;
	[SerializeField]
	private float forwardSpeed = 3f, bounceSpeed = 4f;
	private bool didFlap;
	private Button flapButton;

	private void Awake () {
		isAlive = true;
		MakeSingleton ();
		flapButton = GameObject.FindGameObjectWithTag ("flapButton").GetComponent<Button> ();
		flapButton.onClick.AddListener (() => FlapBird ());
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

	private void MakeSingleton () {
		if (instance == null) {
			instance = this;
			DontDestroyOnLoad (gameObject);;
		} else {
			Destroy (gameObject);
		}
	}

	private void SetCameraX () {
		CameraController.offsetX = (Camera.main.transform.position.x - transform.position.x) - 1;
	}

	public void FlapBird () {
		didFlap = true;
	}

	public float GetPositionX () {
		return transform.position.x;
	}
}
