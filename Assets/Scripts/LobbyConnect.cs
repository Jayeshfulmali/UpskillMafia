using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class LobbyConnect : MonoBehaviourPunCallbacks
{
    [SerializeField] private string sampleRoomName = "My Room";
    [SerializeField] private PhotonView playerPrefab;

    private void Start()
    {
        // Ensure the client will automatically synchronize scenes
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Joined Lobby");
        JoinOrCreateRoom();
    }

    private void JoinOrCreateRoom()
    {
        Debug.Log("Trying to join or create room: " + sampleRoomName);
        RoomOptions roomOptions = new RoomOptions { MaxPlayers = 20, BroadcastPropsChangeToAll = true };
        PhotonNetwork.JoinOrCreateRoom(sampleRoomName, roomOptions, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        //Debug.Log("Joined room: " + PhotonNetwork.CurrentRoom.Name);
        PhotonNetwork.Instantiate(playerPrefab.name, Vector3.zero, Quaternion.identity);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.LogError("Failed to join room: " + message);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.LogError("Failed to create room: " + message);
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.LogError("Disconnected from Photon: " + cause);
    }
}
