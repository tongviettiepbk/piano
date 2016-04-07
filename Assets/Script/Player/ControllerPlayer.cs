using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class ControllerPlayer : MonoBehaviour {
	private SpriteRenderer phimNhac;
	void Start () {
		phimNhac = gameObject.GetComponent<SpriteRenderer> ();
	}

	void OnMouseDown()
	{  
		
		if (ParaGlobal.isPlay == false) {
			Debug.Log ("Ban chua toi luot");
		} else {
			phimNhac.color = new Color (1f, 1f, 1f, 0.3f);
			GameManager.instance.nhanNotNhac (gameObject.name);

		}
	}

	void OnMouseUp(){
		phimNhac.color= new Color (1f, 1f, 1f, 1f);
	}

		
}
