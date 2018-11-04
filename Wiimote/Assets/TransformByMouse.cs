using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformByMouse : MonoBehaviour {

	float speed = 50.0f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		Debug.Log("Mouse_X: "+Input.GetAxisRaw ("Mouse X")+" Mouse_Y: "+Input.GetAxisRaw ("Mouse Y"));

		transform.position += new Vector3 (Input.GetAxisRaw ("Mouse X") * Time.deltaTime * speed, Input.GetAxisRaw ("Mouse Y") * Time.deltaTime * speed,  Input.GetAxisRaw ("Mouse ScrollWheel") * Time.deltaTime * speed);
	}
	
}
