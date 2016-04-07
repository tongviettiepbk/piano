using UnityEngine;
using System.Collections;

public class ServerClick : MonoBehaviour {
	public void onClick(){
		
	ParaGlobal.numberPlay = 2;
		ParaGlobal.isPlay = true;
		GameManager.instance.chiaDoan ();
	}

}
