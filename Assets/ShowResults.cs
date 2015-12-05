using UnityEngine;
using System.Collections;

public class ShowResults : MonoBehaviour {

	public GameObject FinText;
	private float myTime;

	// Use this for initialization
	void Start () {
		FinText.GetComponent<TextMesh>().text = "Reaction Time: " + PlayerDataStore.data.averageRxnTime + "\nMemory Score: " + PlayerDataStore.data.memNumCorrect + "\nBalance Score: " + PlayerDataStore.data.balanceTime;
		myTime = 0;
	}
	
	// Update is called once per frame
	void Update () {
		myTime += Time.deltaTime;

		if (myTime > 8) {
			string name = PlayerDataStore.data.name;
			PlayerDataStore.data.Save(name);
			Application.LoadLevel ("MainMenu");
		}
	}
}
