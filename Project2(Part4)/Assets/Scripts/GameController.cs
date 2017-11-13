using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public GameObject cubePrefab;

	public Text cargoScoreText; 

	GameObject activeCube;

	GameObject[,]cubeRow;

	Vector3 cubePosition;

	public static int Xairplane, Yairplane, startX,startY;

	public GameObject currentCube;

	public static GameObject redcube;

	public static GameObject blackcube;

	int score, cargoGain;

	float turnLength, turnTimer;

	int airplaneCargo, airplaneCargoMax;

	int depotX, depotY ;

	bool airplaneActive;

	int moveX, moveY;

	// Use this for initialization


	/// This is to position the cubes, declare that "cliked is false", create a position to put a red cube at the top left of the game and, to make cubes no more than 16 x 9.
	void Start () {


		cargoScoreText.text = "Cargo: " + airplaneCargo + "  Score: " + score;

		turnLength = 1.5f;

		turnTimer = turnLength;

		score = 0;

		airplaneCargo = 0;

		airplaneCargoMax = 90;

		cargoGain = 10;


		cubeRow = new GameObject[16,9];

		Xairplane = 0;

		Yairplane = 8;

		startX = Xairplane;

		startY = Yairplane;

		for (int y = 0; y < 9; y++){
			

			for (int x = 0; x < 16; x++) {

				////This is goint to make that the cubes stop after going 16 times in one x position and, it is going to stop after 9 times in one y position. 
		 		cubePosition = new Vector3 (x * 2, y * 2 - 8, 0);

				cubeRow [x, y] = (GameObject)Instantiate (cubePrefab, cubePosition, Quaternion.identity);

				cubeRow [x, y].GetComponent<Renderer> ().material.color = Color.white;

				cubeRow [x, y].GetComponent<Cube_Behavior> ().myX = x;

				cubeRow [x, y].GetComponent<Cube_Behavior> ().myY = y;
			 }
		}

		//// airplane will start at the upper left 

		cubeRow [Xairplane,Yairplane] .GetComponent<Renderer> ().material.color = Color.red;
		redcube = cubeRow [Xairplane, Yairplane];

		airplaneActive = false;

		depotX = 16-1;
		depotY = 0;
		blackcube = cubeRow [depotX, depotY];
		cubeRow [depotX, depotY].GetComponent<Renderer> ().material.color = Color.black;

		moveX = 0;
		moveY = 0;
	
	}


	public void ProcessClick (GameObject clikedCube,int x, int y ){
			/// What the airplane was cliked???
	if (Xairplane == x && Yairplane == y) { 
		if (airplaneActive){



					///deactive it 
					airplaneActive = false;
					clikedCube.transform.localScale /= 1.5f;
	
	 	} else {
			
					/// activate it 
					airplaneActive = true;
		 			clikedCube.transform.localScale *= 1.5f;
		}
	}
}
	void LoadCargo() {

		if (Xairplane == startX && Yairplane == startY) {
			airplaneCargo += cargoGain;

			if (airplaneCargo > airplaneCargoMax) {
			
				airplaneCargo = airplaneCargoMax;
			
			}
		
		}


	}

	void DeliverCargo() {
		if (Xairplane == depotX && Yairplane == depotY) {
		
			score += airplaneCargo;

			airplaneCargo = 0;
		
		
		}
	}

	void DetectKeyboardInput(){

		if (Input.GetKeyDown (KeyCode.DownArrow)) {

			moveY = -1;
			moveX =  0;
		}

		else if (Input.GetKeyDown (KeyCode.UpArrow)) {

			moveY =  1;
			moveX =  0;
		}

		else if (Input.GetKeyDown (KeyCode.RightArrow)) {

			moveY =  0;
			moveX =  1;
		}

		else if (Input.GetKeyDown (KeyCode.LeftArrow)) {

			moveY =  0;
			moveX = -1;
		}

	}

	void MoveAirplane() {


		if (airplaneActive) {


				/// Remove the airplane from it's old spot, if it's the depot set it to black
				if (Xairplane == depotX && Yairplane == depotY){
					cubeRow [depotX, depotY].GetComponent<Renderer> ().material.color = Color.black;
				}
				////otherwise,set it to white
				else {

					cubeRow [Xairplane, Yairplane].GetComponent<Renderer> ().material.color = Color.white;

				}

				cubeRow [Xairplane, Yairplane].transform.localScale /= 1.5f;

				///put the airplane in it's new spot 

			Xairplane += moveX;
			Yairplane += moveY;

			/// check to ensure that the plane doesn't go out of bounds

			if (Xairplane >= 16) {

				Xairplane = 15;

			} else if (Xairplane < 0){

				Xairplane = 0;

			}

			if (Yairplane >= 9) {

				Yairplane = 8;

			} else if (Yairplane < 0){

				Yairplane = 0;

			}

			cubeRow [Xairplane, Yairplane].GetComponent<Renderer> ().material.color = Color.red;
			cubeRow [Xairplane, Yairplane].transform.localScale *= 1.5f;

				}

		//// Reset movement for next turn
	
		moveX = 0;
		moveY = 0;

	}

 	// Update is called once per frame
	void Update (){

		DetectKeyboardInput ();

		if (Time.time > turnTimer) {

			MoveAirplane ();

			LoadCargo ();

			DeliverCargo ();

			cargoScoreText.text = "Cargo: " + airplaneCargo + "  Score: " + score;

			turnTimer += turnLength;

		}



	}



}

	
	
	

