using UnityEngine;
using System.Collections;

public class BalanceTestEdInst : MonoBehaviour {
	
	public GameObject pickup1;
	public GameObject FinText;
	private int count;
	private int total = 10;
	public float myTimer;
	
	void Start () {
		FinText.GetComponent<TextMesh> ().text = "Move your head to move the platform\nand collect the cubes";
		count = 0;
		myTimer = 0.0f;		
	}
	
	// Update is called once per frame
	void Update () {
		
		if (transform.position.y < -5)
		{
			Application.LoadLevel ("BalanceTestEdInst");	
		}

		if (myTimer > 8)
		{
			Application.LoadLevel ("BalanceTestEd");	
		}

		myTimer += Time.deltaTime;
	}

}
