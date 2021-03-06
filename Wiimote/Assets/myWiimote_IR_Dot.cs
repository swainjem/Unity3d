﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WiimoteApi;

public class myWiimote_IR_Dot : MonoBehaviour {
	Wiimote wiimote;
	public float x1, y1, x2, y2;
	public RectTransform Dot1;
	public RectTransform Dot2;

	// Use this for initialization
	void Start () {

		//Debug.Log ("FindWiimote: " +WiimoteManager.FindWiimotes());
		Debug.Log ("Wiimote Found: "+WiimoteManager.HasWiimote());
		//Debug.Log ("#Wiimotes: " + WiimoteManager.Wiimotes.Count);


		
	}
		
	// Update is called once per frame
	void Update () {



		//PRESS SPACE TO FIND WIIMOTE
		if (Input.GetKeyDown(KeyCode.Space) && !WiimoteManager.HasWiimote ()){
			Debug.Log("Space pressed");
			WiimoteManager.FindWiimotes ();
			Debug.Log("Wiimote Found: "+WiimoteManager.HasWiimote ());
			wiimote = WiimoteManager.Wiimotes[0];
			wiimote.SendPlayerLED (true,false,false,false);
			wiimote.SetupIRCamera (IRDataType.BASIC);
		}

		//RETURN IF ANY WIIMOTE IS FOUND
		if (!WiimoteManager.HasWiimote () || wiimote == null ) return;
			
		//GET IR DATA
		int ret;

		do {
			
			ret = wiimote.ReadWiimoteData ();

			float[,] ir = wiimote.Ir.GetProbableSensorBarIR ();
			x1 = (float)ir [0, 0] / 1023f;
			y1 = (float)ir [0, 1] / 767f;

			x2 = (float)ir [1, 0] / 1023f;
			y2 = (float)ir [1, 1] / 767f;

			Debug.Log ("x1: " + x1 + "     y1: " + y1 + "     x2: " + x2 + "     y2: " + y2);

			Dot1.anchorMin = new Vector2(x1,y1);
			Dot1.anchorMax = new Vector2(x1,y1);

			Dot2.anchorMin = new Vector2(x2,y2);
			Dot2.anchorMax = new Vector2(x2,y2);
		



		//PRESS BACKSPACE TO CLEAN UP
		} while (ret > 0);



		if (Input.GetKeyDown (KeyCode.Backspace)) {
			Debug.Log ("Backspace pressed!!!");
			//wiimote.SendPlayerLED (false,false,false,true);
			WiimoteManager.Cleanup(wiimote);
			wiimote = null;

		}

		//if (wiimote == null) return;

		/*
		//GREEN POINT
		float[] pointer = wiimote.Ir.GetPointingPosition();
		Debug.Log ("pointer0: "+pointer[0]);
		Debug.Log ("pointer1: "+pointer[1]);
		*/

	}
}
