using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public static float offsetX;

	private void Update () {
		if (PlayerController.instance != null && PlayerController.instance.isAlive) {
			MoveCamera ();
		}
	}

	private void MoveCamera () {
		Vector3 temp = transform.position;
		temp.x = PlayerController.instance.GetPositionX () + offsetX;
		transform.position = temp;
	}
}
