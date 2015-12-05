using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuActions : MonoBehaviour {

	public InputField textBox;

	public void StartTheTest () {
		string name = textBox.text;
		PlayerDataStore.data.Load(name); 
		Application.LoadLevel("Memory");
	}
}
