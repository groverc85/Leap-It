using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMove : MonoBehaviour {
	// Use this for initialization
	void Start(){
		transform.GetComponent<MeshRenderer> ().material.SetColor ("_Color", Color.white);
	}

	void OnMouseEnter(){
		transform.GetComponent<MeshRenderer> ().material.SetColor ("_Color", Color.grey);
	}

	void OnMouseExit() {
		transform.GetComponent<MeshRenderer> ().material.SetColor ("_Color", Color.white);
	}
}
