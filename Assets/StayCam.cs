using UnityEngine;
using System.Collections;

public class StayCam : MonoBehaviour {

	// Update is called once per frame
	void Update () {
		Vector3 rotation = transform.localEulerAngles;
		rotation.x = 0;
		rotation.z = 0;
		transform.localEulerAngles = rotation;
		Vector3 position = transform.position;
		position.y = 5;
		transform.position = position;
	}
}
