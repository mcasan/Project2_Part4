using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube_Behavior : MonoBehaviour {
	GameController myGameController;

	public int myX, myY;

	///Use this for initialization
	void Start(){

		myGameController = GameObject.Find ("GameControllerObject").GetComponent<GameController> ();
	}

	void Update(){

	}

	void OnMouseDown() {

		myGameController.ProcessClick (gameObject, myX, myY);
	}
}