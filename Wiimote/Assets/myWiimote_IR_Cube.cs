using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WiimoteApi;

public class myWiimote_IR_Cube : MonoBehaviour {
	Wiimote wiimote;
	public float x1, y1, x2, y2;
	public Transform Cube;
	// Use this for initialization
	void Start () {

		Debug.Log ("FindWiimote: " +WiimoteManager.FindWiimotes());
		Debug.Log ("Wiimote Found: "+WiimoteManager.HasWiimote());
		//Debug.Log ("#Wiimotes: " + WiimoteManager.Wiimotes.Count);
		wiimote = WiimoteManager.Wiimotes[0];
		wiimote.SendPlayerLED(true, false, false, false);
		wiimote.SetupIRCamera(IRDataType.BASIC);


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
			x1 = (float)ir [0, 0];
			y1 = (float)ir [0, 1];

			x2 = (float)ir [1, 0];// / 1023f;
			y2 = (float)ir [1, 1];// / 767f;

			Debug.Log ("x1: " + x1 + "     y1: " + y1 + "     x2: " + x2 + "     y2: " + y2);

			//transform.position = new Vector3( x1 * Time.deltaTime, 0.0f, y1 * Time.deltaTime);
			//Cube.transform.position = new Vector3( x2 * Time.deltaTime * 100.0f ,0.0f ,0.0f);

			if(x1!=-1 && y1!=-1)
				transform.position = new Vector3(-x1 ,y1 ,-100);
			else 
				transform.position = new Vector3(-512, 384, -100);

			//transform.LookAt(Cube);




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
