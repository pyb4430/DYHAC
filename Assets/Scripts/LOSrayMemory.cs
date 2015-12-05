using UnityEngine;
using System.Collections;

public class LOSrayMemory : MonoBehaviour {
	
	public GameObject spotlight;
	public GameObject stim1;
	public GameObject stim2;
	public GameObject stim3;
	public GameObject stim4;
	public GameObject Text;
	private int stimnum = 4;
	private int numTrials = 5;
	private bool returnpick;
	private bool waiting;
	private float average;
	float myTime;
	int timeIndex;
	public int dcount;
	public int corrCount;
	int numDir;
	float[] rxnTimes;
	private int rand;
	GameObject[] stims;
	string[] directions;
	public string[] setDirections;
	public int[] numsCorrect;
	public int numCorrect;
	private bool instructing;
	private bool initializing;
	private bool setList;
	private string dirList;
	float colorCount;
	public GameObject pickup;
	
	// Use this for initialization
	void Start () {
		//Screen.orientation = ScreenOrientation.LandscapeRight;
		spotlight.SetActive (false);
		pickup.GetComponent<Renderer> ().material.color = Color.white;
		numDir = 5;
		returnpick = false;
		timeIndex = 0;
		myTime = 0;
		rxnTimes = new float[numTrials];
		stims = new GameObject[stimnum];
		stims [0] = stim1;
		stims [1] = stim2;
		stims [2] = stim3;		
		stims [3] = stim4;
		for (int i=0; i<stimnum; i++) {
			stims[i].SetActive (true);
		}
		directions = new string[4];
		directions [0] = "Left";
		directions [1] = "Up";
		directions [2] = "Right";
		directions [3] = "Down";
		setDirections = new string[numDir];
		numsCorrect = new int[numTrials];
		initializing = true;
		instructing = false;
		setList = true;
		numCorrect = 0;
		corrCount = 0;
		dcount = 0;
		dirList = "Look ";
		stim1.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit hit;
		Ray LOSray = new Ray (transform.position, transform.forward);
		
		Debug.DrawRay (transform.position, transform.forward * 20);

		if (initializing) {
			//instruct the user
			Text.GetComponent<TextMesh>().text = "Look directly at the rotating cube\nso that it glows. In a moment\nyou will see a list of directions\n(up, down, left, right). Look in\neach direction in order, returning\nyour gaze to the rotating\ncube between each direction.\nStare at the cube to continue";
			if(myTime>5) {
				initializing = false;
				instructing = true;
				myTime = 0;
				stim1.SetActive (true);
			}
		} else if (instructing) {
			//display text
			Text.GetComponent<TextMesh>().text = "Look ";
			if (setList) {
				SetDirections();
				for (int i=0; i<numDir; i++) {
					dirList = dirList + setDirections[i] + " ";
				}
				setList = false;
			}
			Text.GetComponent<TextMesh>().text = dirList; 
			myTime += Time.deltaTime;
			if(myTime>3) {
				instructing = false;
				myTime = 0;
				Text.GetComponent<TextMesh>().text = "";
			}
		} 

		if (corrCount < numTrials) {
			if (Physics.Raycast (LOSray, out hit, 80)) {
				GameObject currentDir = GameObject.FindWithTag (hit.collider.tag);
				if(waiting && !(initializing || instructing)) {
					if (hit.collider.tag == setDirections[dcount]) {
						numCorrect++;
						currentDir.GetComponent<Renderer>().material.color = Color.green;
						dcount++;
						waiting = false;
						returnpick = true;
					} else if (hit.collider.tag == "Pickup") {
						waiting = true;

					} else if (hit.collider.tag != "Pickup" && hit.collider.tag != setDirections[dcount] && hit.collider.tag != "Untagged") {
						dcount++;
						currentDir.GetComponent<Renderer>().material.color = Color.red;
						waiting = false;
						returnpick = true;
					} 

					if (dcount>=numDir) {
						waiting = false;
						dcount = 0;
						instructing = true;
						setList = true;
						numsCorrect[corrCount] = numCorrect;
						numCorrect = 0;
						corrCount++;
						dirList = "Look ";
					}
				}

				if (hit.collider.tag == "Untagged") {
					for (int i=0; i<stimnum; i++) {
						stims[i].GetComponent<Renderer>().material.color = Color.white;
					}
				}

				if (hit.collider.tag == "Pickup") {
					spotlight.SetActive (true);
					pickup.GetComponent<Renderer>().material.color = Color.green;
					if(!(initializing || instructing)) {
						if (!returnpick) {
							waiting = true;
						} else {
							waiting = true;
							returnpick = false;
						}
					}
					if(initializing) {
						myTime += Time.deltaTime;
					}
				} else {
					spotlight.SetActive (false);
					pickup.GetComponent<Renderer>().material.color = Color.white;
					if(initializing) {
						myTime = 0;
					}
				}
			} else {
				hit.point = Vector3.zero;
				spotlight.SetActive (false);
				pickup.GetComponent<Renderer>().material.color = Color.white;
			}
			
			//if (returnpick || waiting) {
			//	myTime += Time.deltaTime;
			//}

		} else {
			averageCorr();
			Text.GetComponent<TextMesh>().text = "Average number correct: " + average.ToString ();
			myTime += Time.deltaTime;
			if(myTime>5) {
				PlayerDataStore.data.memNumCorrect = average;
				Application.LoadLevel("RxnTimeInst");
			}
		}
	
	}
	
	void SetDirections() {
		for(int i=0; i<numDir; i++) {
			int rand = Random.Range (0,4);
			setDirections [i] = directions[rand];
			if(i>0) {
				while(setDirections[i] == setDirections[i-1]) {
					rand = Random.Range (0,4);
					setDirections [i] = directions[rand];
				}
			}
		}
		dcount = 0;
	}
	
	void CheckStimulus() {
		
	}
	
	void averageCorr() {
		float sum = 0.0f;
		for (int i=0; i<numTrials; i++) {
			sum += numsCorrect[i];
		}
		average = sum / (float)numTrials;
	}
}
