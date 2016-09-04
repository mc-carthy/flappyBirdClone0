﻿using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour {

	public static MenuController instance;

	[SerializeField]
	private GameObject[] birds;
	private bool isGreenBirdUnlocked, isRedBirdUnlocked;

	private void Awake () {
		MakeSingleton ();
	}

	private void Start () {
		birds [GameController.instance.GetSelectedBird()].SetActive (true);
		CheckIfBirdsAreUnlocked ();
	}

	private void MakeSingleton () {
		if (instance == null) {
			instance = this;
			DontDestroyOnLoad (gameObject);
		} else {
			Destroy(gameObject);
		}
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
}