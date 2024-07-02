using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.UIElements;
public class PlayerNameList : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject playerNamePrefab; // Prefab with Text or TextMeshPro component
    [SerializeField] private Transform playerNameContainer; // Container with Vertical Layout Group
    private Dictionary<int, GameObject> playerNameObjects = new Dictionary<int, GameObject>();

    public Animator _animPlayerNameList;
    public int _personalActorNumber;

    //buttons
    [SerializeField] private GameObject _openBtn;
  
    [SerializeField] private TextMeshProUGUI _playerOwnName;
    // Called when the local player joins a room
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        // Set _personalActorNumber to the local player's actor number
        _personalActorNumber = PhotonNetwork.LocalPlayer.ActorNumber;
        _playerOwnName.text = PhotonNetwork.LocalPlayer.NickName;
        UpdatePlayerList();
    }

    // Called when a new player joins the room
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        AddPlayerName(newPlayer);
    }

    // Called when a player leaves the room
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);
        RemovePlayerName(otherPlayer);
    }

    private void UpdatePlayerList()
    {
        foreach (KeyValuePair<int, Player> playerInfo in PhotonNetwork.CurrentRoom.Players)
        {
            AddPlayerName(playerInfo.Value);
        }
    }

    private void AddPlayerName(Player player)
    {
        // Check if the player is not the local player
        if (player.ActorNumber != _personalActorNumber)
        {
            if (!playerNameObjects.ContainsKey(player.ActorNumber))
            {
                GameObject playerNameObject = Instantiate(playerNamePrefab, playerNameContainer);
                TextMeshProUGUI textComponent = playerNameObject.GetComponentInChildren<TextMeshProUGUI>();
                textComponent.text = player.NickName;
                playerNameObjects.Add(player.ActorNumber, playerNameObject);
            }
        }
    }

    private void RemovePlayerName(Player player)
    {
        if (playerNameObjects.TryGetValue(player.ActorNumber, out GameObject playerNameObject))
        {
            Destroy(playerNameObject);
            playerNameObjects.Remove(player.ActorNumber);
        }
    }

    public void openPlayerListNamePanel()
    {
       
            transform.Translate(Vector3.right * 3);
            _animPlayerNameList.SetBool("PlayerNamePanel", true);
            //FindObjectOfType<MovingBlock>().MoveRight();
            //foreach (var block in FindObjectsOfType<MovingBlock>())
            //{
            //    block.MoveRight();
            //}
        _openBtn.SetActive(false);
        
    }
    public void ClosePlayerListNamePanel()
    {
        _animPlayerNameList.SetBool("PlayerNamePanel", false);
        // FindObjectOfType<MovingBlock>().MoveLeft();

        //foreach (var block in FindObjectsOfType<MovingBlock>())
        //{
        //    block.MoveLeft();
        //}
        _openBtn.SetActive(true);
    }
}
