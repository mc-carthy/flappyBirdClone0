using UnityEngine;
using System.Collections;

public class BackgroundCollector : MonoBehaviour {

	private GameObject[] backgrounds, grounds;

	private float lastBackgroundX, lastGroundX;


	private void Awake () {
		backgrounds = GameObject.FindGameObjectsWithTag ("background");
		grounds = GameObject.FindGameObjectsWithTag ("ground");

		lastBackgroundX = backgrounds [0].transform.position.x;
		lastGroundX = grounds [0].transform.position.x;

		for (int i = 1; i < backgrounds.Length; i++) {
			if (backgrounds [i].transform.position.x > lastBackgroundX) {
				lastBackgroundX = backgrounds [i].transform.position.x;
			}
		}

		for (int i = 1; i < grounds.Length; i++) {
			if (grounds [i].transform.position.x > lastGroundX) {
				lastGroundX = grounds [i].transform.position.x;
			}
		}
	}

	private void OnTriggerEnter2D (Collider2D trig) {
		if (trig.tag == "background") {
			Vector3 temp = trig.transform.position;
			float width = ((BoxCollider2D)(trig)).size.x;
			temp.x = lastBackgroundX + width;
			trig.transform.position = temp;
			lastBackgroundX = temp.x;
		} else if (trig.tag == "ground") {
			Vector3 temp = trig.transform.position;
			float width = ((BoxCollider2D)(trig)).size.x;
			temp.x = lastGroundX + width;
			trig.transform.position = temp;
			lastGroundX = temp.x;
		}
	}
}
