using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;

public class CharacterMove : MonoBehaviour {

	private Vector3 pos;
	public Controller controller;
	public float speed = 10;
	public float spacing = 1;

	void Awake()
	{
		controller = new Controller();
	}


	// Use this for initialization
	void Start () {
		pos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		bool IsReady = ( controller != null && controller.Devices.Count > 0 && controller.IsConnected );

		Frame CurrentFrame = ( IsReady ) ? controller.Frame() : null;
		HandList Hands = ( CurrentFrame != null && CurrentFrame.Hands.Count > 0 ) ? CurrentFrame.Hands : null;


//		Hand RightHand; // The front most hand captured by the Leap Motion Controller
		Hand LeftHand;

		// Check if the Leap Motion Controller is ready
		if ( !IsReady || Hands == null )
		{
			return;
		}
			
		LeftHand = Hands.Leftmost;

		float left_pitch, left_yaw, left_roll;

		left_pitch = LeftHand.Direction.Pitch;
		left_yaw = LeftHand.Direction.Yaw;
		left_roll = LeftHand.PalmNormal.Roll;

		transform.rotation = Quaternion.Euler( LeftHand.Direction.Pitch, LeftHand.Direction.Yaw, LeftHand.PalmNormal.Roll );

		if (left_roll > 0.3)
			pos.x += spacing/30;
		else if (left_roll < -0.3)
			pos.x -= spacing/30;

//		if (left_pitch > 0.3)
//			pos.y -= spacing/40;
//		else if (left_pitch < -0.3)
//			pos.y += spacing/40;


		//		if (Input.GetKeyDown (KeyCode.W)) {
		//			pos.y += spacing;
		//			print ("W key detected");
		//		}
		//		if (Input.GetKeyDown(KeyCode.S))
		//			pos.y -= spacing;
		//		if (Input.GetKeyDown(KeyCode.A))
		//			pos.x -= spacing;
		//		if (Input.GetKeyDown(KeyCode.D))
		//			pos.x += spacing;
		//

		transform.position = Vector3.MoveTowards(transform.position, pos, speed * Time.deltaTime);

	}
}
