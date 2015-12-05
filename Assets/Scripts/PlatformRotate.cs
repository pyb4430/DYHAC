using UnityEngine;
using System.Collections;

public class PlatformRotate : MonoBehaviour {
	
	public float speed;

	// Use this for initialization
	void Start () {
		// Activate the gyroscope
		Input.gyro.enabled = true;
	}
	
	void Update ()
	{
		float rotatex = Input.acceleration.x;
		float rotatez = Input.acceleration.z;

		//float rotatex = Input.GetAxis ("Horizontal");
		//float rotatez = Input.GetAxis ("Vertical");
		
		Vector3 movement = new Vector3 (rotatex, 0.0f, rotatez);
		
		//transform.Rotate (rotatex*Time.deltaTime*speed, 0.0f, rotatez*Time.deltaTime*speed, Space.Self);
		//transform.Rotate (-Input.gyro.rotationRateUnbiased.x, 0, 0);
		//transform.Rotate (0, 0, Input.gyro.rotationRateUnbiased.z);
		transform.rotation = Input.gyro.attitude;
	}

}

