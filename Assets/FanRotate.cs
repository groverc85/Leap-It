using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanRotate : MonoBehaviour {

//	public GameObject character;
	public GameObject fan;
	float speed = 1.0f;

	// Use this for initialization
	void Start () {
		StartCoroutine (RotateObject (360, Vector3.up, 1));
//		FanMove ();
	}
		

	IEnumerator RotateObject(float angle, Vector3 axis, float inTime)
	{
		// calculate rotation speed
		float rotationSpeed = 4 * angle / inTime;

		while (true)
		{
			yield return new WaitForSeconds(4);

			// save starting rotation position
			Quaternion startRotation = fan.transform.rotation;

			float deltaAngle = 0;

			// rotate until reaching angle
			while (deltaAngle < angle*2)
			{
				deltaAngle += rotationSpeed * Time.deltaTime;
				deltaAngle = Mathf.Min(deltaAngle, angle*10);

				fan.transform.rotation = startRotation * Quaternion.AngleAxis(deltaAngle, axis);

				yield return null;
			}

//			yield return new WaitForSeconds(15);
		}
	}
	
//	IEnumerator FanMove () {
//		while (true)
//		{
//			float distance = 0;
//
//			while (distance < 5) {
//				character.transform.position -= character.transform.forward * speed * (Time.deltaTime);
//				distance += speed * (Time.deltaTime);
//
//				yield return null;
//			}
//
//			// delay here
//			yield return new WaitForSeconds(5);
//		}
//
//
//	}
}
