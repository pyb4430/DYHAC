using UnityEngine;
using System.Collections;

public class LOSray : MonoBehaviour {

	public GameObject spotlight;
	public GameObject stim1;
	public GameObject stim2;
	public GameObject stim3;
	public GameObject stim4;
	public GameObject stim5;
	public GameObject stim6;
	public GameObject stim7;
	public GameObject stim8;
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
	GameObject[] stims;

	// Use this for initialization
	void Start () {
		//Screen.orientation = ScreenOrientation.LandscapeRight;
		spotlight.SetActive (false);
		timing = false;
		timeIndex = 0;
		myTime = 0;
		rxnTimes = new float[numTrials];
		stims = new GameObject[stimnum];
		stims [0] = stim1;
		stims [1] = stim2;
		stims [2] = stim3;		
		stims [3] = stim4;
		stims [4] = stim5;
		stims [5] = stim6;
		stims [6] = stim7;
		stims [7] = stim8;
		for (int i=0; i<stimnum; i++) {
			stims[i].SetActive (false);
		}
	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit hit;
		Ray LOSray = new Ray (transform.position, transform.forward);

		Debug.DrawRay (transform.position, transform.forward*20);

		if (timeIndex < numTrials) {
			if (Physics.Raycast (LOSray, out hit, 20)) {
				if (hit.collider.tag == "Stimulus") {
					KillStimulus ();
				}

				if (hit.collider.tag == "Pickup") {
					spotlight.SetActive (true);
					if (!timing) {
						waiting = true;
					} else {
						waiting = false;
					}
				} else {
					spotlight.SetActive (false);
					waiting = false;
				}
			} else {
				hit.point = Vector3.zero;
				spotlight.SetActive (false);
				waiting = false;
			}

			if (timing || waiting) {
				myTime += Time.deltaTime;
			}

			if (waiting && myTime > 2 + Random.Range (0, 3)) {
				rand = Random.Range (0, stimnum);
				waiting = false;
				SpawnStimulus ();
			}
		} else {
			averageTime();
			FinText.GetComponent<TextMesh>().text = average.ToString () + " seconds";
			myTime += Time.deltaTime;
			if(myTime>5) {
				PlayerDataStore.data.averageRxnTime = average;
				Application.LoadLevel("BalanceTestEdInst");
			}
		}
	}

	void SpawnStimulus() {
		stims[rand].SetActive (true);
		myTime = 0;
		timing = true;
	}

	void KillStimulus() {
		stims[rand].SetActive (false);
		timing = false;
		rxnTimes[timeIndex] = myTime;
		timeIndex++;
		myTime = 0;
	}

	void averageTime() {
		float sum = 0.0f;
		for (int i=0; i<stimnum; i++) {
			sum += rxnTimes[i];
		}
		average = sum / (float)stimnum;
	}
}
