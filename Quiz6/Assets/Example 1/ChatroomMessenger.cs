using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;

public class ChatMessage : MessageBase{
	public string name;
	public string message;
}

public class ChatroomMessenger : MonoBehaviour {
	[SerializeField]
	InputField user_name;
	[SerializeField]
	InputField message_window;
	[SerializeField]
	InputField user_message;

	public const short ChatMsgID = MsgType.Highest + 1;

	//註冊傳送的訊息
	public void RegisterMsgHandler(){
		if(NetworkServer.active) NetworkServer.RegisterHandler(ChatMsgID, ServerRecvMessage);
		if(NetworkClient.active) ChatroomManager.client.RegisterHandler(ChatMsgID, RecvChatMessage);
	}

	// Client傳送訊息 [UI/Chatroom/SendBtn]
	public void SendChatMessage () {
		ChatMessage chatmsg = new ChatMessage();
		chatmsg.name = user_name.text;
		chatmsg.message = user_message.text;
		
		ChatroomManager.client.Send(ChatMsgID, chatmsg);
	}

	// Server接收訊息並傳給連線中的每個人
	void ServerRecvMessage(NetworkMessage msg){
		NetworkServer.SendToAll(ChatMsgID, msg.ReadMessage<ChatMessage>());
	}

	//Client收到Server的訊息並顯示在Chat Window上面
	void RecvChatMessage (NetworkMessage msg) {
		ChatMessage chatmsg = msg.ReadMessage<ChatMessage>();

		message_window.text += chatmsg.name + " : " + chatmsg.message + "\n";
	}

}
