using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;

public class LightMove : MonoBehaviour {


	public float speed = 10;
	public float spacing = 1;
	public Controller controller;

	private Vector3 pos;

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


		
		Hand RightHand; // The front most hand captured by the Leap Motion Controller

		// Check if the Leap Motion Controller is ready
		if ( !IsReady || Hands == null )
		{
			return;
		}

		RightHand = Hands.Rightmost;

		float right_pitch, right_yaw, right_roll;

		right_pitch = RightHand.Direction.Pitch;
		right_yaw = RightHand.Direction.Yaw;
		right_roll = RightHand.PalmNormal.Roll;

		transform.rotation = Quaternion.Euler( RightHand.Direction.Pitch, RightHand.Direction.Yaw, RightHand.PalmNormal.Roll );


		if (right_roll > 0.3)
			pos.x -= spacing/15;
		else if (right_roll < -0.3)
			pos.x += spacing/15;

		if (right_pitch > 0.3)
			pos.y -= spacing/15;
		else if (right_pitch < -0.3)
			pos.y += spacing/15;


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
