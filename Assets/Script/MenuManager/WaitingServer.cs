using UnityEngine;
using System.Collections;

public class WaitingServer : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		// Server cho phep bat dau choi
		if (ParaGlobal.stateStart) {
			Application.LoadLevel ("GamePlay");
		}
	}
}
