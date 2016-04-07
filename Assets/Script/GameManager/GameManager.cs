using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public static GameManager instance;
	// Danh sách bài hát
	public int TestNumberPlayer = 1;

	private List<string> bai_1;
	private List<string> bai_2;
	private List<string> bai_3;
	private List<string> bai_4;
	private List<string> arraySong;

	// Vi tri not nhac dau tien duoc choi.
	private int indexStart=0; 

	// Vi tri not nhac cuoi cung duoc choi.
	private int indexEnd = 0;

	// Tên bài hát người dùng chọn.
	private string nameSong;

	// Đếm số nốt người dùng đánh sai.
	public int demNotSai = 0;
	public Text txtCountFail;

	// Danh sách lưu các phím nhạc được clone theo list bài hát.
	public List<GameObject> listCloneObject;

	//Danh sách lưu các text tương ứng với listCloneObject
	//public List<Text> listCloneText;
	GameObject objDo,objRe,objMi,objFa,objSon,objLa,objSi;
	GameObject txtDo,txtRe,txtMi,txtFa,txtSon,txtLa,txtSi,txtDen1,txtDen2,txtDen3,txtDen4,txtDen5;
	public GameObject objMoc,txtMoc;
	GameObject objParent,txtParent;
	ControllerPlayer player;
	Text txtTenBaiHat;
	//Nốt đầu tiên và nốt cuối cùng được hiển thị.
	public int firstNode=0, lastNode=6, lenghtSong=0;
	public Vector3 posObjMoc,posTxtMoc;

	public void Setplayer(){
		ParaGlobal.numberPlay = TestNumberPlayer;
	}

	// Hien thi luot choi
	Text txtStatePlay;
	void Start () {
		instance = this;
		listCloneObject = new List<GameObject> ();
		arraySong = new List<string> ();
		// Khởi tạo các đối tượng.
		createObj ();

		// Nếu người dùng chọn bài hát bắt buộc.
		if (ParaGlobal.bai != 0) {
			// Tạo danh sách bài hát.
			addListSong ();
			arraySong = getSong ();

			// Hien thi ten bai hat.
			txtTenBaiHat.text = nameSong;

			// Chia ban nhac cho tung client.
			chiaDoan ();

			// Hiển thị 7 nốt nhạc đầu tiên của bài hát.
			createSong (ParaGlobal.bai, firstNode, lastNode);
		} else {
			chiaDoan ();
		}

	

	}
	void Update(){
		if (!ParaGlobal.isPlay) {
			txtStatePlay.text = "Mat luot";
		} else {
			txtStatePlay.text = "Dang choi";
		}
	}
	// Tao cac not nhac.
    GameObject InitObject(GameObject objOriginal,GameObject moc,GameObject parent,float distanceX,float distanceY){
		GameObject cloneObj, cloneTxt;
		cloneObj= Instantiate(objOriginal) as GameObject;
		cloneObj.transform.parent = parent.transform;
		cloneObj.transform.localScale = new Vector3 (1,1,1);
		cloneObj.transform.localPosition = new Vector3(moc.transform.localPosition.x+distanceX,moc.transform.localPosition.y+distanceY,moc.transform.localPosition.z);
		moc.transform.localPosition = new Vector3 (cloneObj.transform.localPosition.x, cloneObj.transform.localPosition.y - distanceY, cloneObj.transform.localPosition.z);
		listCloneObject.Add (cloneObj);
		cloneObj.SetActive (true);
		return cloneObj;
	}

	// Hien thi not nhac tu not thu first vi tri last. Tổng là 7 nốt nhạc trên một khuông.
	public void createSong(int pos,int first,int last){
		objMoc.transform.localPosition=posObjMoc;
		txtMoc.transform.localPosition=posTxtMoc;

		// Nếu người dùng chọn bài hát 1.
		switch (pos) {
		case 1:
			if (last <= lenghtSong) {
				for (int i = first; i < last; i++) {
					showNotNhac (bai_1 [i]);
					changeColorText (i);
				}
			}
			break;
		case 2:
			if (last <= lenghtSong) {
				for (int i = first; i < last; i++) {
					showNotNhac (bai_2 [i]);
					changeColorText (i);
				}
			}
			break;
		case 3:
			if (last <= lenghtSong) {
				for (int i = first; i < last; i++) {
					showNotNhac (bai_3 [i]);
					changeColorText (i);
				}
			}
			break;
		case 4:
			if (last <= lenghtSong) {
				for (int i = first; i < last; i++) {
					showNotNhac (bai_4 [i]);
					changeColorText (i);
				}
			}
			break;
		}
	}
    
	void changeColorText( int i){
		if(i >= indexStart && i <=indexEnd){
			Text text = listCloneObject[2*i+1].GetComponent<Text>() as Text;
			text.color = new Color (0f, 1f, 0f, 1f);

		}
	}

	// Hủy đối tượng clone khi người dùng đã chơi hết 7 nốt nhạc. Để hiện 7 nốt nhạc tiếp theo.
	public void destroyObject(){
		for (int i = 0; i < listCloneObject.Count; i++) {
			Destroy (listCloneObject [i]);

		}
		firstNode += 7;
		int a = lenghtSong - lastNode;
		if (a >= 7) {
			lastNode += 7;
		} else {
			lastNode +=a;
		}

	}

	// Bat su kien khi nhan duoc ten not nhac.
	public void nhanNotNhac(string tennotnhac){

		// Người chơi chọn bài tự do.
		if(ParaGlobal.bai==0){
			int x = ParaGlobal.countPlayer - 1;
			if (ParaGlobal.countPlayer>0 &&ParaGlobal.countPlayer % 7==0) {
				destroyObject ();
				createSong (ParaGlobal.bai, firstNode,lastNode);
			}
			showNotNhac (tennotnhac);
			ParaGlobal.countPlayer++;
			if(ParaGlobal.countPlayer>indexEnd){
				ParaGlobal.isPlay = false;
			}
		}else{

			if (ParaGlobal.countPlayer < arraySong.Count) {
				Image notNhac=listCloneObject [2*ParaGlobal.countPlayer].GetComponent<Image> ();
				notNhac.color = new Color (1f, 1f, 1f, 0.3f);

				// Nếu người dùng đánh sai
				if (arraySong [ParaGlobal.countPlayer] != tennotnhac) {
					demNotSai++;
					txtCountFail.text = demNotSai.ToString ();
					Text txtNotNhac =listCloneObject [2 * ParaGlobal.countPlayer + 1].GetComponent<Text> ();
					txtNotNhac.color = new Color (1f,0f,0f,1f);
				}
				ParaGlobal.countPlayer++;

				// Kiem tra neu choi duoc 7 not thi xoa de hien thi cac not tiep theo.
				if (ParaGlobal.countPlayer % 7==0) {
					destroyObject ();
					createSong (ParaGlobal.bai,firstNode,lastNode);
				}

				// Neu nguoi cho danh het phan cua minh.
				if (ParaGlobal.countPlayer>indexEnd) {
					ParaGlobal.isPlay = false;
				}
					
			}
		}
		playSound (tennotnhac);
	}

	void playSound(string phim){
		AudioController.instance.PlayEfxClip (phim);
	}
	public List<string> getSong()
	{
		switch (ParaGlobal.bai) {
		case 1:
			return bai_1;
			break;
		case 2: 
			return bai_2;
			break;
		case 3:
			return bai_3;
			break;
		case 4:
			return bai_4;
			break;
		}
		return null;
	}
		
	public void chiaDoan(){
		int distance = 0;
		int thutu = 0;
		switch (ParaGlobal.bai) {
		case 0:
			distance = 14;
			thutu = 1;
			break;
		case 1:
			distance = (bai_1.Count) / 3;
			thutu = 2;
			break;
		case 2:
			distance = (bai_2.Count) / 3;
			thutu = 3;
			break;
		case 3:
			distance = (bai_3.Count) / 3;
			thutu = 4;
			break;
		case 4:
			distance = (bai_4.Count) / 3;
			thutu = 5;
			break;
		}
	//	thutu = (ParaGlobal.numberPlay + thutu)%3+1;
		indexStart = (ParaGlobal.numberPlay - 1) * distance;
		indexEnd = (ParaGlobal.numberPlay) * distance-1;
	}
	public int getIndexStart(){
		return indexStart;
	}
	public int getIndexEnd(){
		return indexEnd;
	}

	// clone các nốt nhạc và text.
	public void showNotNhac(string not){
		switch (not) {
		case "PhimDo":
			InitObject (objDo, objMoc, objParent, 100f, -40f);
			InitObject (txtDo, txtMoc, txtParent, 100f, 0f);
			break;
		case "PhimRe":
			InitObject (objRe, objMoc, objParent,100f,-30f);
			InitObject (txtRe, txtMoc, txtParent,100f,0f);
			break;
		case "PhimMi":
			InitObject (objMi, objMoc, objParent,100f,-20f);
			InitObject (txtMi, txtMoc, txtParent,100f,0f);
			break;
		case "PhimFa":
			InitObject (objFa, objMoc, objParent,100f,-10f);
			InitObject (txtFa, txtMoc, txtParent,100f,0f);
			break;
		case "PhimSon":
			InitObject (objSon,objMoc,objParent,100f,0f);
			InitObject (txtSon, txtMoc, txtParent,100f,0f);
			break;
		case "PhimLa":
			InitObject (objLa, objMoc, objParent,100f,15f);
			InitObject (txtLa, txtMoc, txtParent,100f,0f);
			break;
		case "PhimSi":
			InitObject (objSi, objMoc, objParent,100f,-30f);
			InitObject (txtSi, txtMoc, txtParent,100f,0f);
			break;
		case "PhimDen_1":
			InitObject (objLa, objMoc, objParent,100f,15f);
			InitObject (txtDen1, txtMoc, txtParent,100f,0f);
			break;
		case "PhimDen_2":
			InitObject (objLa, objMoc, objParent,100f,15f);
			InitObject (txtDen2, txtMoc, txtParent,100f,0f);
			break;
		case "PhimDen_3":
			InitObject (objLa, objMoc, objParent,100f,15f);
			InitObject (txtDen3, txtMoc, txtParent,100f,0f);
			break;
		case "PhimDen_4":
			InitObject (objLa, objMoc, objParent,100f,15f);
			InitObject (txtDen4, txtMoc, txtParent,100f,0f);
			break;
		case "PhimDen_5":
			InitObject (objLa, objMoc, objParent,100f,15f);
			InitObject (txtDen5, txtMoc, txtParent,100f,0f);
			break;

		}
	}

	// Khoi tao cac đối tượng.
	void createObj(){
		objDo = GameObject.Find("Do");
		objRe = GameObject.Find ("Re");
		objMi = GameObject.Find ("Mi");
		objFa = GameObject.Find ("Fa");
		objSon = GameObject.Find ("Son");
		objLa = GameObject.Find ("La");
		objSi = GameObject.Find ("Si");
		objMoc = GameObject.Find ("Moc");
		objParent = GameObject.Find ("KhungNhac");

		txtDo = GameObject.Find ("txtDo");
		txtRe = GameObject.Find ("txtRe");
		txtMi = GameObject.Find ("txtMi");
		txtFa = GameObject.Find ("txtFa");
		txtSon = GameObject.Find ("txtSon");
		txtLa = GameObject.Find ("txtLa");
		txtSi = GameObject.Find ("txtSi");
		txtDen1 = GameObject.Find ("txtDen1");
		txtDen2 = GameObject.Find ("txtDen2");
		txtDen3 = GameObject.Find ("txtDen3");
		txtDen4 = GameObject.Find ("txtDen4");
		txtDen5 = GameObject.Find ("txtDen5");

		txtMoc = GameObject.Find ("txtMoc");
		txtParent = GameObject.Find ("TenNot");
		txtTenBaiHat = GameObject.Find ("TenBaiHat").GetComponent<Text>();
		posObjMoc = objMoc.transform.localPosition;
		posTxtMoc = txtMoc.transform.localPosition;

		txtStatePlay = GameObject.Find ("statePlay").GetComponent<Text>() as Text;

	}


	void addListSong(){
		switch (ParaGlobal.bai) {
		case 1: 
			bai_1 = new List<string> ();
			nameSong = "Bai 1";
			bai_1.Add ("PhimDo");
			bai_1.Add ("PhimDo");
			bai_1.Add ("PhimSon");
			bai_1.Add ("PhimSon");
			bai_1.Add ("PhimLa");
			bai_1.Add ("PhimLa");
			bai_1.Add ("PhimSon");
			bai_1.Add ("PhimFa");
			bai_1.Add ("PhimFa");
			bai_1.Add ("PhimMi");
			bai_1.Add ("PhimMi");
			bai_1.Add ("PhimRe");
			bai_1.Add ("PhimRe");
			bai_1.Add ("PhimDo");
			lenghtSong = bai_1.Count;
			break;
		case 2:
			bai_2 = new List<string> ();
			nameSong = "Bai 2";
			bai_2.Add ("PhimSon");
			bai_2.Add ("PhimLa");
			bai_2.Add ("PhimSon");
			bai_2.Add ("PhimMi");
			bai_2.Add ("PhimSon");
			bai_2.Add ("PhimDo");
			bai_2.Add ("PhimMi");
			bai_2.Add ("PhimLa");
			bai_2.Add ("PhimSon");
			bai_2.Add ("PhimSon");

			bai_2.Add ("PhimSon");
			bai_2.Add ("PhimLa");
			bai_2.Add ("PhimSon");
			bai_2.Add ("PhimSon");
			bai_2.Add ("PhimMi");
			bai_2.Add ("PhimLa");
			bai_2.Add ("PhimLa");
			bai_2.Add ("PhimLa");
			bai_2.Add ("PhimSon");
			bai_2.Add ("PhimMi");
			bai_2.Add ("PhimRe");
			lenghtSong = bai_2.Count;
			break;
		case 3:
			bai_3 = new List<string> ();
			nameSong = "Bai 3";
			bai_3.Add ("PhimSi");
			bai_3.Add ("PhimSi");
			bai_3.Add ("PhimSon");
			bai_3.Add ("PhimSi");
			bai_3.Add ("PhimSi");
			bai_3.Add ("PhimLa");
			bai_3.Add ("PhimLa");
			bai_3.Add ("PhimLa");
			bai_3.Add ("PhimSon");
			bai_3.Add ("PhimFa");
			bai_3.Add ("PhimSon");
			bai_3.Add ("PhimLa");
			bai_3.Add ("PhimSi");

			bai_3.Add ("PhimSi");
			bai_3.Add ("PhimSi");
			bai_3.Add ("PhimSon");
			bai_3.Add ("PhimSi");
			bai_3.Add ("PhimSi");
			bai_3.Add ("PhimLa");
			bai_3.Add ("PhimLa");
			bai_3.Add ("PhimLa");
			bai_3.Add ("PhimLa");
			bai_3.Add ("PhimSon");
			bai_3.Add ("PhimFa");
			bai_3.Add ("PhimRe");
			bai_3.Add ("PhimRe");
			lenghtSong = bai_3.Count;
			break;
		case 4:
			bai_4 = new  List<string> ();
			nameSong = "Bai 4";
			bai_4.Add ("PhimFa");
			bai_4.Add ("PhimFa");
			bai_4.Add ("PhimFa");
			bai_4.Add ("PhimSon");
			bai_4.Add ("PhimLa");
			bai_4.Add ("PhimLa");

			bai_4.Add ("PhimLa");
			bai_4.Add ("PhimFa");
			bai_4.Add ("PhimSon");
			bai_4.Add ("PhimLa");
			bai_4.Add ("PhimSon");

			bai_4.Add ("PhimRe");
			bai_4.Add ("PhimDo");
			bai_4.Add ("PhimRe");
			bai_4.Add ("PhimFa");
			bai_4.Add ("PhimSon");
			bai_4.Add ("PhimSon");

			bai_4.Add ("PhimSon");
			bai_4.Add ("PhimRe");
			bai_4.Add ("PhimFa");
			bai_4.Add ("PhimRe");
			bai_4.Add ("PhimDo");

			bai_4.Add ("PhimFa");
			bai_4.Add ("PhimFa");
			bai_4.Add ("PhimFa");
			bai_4.Add ("PhimSon");
			bai_4.Add ("PhimLa");
			bai_4.Add ("PhimLa");

			bai_4.Add ("PhimLa");
			bai_4.Add ("PhimFa");
			bai_4.Add ("PhimSon");
			bai_4.Add ("PhimLa");
			bai_4.Add ("PhimSon");

			bai_4.Add ("PhimRe");
			bai_4.Add ("PhimDo");
			bai_4.Add ("PhimRe");
			bai_4.Add ("PhimFa");
			bai_4.Add ("PhimSon");
			bai_4.Add ("PhimSon");
			bai_4.Add ("PhimFa");
			bai_4.Add ("PhimFa");
			bai_4.Add ("PhimFa");
			bai_4.Add ("PhimFa");
			bai_4.Add ("PhimFa");
			bai_4.Add ("PhimFa");

			bai_4.Add ("PhimFa");
			bai_4.Add ("PhimRe");
			bai_4.Add ("PhimFa");
			bai_4.Add ("PhimSon");
			bai_4.Add ("PhimFa");
			lenghtSong = bai_4.Count;
			break;
		}


	}
}
