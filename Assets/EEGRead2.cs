using UnityEngine;
using System.Collections;

public class EEGRead2 : MonoBehaviour {

	public GameObject cube;
	float attention;
	float meditation;

	// generate a handle to a ThinkGear connection
	int handleID = ThinkGear.TG_GetNewConnectionId();

	void OnGUI() {
		GUI.Label (new Rect (5, 40, 400, 120), "handleID: " + handleID);
		GUI.Label (new Rect (5, 100, 600, 120), "Attention: " + attention);
		GUI.Label (new Rect (5, 160, 600, 120), "Meditation: " + meditation);
	}

	// Update is called once per frame
	IEnumerator Start() {
		// perform the actual connection
		int connectStatus = ThinkGear.TG_Connect(handleID,
		                                         "/dev/tty.MindSet", 
		                                         ThinkGear.BAUD_9600, 
		                                         ThinkGear.STREAM_PACKETS);	
		if (connectStatus >= 0) {
			// sleep for 1.5 seconds
			yield return new WaitForSeconds (1.5f);
			
			// read all of the data in the buffer
			int packetCount = ThinkGear.TG_ReadPackets (handleID, -1);
			
			// we've received some data, thus we've connected to a valid headset
			if (packetCount > 0) {
				// implement some behavior here
				InvokeRepeating ("UpdateHeadsetData", 0.0f, 1.0f);   
			}
			// no valid headset data received, so close the connection
			else {
				ThinkGear.TG_FreeConnection (handleID);
				Application.LoadLevel("EEGRead");
			}	
		} else {
			// the connection attempt was unsuccessful
			ThinkGear.TG_FreeConnection (handleID);
			Application.LoadLevel("EEGRead");
		}
	}

	void UpdateHeadsetData(){
		int packetCount = ThinkGear.TG_ReadPackets(handleID, -1);
		
		if(packetCount > 0){
			attention = ThinkGear.TG_GetValue(handleID, 
			                                        ThinkGear.DATA_ATTENTION);
			
			meditation = ThinkGear.TG_GetValue(handleID,
			                                         ThinkGear.DATA_MEDITATION);

			cube.transform.position = new Vector3(attention,0,0);
		}


	}
}
