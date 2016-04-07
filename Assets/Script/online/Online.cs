using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using SocketIO;
using UnityEngine.UI;
using System.IO;
using System;
using WebSocketSharp;
using WebSocketSharp.Net;

public class Online : MonoBehaviour {

	public static Online	instance;

	public SocketIOComponent _socket;

	public GameObject socketIO;
	public Text id;
	public Text pass;
	public string goScene;

	private bool isSuccess;
	private bool isError;

	void Awake(){
		if (instance)
		{
			DestroyImmediate(gameObject);
			return;
		}
		instance = this;
		DontDestroyOnLoad(gameObject);
	}

	void Start () {
		_socket = socketIO.GetComponent<SocketIOComponent>();
		_socket.On("open", OpenSocket);
		_socket.On("error", ErrorSocket);
		_socket.On("close", CloseSocket);
		_socket.On ("loginCallBack",Login);
	}

	void Update () {
		
	}

	public void StartConnect()
	{
		Dictionary<string, string> data = new Dictionary<string, string>();
		data["id"] = id.text;
		data["pass"] = pass.text;
		_socket.Emit ("login",new JSONObject(data));
	}

	void Login(SocketIOEvent e){
		Debug.Log ("callbackLogin: ket noi thanh cong");
		Dictionary<string,string> aboutLogin = e.data.ToDictionary ();
		if (aboutLogin ["login"] == "1") {
			Application.LoadLevel (goScene);
		} else {
			Debug.Log("Hay kiem tra lai");
		}
	}
	

	public void OpenSocket(SocketIOEvent e)
	{
		Debug.Log("Kết lối thành công ..");
		isSuccess = true;
	}
	
	public void ErrorSocket(SocketIOEvent e)
	{ 
		Debug.Log("Kết lối thất bại ..");
		isError = true;
	}
	
	public void CloseSocket(SocketIOEvent e)
	{
		Debug.Log("Đóng kết lối ..");
		isError = true;
	}
}
