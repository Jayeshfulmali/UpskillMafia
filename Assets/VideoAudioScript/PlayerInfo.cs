using Photon.Pun;
using TMPro;
using UnityEditor;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    private const string CHANNEL_NAME = "ChannelName";
    private const string TOKEN = "Token";

    private const string PLAYER_NAME = "PlayerName";

    private PhotonView photonView;
    private SpriteRenderer spriteRenderer;
    //private TextMeshPro playerNameText;

    private ExitGames.Client.Photon.Hashtable playerDetails = new ExitGames.Client.Photon.Hashtable();

    private string channelName = "";
    private string token = "";
    //private string playerName = PhotonNetwork.LocalPlayer.NickName;

    private void Awake()
    {
        photonView = GetComponent<PhotonView>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        //playerNameText = transform.GetChild(0).GetComponent<TextMeshPro>();
    }

    #region GETTERS
    public string GetChannelName() { return channelName; }
    public string GetToken() { return token; }
 
   // public string GetName() { return playerName; }
    #endregion

    #region SETTERS

    public void SetChannelName(string channelName) { this.channelName = channelName; }
    public void SetToken(string token) { this.token = token; }
 
    //public void SetName(string name)
    //{
    //    this.playerName = name;
    //    playerNameText.text = name;
    //}
    #endregion

    private void Update()
    {
        //If the photon view is owned by the player, we will update the local playerdetails and then update the custom properties for that player
        if (photonView.IsMine)
        {
            AddOrDefault(CHANNEL_NAME, channelName);
            AddOrDefault(TOKEN, token);

            //AddOrDefault(PLAYER_NAME, playerName);

            PhotonNetwork.LocalPlayer.SetCustomProperties(playerDetails);
        }
        //If this photon view is owned by some other player, then we will grab the custom properties from the network and assign it to the local fields
        else
        {
            var player = photonView.Owner;

            channelName = (string)player.CustomProperties[CHANNEL_NAME];
            token = (string)player.CustomProperties[TOKEN];

            //playerName = (string)player.CustomProperties[PLAYER_NAME];
            //playerNameText.text = playerName;


        }
    }

    private void AddOrDefault<T>(string key, T value)
    {
        if (playerDetails.ContainsKey(key))
        {
            playerDetails[key] = value;
        }
        else
        {
            playerDetails.Add(key, value);
        }
    }
}
