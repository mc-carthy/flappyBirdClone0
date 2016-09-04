using UnityEngine;
using System.Collections;

public class PipeCollector : MonoBehaviour {

	private GameObject[] pipeHolders;
	private float distance = 2.5f, lastPipeX, pipeMin = -2.5f, pipeMax = 2.5f;

	private void Awake () {
		pipeHolders = GameObject.FindGameObjectsWithTag ("pipeHolder");

		for (int i = 0; i < pipeHolders.Length; i++) {
			Vector3 temp = pipeHolders [i].transform.position;
			temp.y = Random.Range (pipeMin, pipeMax);
			pipeHolders [i].transform.position = temp;
		}

		lastPipeX = pipeHolders [0].transform.position.x;

		for (int i = 1; i < pipeHolders.Length; i++) {
			if (pipeHolders [i].transform.position.x > lastPipeX) {
				lastPipeX = pipeHolders [i].transform.position.x;
			}
		}

	}

	private void OnTriggerEnter2D (Collider2D trig) {
		if (trig.tag == "pipeHolder") {
			Vector3 temp = trig.transform.position;
			temp.x = lastPipeX + distance;
			temp.y = Random.Range (-2.5f, 2.5f);
			trig.transform.position = temp;
			lastPipeX = temp.x;
		}
	}
}
