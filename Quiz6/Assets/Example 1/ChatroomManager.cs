using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

[RequireComponent(typeof(ChatroomMessenger))]
public class ChatroomManager : MonoBehaviour {
	public int listen_port = 7777;
	[SerializeField]
	GameObject StartMenu;
	[SerializeField]
	GameObject Chatroom;
	[SerializeField]
	InputField IPAddress;

	public static NetworkClient client;

	//啟動Server [/UI/StartMenu/ServerBtn]
	public void StartServer(){
		if(NetworkServer.Listen(listen_port)){
			GetComponent<ChatroomMessenger>().RegisterMsgHandler();
			
			StartMenu.SetActive(false);
		}
	}

	//啟動Server [/UI/StartMenu/HostBtn]
	public void StartHost(){
		if(NetworkServer.Listen(listen_port)){
			client = ClientScene.ConnectLocalServer();

			GetComponent<ChatroomMessenger>().RegisterMsgHandler();

			StartMenu.SetActive(false);
			Chatroom.SetActive(true);
		}
	}

	//啟動Client [/UI/StartMenu/ClientBtn]
	public void StartClient(){
		client = new NetworkClient();
		client.RegisterHandler(MsgType.Connect, OnConnected);
		client.RegisterHandler(MsgType.Disconnect, OnDisconnected);
		client.RegisterHandler(MsgType.Error, OnError);

		client.Connect(IPAddress.text, listen_port);
	}

	//連結Server成功
	public void OnConnected(NetworkMessage msg) {
		Debug.Log("Connected to server");
		GetComponent<ChatroomMessenger>().RegisterMsgHandler();
		
		StartMenu.SetActive(false);
		Chatroom.SetActive(true);
	}

	//從Server連線斷開
	public void OnDisconnected(NetworkMessage msg) {
		Debug.Log("Disconnected from server");

		StartMenu.SetActive(true);
		Chatroom.SetActive(false);
	}

	//連線錯誤發生
	public void OnError(NetworkMessage msg) {
		ErrorMessage errorMsg = msg.ReadMessage<ErrorMessage>();
		Debug.Log("Error connecting with code " + errorMsg.errorCode);
	}
}
