using ExitGames.Client.Photon;
using Photon.Chat;
using Photon.Pun;
using Photon.Pun.Demo.Cockpit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;

public class ChatForPlayer : MonoBehaviour,IChatClientListener
{
    ChatClient chatClient;
    [SerializeField] InputField chatField;
    string currentChat;
    string _privateReceiver="";
    [SerializeField] Text chatDisplay;
    [HideInInspector] public string _playerNameClicked;
    [SerializeField] private GameObject chatPanel;
    public void DebugReturn(DebugLevel level, string message)
    {
        //throw new System.NotImplementedException();
    }

    public void OnChatStateChange(ChatState state)
    {
        //throw new System.NotImplementedException();
    }

    public void OnConnected()
    {
       // throw new System.NotImplementedException();

        chatClient.Subscribe(new string[] { "RegionalChannel" });

    }

    public void OnDisconnected()
    {
        //throw new System.NotImplementedException();
    }

    public void OnGetMessages(string channelName, string[] senders, object[] messages)
    {
        //throw new System.NotImplementedException();
        string msgs = "";
        for(int i = 0; i < senders.Length; i++)
        {
            msgs = string.Format("{0}: {1}", senders[i], messages[i]);
            chatDisplay.text += "\n " + msgs;
            Debug.Log(msgs);
        }
    }

    public void OnPrivateMessage(string sender, object message, string channelName)
    {
        //throw new System.NotImplementedException();
        string msgs = "";
       
            msgs = string.Format("{0}: {1}", sender, message);
            chatDisplay.text += "\n " + msgs;
            Debug.Log(msgs);
        
    }

    public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
    {
       // throw new System.NotImplementedException();
    }

    public void OnSubscribed(string[] channels, bool[] results)
    {
        //throw new System.NotImplementedException();
    }

    public void OnUnsubscribed(string[] channels)
    {
        //throw new System.NotImplementedException();
    }

    public void OnUserSubscribed(string channel, string user)
    {
       // throw new System.NotImplementedException();
    }

    public void OnUserUnsubscribed(string channel, string user)
    {
        //throw new System.NotImplementedException();
    }

    private string userName;

    // Start is called before the first frame update
    void Start()
    {
        chatPanel.SetActive(false);
        chatClient = new ChatClient(this);
        chatClient.Connect(PhotonNetwork.PhotonServerSettings.AppSettings.AppIdChat, PhotonNetwork.AppVersion, new AuthenticationValues(PhotonNetwork.NickName));
    }

    // Update is called once per frame
    void Update()
    {
        chatClient.Service();
        if (chatField.text != "" && Input.GetKey(KeyCode.Return))
        {
            SubmitOnPublicChatClick();
            SubmitOnPrivateChatClick();
        }
    }


    public void onChatClickJoin(string namePlayer)
    {
        _privateReceiver = namePlayer;
        Debug.Log(_privateReceiver+" chat List");
       
    }

  public void SubmitOnPublicChatClick()
    {
        if (_privateReceiver == "")
        {
        chatClient.PublishMessage("RegionalChannel", currentChat);
        chatField.text = "";
        currentChat = "";
        }
    }

    public void SubmitOnPrivateChatClick()
    {
        if (_privateReceiver != "")
        {
            chatClient.SendPrivateMessage(_privateReceiver, currentChat);
            chatField.text = "";
            currentChat = "";
        }
    }

     public void TypeChatOnValueChange(string valueIn)
    {
        currentChat = valueIn;
    }
    public void ClickOnName()
    {
        chatPanel.SetActive(true);
    }
    public void CrossBtn()
    {
        chatPanel.SetActive(false);
    }

}
