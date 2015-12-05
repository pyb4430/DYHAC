using UnityEngine;
using System.Collections;

public class RxnTimeInst : MonoBehaviour {
	
	public GameObject spotlight;
	public GameObject pickup;
	public GameObject FinText;
	private int stimnum = 8;
	private int numTrials = 10;
	private bool timing;
	private bool waiting;
	public float average;
	float myTime;
	int timeIndex;
	float[] rxnTimes;
	private int rand;

	
	// Use this for initialization
	void Start () {
		//Screen.orientation = ScreenOrientation.LandscapeRight;
		FinText.GetComponent<TextMesh> ().text = "";
		FinText.GetComponent<TextMesh>().text = "Look at the cube so that it glows\ngreen. When a sphere appears somewhere\nin your peripheral vision, turn your head\nto look at it as fast as possible, then\nlook back at the cube. Look at the cube to continue";
		spotlight.SetActive (false);
		timing = false;
		waiting = true;
		timeIndex = 0;
		myTime = 0;
	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit hit;
		Ray LOSray = new Ray (transform.position, transform.forward);
		
		Debug.DrawRay (transform.position, transform.forward*20);
		
		if (!waiting) {
			if (Physics.Raycast (LOSray, out hit, 20)) {
				
				if (hit.collider.tag == "Pickup") {
					spotlight.SetActive (true);
					myTime += Time.deltaTime;
				} else {
					spotlight.SetActive (false);
					myTime = 0;
				}
			} else {
				hit.point = Vector3.zero;
				spotlight.SetActive (false);
				myTime = 0;
			}

		} 

		if (waiting) {
			myTime += Time.deltaTime;
		}

		if (waiting && myTime > 5) {
			waiting = false;
			timing = true;
			myTime = 0;
		}

		if (timing && myTime > 5) {
			myTime += Time.deltaTime;
			Application.LoadLevel("RxnTime");
		}
	}
}
