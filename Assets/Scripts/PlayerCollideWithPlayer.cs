//using Agora_RTC_Plugin.API_Example.Examples.Basic.JoinChannelAudio;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class PlayerCollideWithPlayer : MonoBehaviourPunCallbacks
{
    
    private bool _isTriggerChannel;
    
    [SerializeField] private Rigidbody2D _rbPlayer;


    private void Start()
    {
        
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "MainPlayer")
        {
             
            _rbPlayer.bodyType = RigidbodyType2D.Static;


        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "MainPlayer")
        {

          

        }

        _rbPlayer.bodyType = RigidbodyType2D.Dynamic;
        _rbPlayer.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
   
}
