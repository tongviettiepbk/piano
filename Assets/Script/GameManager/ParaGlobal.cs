using UnityEngine;
using System.Collections;

public class ParaGlobal : MonoBehaviour {

	public static ParaGlobal instance;

	public static int bai =-1; 		// luu bai minh chon
	public static bool stateGo = false; 	// luu trang thai nguoi choi da an go
	public static bool stateStart = false; 		// luu game bat dau, nhan cua server.
	public static int numberPlay = 1; // luu id do server tra ve (1,2,3)
	public static bool 	isPlay = true; // den luot choi
	public static int countPlayer=0; // đếm số nốt nhạc đã đánh.
	public static string namePlayer = "ab";
	public static string namePlayer2 = "ab2";

}
