using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//#if UNITY_EDITOR
//using UnityEditor;
//#endif
using UnityEngine.SceneManagement;

public class BlockPlay : MonoBehaviour {
	public int numberObject;

	public GameObject[] cube;
	public GameObject character;
	public GameObject spotlight;
	public GameObject plane;


	double[] cube_up;
	double[] cube_left;
	double[] cube_right;

	double character_x;
	double character_y;
	double character_original_y;

	bool isFallen;
	bool isChecked;
	bool isPassedLevel;
	bool isFanOn;
	float speed = 0.2f;

	int currentBlockNumber = 0;

	void FanMove () {
//		character.transform.position -= character.transform.forward * speed * (Time.deltaTime);
		isFanOn = true;
	}

	void FanOff() {
		isFanOn = false;
	}

	// Use this for initialization
	void Start () {
		isFallen = false;
		isChecked = false;
		isPassedLevel = false;
		isFanOn = false;
		cube_up = new double[numberObject];
		cube_left = new double[numberObject];
		cube_right = new double[numberObject];

		character_y = (plane.transform.position.z - spotlight.transform.position.z) /
			(character.transform.position.z - spotlight.transform.position.z) *
			(character.transform.position.y - spotlight.transform.position.y) +
			spotlight.transform.position.y;

//		InvokeRepeating ("FanMove", 5, 5);
//		InvokeRepeating ("FanOff", 6, 5);
	}

	int detectBlock(double pos_x){
		for (int i = 0; i < numberObject; i++) {
			if (pos_x >= cube_left [i] && pos_x <= cube_right [i])
				return i;
		}
		return -1;
	}
	
	// Update is called once per frame
	void Update () {
		// compute the location of shadows
		if (!isFallen && !isFanOn) {
			character_x = (plane.transform.position.z - spotlight.transform.position.z) /
			(character.transform.position.z - spotlight.transform.position.z) *
			(character.transform.position.x - spotlight.transform.position.x) +
			spotlight.transform.position.x;


			for (int i = 0; i < numberObject; i++) {
				cube_up [i] = (plane.transform.position.z - spotlight.transform.position.z) /
				(cube [i].transform.position.z - spotlight.transform.position.z) *
				(cube [i].transform.position.y + 0.5 - spotlight.transform.position.y) +
				spotlight.transform.position.y;

				cube_left [i] = (plane.transform.position.z - spotlight.transform.position.z) /
				(cube [i].transform.position.z - spotlight.transform.position.z) *
				(cube [i].transform.position.x - 0.5 - spotlight.transform.position.x) +
				spotlight.transform.position.x;

				cube_right [i] = (plane.transform.position.z - spotlight.transform.position.z) /
				(cube [i].transform.position.z - spotlight.transform.position.z) *
				(cube [i].transform.position.x + 0.5 - spotlight.transform.position.x) +
				spotlight.transform.position.x;
			}

//			currentBlockNumber = detectBlock (character_x);
			character_y = cube_up [currentBlockNumber] + 0.2;

			character_original_y = (character.transform.position.z - spotlight.transform.position.z) /
			(plane.transform.position.z - spotlight.transform.position.z) *
			(character_y - spotlight.transform.position.y) + spotlight.transform.position.y;
				

			character.transform.position = new Vector3 (character.transform.position.x, (float)character_original_y, 
				character.transform.position.z);

			if (currentBlockNumber == numberObject - 1) {
				isPassedLevel = true;
//				EditorUtility.DisplayDialog ("Congrats!", "Level cleared", "OK", "Cancel");
				if (SceneManager.GetActiveScene ().buildIndex == 1)
					SceneManager.LoadScene (2);
				else
					Application.Quit ();
			}

//			if (Input.GetKeyDown ("space")) {
//				//				character.transform.position += character.transform.up * speed * (Time.deltaTime);
//				character.transform.position += character.transform.up * 200 * speed * (Time.deltaTime);
//
////				character.transform.Translate (Vector3.up * 260 * Time.deltaTime, Space.World);
//			}

			if (character_x >= cube_right [currentBlockNumber] && isChecked == false) {
				if (cube_left [currentBlockNumber + 1] - cube_right [currentBlockNumber] <= 1) {
					isFallen = false;
					currentBlockNumber++;
					isChecked = true;
				} else {
					isFallen = true;
				}
			} 
//				else if (character_x <= cube_left [currentBlockNumber]) {
//				isFallen = true;
//			} else {
//			}

				

			if (character_x >= cube_right [currentBlockNumber]) {
				isChecked = false;
			}

		} else if (isFallen) {
			character.transform.position -= character.transform.up * 3000 * speed * (Time.deltaTime) * (Time.deltaTime);
			if (character.transform.position.y <= -6) {
//				EditorUtility.DisplayDialog ("GAME OVER", "PRESS OK TO RESTART", "OK");
				SceneManager.LoadScene (1);
			}
		} else if (isFanOn) {
			character.transform.position -= character.transform.forward * 100 * speed * (Time.deltaTime) * (Time.deltaTime);
		}
			
	}
}
