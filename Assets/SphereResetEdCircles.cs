using UnityEngine;
using System.Collections;

public class SphereResetEdCircles : MonoBehaviour {
	
	public GameObject pickup1;
	public GameObject pickup2;
	public GameObject pickup3;
	public GameObject pickup4;
	public GameObject green1;
	public GameObject green2;
	public GameObject green3;
	public GameObject green4;
	public GameObject red1;
	public GameObject red2;
	public GameObject red3;
	public GameObject red4;
	private bool timing;
	private int count;
	private int total = 4;
	public float myTimer;
	GameObject[] pickups;
	GameObject[] greens;
	GameObject[] reds;
	
	void Start () {
		count = 0;
		myTimer = 0.0f;
		timing = true;
		pickups = new GameObject[total];
		pickups [0] = pickup1;
		pickups [1] = pickup2;
		pickups [2] = pickup3;
		pickups [3] = pickup4;
		greens = new GameObject[total];
		greens [0] = green1;
		greens [1] = green2;
		greens [2] = green3;
		greens [3] = green4;
		reds = new GameObject[total];
		reds [0] = red1;
		reds [1] = red2;
		reds [2] = red3;
		reds [3] = red4;

		for (int i = 1; i<total; i++) {
			pickups[i].SetActive(false);
			reds[i].SetActive(false);
		}

		for (int i = 0; i<total; i++) {
			greens [i].SetActive (false);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
		if (transform.position.y < -5)
		{
			
			Application.LoadLevel ("BalanceTestEd");
			
		}
		
		if (timing) {
			myTimer += Time.deltaTime;
			//FinText.GetComponent<TextMesh>().text = myTimer.ToString ();
		}
		


		
	}
	
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Pickup") {
			timing = true;
		}
		
	}

	void OnTriggerExit(Collider other) {
		if (myTimer < 20) {
			//cut off green light
			greens [count].SetActive (false);
			//turn on red light
			reds [count].SetActive (true);
			//restart timer
			myTimer = 0;
		} else {
			if (count < total) {
				timing = false;
				count++;
				pickups [count].SetActive (true);
				pickups [count].GetComponent<Renderer>().enabled = false;
				greens [count].SetActive (true);
			} 
		}
	}
}
