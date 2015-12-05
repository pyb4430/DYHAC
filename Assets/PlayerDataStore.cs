using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class PlayerDataStore : MonoBehaviour {
	public static PlayerDataStore data; 
	public float memNumCorrect;
	public float averageRxnTime;
	public float balanceTime;
	public string name;

	void Awake () {
		if (data == null) {
			DontDestroyOnLoad (gameObject);
			data = this;
		} else if (data != this) {
			Destroy (gameObject);
		}
	}

	//void OnEnable() {
	//	Load ();
	//}

	void OnGUI() {
		GUI.Label (new Rect (5, 10, 100, 30), "Memory: " + memNumCorrect);
		GUI.Label (new Rect (5, 25, 150, 30), "Balance: " + balanceTime);
		GUI.Label (new Rect (5, 40, 150, 30), "Reaction time: " + averageRxnTime);
	}

	public void Save(string name) {
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + "/" + name + ".dat");

		PlayerData dataS = new PlayerData ();
		dataS.memNumCorrect = memNumCorrect;
		dataS.averageRxnTime = averageRxnTime;
		dataS.balanceTime = balanceTime;
		dataS.name = name;

		bf.Serialize (file, dataS);
		file.Close ();
	}

	public void Load(string name) {
		if (File.Exists (Application.persistentDataPath + "/" + name + ".dat")) {
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/" + name + ".dat", FileMode.Open);
			PlayerData dataS = (PlayerData)bf.Deserialize(file);
			file.Close();

			memNumCorrect = dataS.memNumCorrect;
			averageRxnTime = dataS.averageRxnTime;
			balanceTime = dataS.balanceTime;
			name = dataS.name;
		}
	}
}

[Serializable]
class PlayerData {
	public string name;
	public float memNumCorrect;
	public float averageRxnTime;
	public float balanceTime;
}
