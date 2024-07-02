using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Photon.Pun;
using TMPro;
using WebSocketSharp;
using Photon.Realtime;
using UnityEngine.UI;
using System;
using Photon.Pun.Demo.PunBasics;
//using System.Drawing;

public class PlayerMovement : MonoBehaviourPunCallbacks
{
    //[SerializeField] private Speaker _sp;
    public float _playerSpeed = 2.0f;
    public TextMeshPro _playerName;
    //public FixedJoystick joystick;
    Rigidbody2D _rb;
    Vector2 move;
    public Renderer playerRenderer;
    private Color playerColor = Color.black;

    public Player Player { get; private set; }
    private bool colorCheck;

    //canvas panel
    [SerializeField] private GameObject _playerCanvas,_avatarPanel;
    private bool avatarPanelOpen;
    private bool hasMoved = false;




    private void Awake()
    {
        
        _rb = GetComponent<Rigidbody2D>();
        _playerSpeed = 2.0f;

        PhotonCustomTypes.Register();
        avatarPanelOpen = false;

        
    }
    private void Start()
    {
        //_sp.enabled = false;


        //_playerCanvas.enabled = false;
        if (photonView.IsMine)
        {
            _playerName.text = PhotonNetwork.NickName;
            _playerCanvas.SetActive(true);
        }

        else
        {
            // Set the name of the other players
            _playerName.text = photonView.Owner.NickName;
        }
        

    }
    private void Update()
    {


        if (photonView.IsMine)
        {

            move.x = FindObjectOfType<JoystickTouch>().joystick.Horizontal;
        move.y = FindObjectOfType<JoystickTouch>().joystick.Vertical;
        
        if (move.x !=0 || move.y != 0)
        {
            _playerSpeed = 2.0f;
            _rb.bodyType = RigidbodyType2D.Dynamic;
            _rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                hasMoved = true;
                
        }
            else
            {
                hasMoved = false;
               

            }



        }

    }

    private void FixedUpdate()
    {

        if (photonView.IsMine)
        {

            _rb.MovePosition(_rb.position + move * _playerSpeed * Time.deltaTime);
        }

    }

    // Method to be called by the UI button to change color

    public void AvatarBtn()
    {
        if (photonView.IsMine)
        {
            if (!avatarPanelOpen)
            {
            _avatarPanel.SetActive(true);
                avatarPanelOpen = true;
            }
            else
            {
                _avatarPanel.SetActive(false);
                avatarPanelOpen = false;
            }
        }
    }
    public void ChangeColor(int val)
    {
        if (photonView.IsMine)
        {
            if (val == 0)
            {
            playerColor = Color.white;
            }
            if (val == 1)
            {
                playerColor = Color.red;
            }
            if (val == 2)
            {
                playerColor = Color.blue;
            }
            if (val == 3)
            {
                playerColor = Color.green;
            }
            photonView.RPC("UpdateColor", RpcTarget.AllBuffered, playerColor);
        }
    }

    // RPC method to update color across all clients
    [PunRPC]
    void UpdateColor(Color newColor)
    {
        playerColor = newColor;
        playerRenderer.material.color = playerColor;
    }

    // Method to synchronize color over the network
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(playerColor);
        }
        else
        {
            playerColor = (Color)stream.ReceiveNext();
            playerRenderer.material.color = playerColor;
        }
    }

    public bool HasPlayerMoved()
    {
        return hasMoved;
    }






}


