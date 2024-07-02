using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NameButtonPlayer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _playerNameByClick;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayerNameClick()
    {
        if(_playerNameByClick.text== PhotonNetwork.NickName)
        {
            Debug.Log("name same hai");
            Debug.Log(_playerNameByClick.text);
            Debug.Log(PhotonNetwork.NickName);
            return;
        }
        else
        {

        Debug.Log(_playerNameByClick.text+" list player");
        FindObjectOfType<ChatForPlayer>().ClickOnName();
        FindObjectOfType<ChatForPlayer>().onChatClickJoin(_playerNameByClick.text);
        FindObjectOfType<ChatForPlayer>()._playerNameClicked = _playerNameByClick.text;
        }
    }

  
}
