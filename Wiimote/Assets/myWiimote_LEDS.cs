using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WiimoteApi;

public class myWiimote_LEDS : MonoBehaviour {
	Wiimote wiimote;
	// Use this for initialization
	void Start () {




		Debug.Log ("FindWiimote: " +WiimoteManager.FindWiimotes());
		Debug.Log ("HasWiimote: "+WiimoteManager.HasWiimote());
		Debug.Log ("#Wiimotes: " + WiimoteManager.Wiimotes.Count);
		wiimote = WiimoteManager.Wiimotes[0];

		StartCoroutine (waiter());

		//WiimoteManager.Cleanup (wiimote);
		//Debug.Log ("FindWiimote: " +);

		//Debug.Log ("WiimotesList: " +WiimoteManager.Wiimotes.));

		//bool haswm=WiimoteManager.HasWiimote();

	}
	
	// Update is called once per frame
	void Update () {




		
	}

	IEnumerator waiter(){
		while(true){
			wiimote.SendPlayerLED (true,false,false,false);
			yield return new WaitForSeconds (1);
			wiimote.SendPlayerLED (false,true,false,false);
			yield return new WaitForSeconds (1);
			wiimote.SendPlayerLED (false,false,true,false);
			yield return new WaitForSeconds (1);
			wiimote.SendPlayerLED (false,false,false,true);
			yield return new WaitForSeconds (1);
		}
	}
}
