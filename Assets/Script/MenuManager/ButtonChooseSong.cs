using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonChooseSong : MonoBehaviour {
	Image image;
	Text text0,text1, text2, text3, text4;
	void Start () {
		image = gameObject.GetComponent<Image> ();
		text0 = GameObject.Find ("Free").GetComponentInChildren<Text> ();
		text1 = GameObject.Find ("Bai1").GetComponentInChildren<Text> ();
		text2 = GameObject.Find ("Bai2").GetComponentInChildren<Text> ();
		text3 = GameObject.Find ("Bai3").GetComponentInChildren<Text> ();
		text4 = GameObject.Find ("Bai4").GetComponentInChildren<Text> ();
	}
	
	// Update is called once per frame
	void Update () {

	}
	public void onClick(){
		
		switch (gameObject.name) {
		case "Bai1":
			ParaGlobal.bai = 1;
			text1.color = new Color (0f, 1f, 0f, 1f);
			text0.color = new Color (1f, 0f, 0f, 1f);
			text2.color = new Color (1f, 0f, 0f, 1f);
			text3.color = new Color (1f, 0f, 0f, 1f);
			text4.color = new Color (1f, 0f, 0f, 1f);
			break;
		case "Bai2":
			ParaGlobal.bai = 2;
			text2.color = new Color (0f, 1f, 0f, 1f);
			text1.color = new Color (1f, 0f, 0f, 1f);
			text0.color = new Color (1f, 0f, 0f, 1f);
			text3.color = new Color (1f, 0f, 0f, 1f);
			text4.color = new Color (1f, 0f, 0f, 1f);
			break;
		case "Bai3":
			ParaGlobal.bai = 3;
			text3.color = new Color (0f, 1f, 0f, 1f);
			text1.color = new Color (1f, 0f, 0f, 1f);
			text2.color = new Color (1f, 0f, 0f, 1f);
			text0.color = new Color (1f, 0f, 0f, 1f);
			text4.color = new Color (1f, 0f, 0f, 1f);
			break;
		case "Bai4":
			ParaGlobal.bai = 4;
			text4.color = new Color (0f, 1f, 0f, 1f);
			text1.color = new Color (1f, 0f, 0f, 1f);
			text2.color = new Color (1f, 0f, 0f, 1f);
			text3.color = new Color (1f, 0f, 0f, 1f);
			text0.color = new Color (1f, 0f, 0f, 1f);
			break;
		case "Free":
			ParaGlobal.bai = 0;
			text0.color = new Color (0f, 1f, 0f, 1f);
			text1.color = new Color (1f, 0f, 0f, 1f);
			text2.color = new Color (1f, 0f, 0f, 1f);
			text3.color = new Color (1f, 0f, 0f, 1f);
			text4.color = new Color (1f, 0f, 0f, 1f);
			break;
		}

	}
	public void onGoClick(){
		if (ParaGlobal.bai != -1) {
			GameObject.Find ("PanelMenu").SetActive (false);
			ParaGlobal.stateGo = true;
		}
	}
	public void onStartClick(){
		ParaGlobal.stateStart = true;
	}
	public void OnMouseDown(){
		image.color = new Color (1f, 1f, 1f, 0.3f);
	}

	public void OnMouseUp(){
		image.color = new Color (1f, 1f, 1f, 1f);
	}
}
