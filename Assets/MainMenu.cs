using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	public bool isStart;
	public bool isQuit;

	void ChangeLevel() {
		SceneManager.LoadScene (1);	
	}

	void OnMouseUp(){
		if(isStart)
		{
//			Invoke( "ChangeLevel", 3.0f );
			SceneManager.LoadScene (1);	

		}
		if (isQuit)
		{
			Application.Quit();
		}
	} 
}



