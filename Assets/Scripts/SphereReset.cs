using UnityEngine;
using System.Collections;

public class SphereReset : MonoBehaviour {

	public GameObject pickup1;
	public GameObject pickup2;
	public GameObject pickup3;
	public GameObject pickup4;
	public GameObject pickup5;
	public GameObject pickup6;
	public GameObject pickup7;
	public GameObject pickup8;
	public GameObject pickup9;
	public GameObject pickup10;
	public GameObject FinText;
	private int count;
	private int total = 10;
	float myTimer;
	GameObject[] pickups;

	void Start () {
		count = 0;
		myTimer = 0.0f;
		pickups = new GameObject[total];
		pickups [0] = pickup1;
		pickups [1] = pickup2;
		pickups [2] = pickup3;
		pickups [3] = pickup4;
		pickups [4] = pickup5;
		pickups [5] = pickup6;
		pickups [6] = pickup7;
		pickups [7] = pickup8;
		pickups [8] = pickup9;
		pickups [9] = pickup10;
		for (int i = 1; i<total; i++) {
			pickups[i].SetActive(false);
		}
		
	}

	// Update is called once per frame
	void Update () {

		if (transform.position.y < -5)
		{
			
			Application.LoadLevel ("BalanceTest");
			
		}

		if (count > 0 && count < total) {
			myTimer += Time.deltaTime;
		}

		if (count == total) {
			FinText.GetComponent<TextMesh>().text = myTimer.ToString () + " seconds";
		}

	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Pickup") {
			other.gameObject.SetActive (false);
			count += 1;
			if(count<total) {
				pickups[count].SetActive(true);
			} 
		}

	}
}
