using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerId : MonoBehaviour
{

    public void JoinFun()
    {
        string _channelName = "Jayesh";
        //FindObjectOfType<AgoraVideoChat>().JoinChannelOnTrigger(_channelName);
        
        Debug.Log("Join");
    }
    public void LeaveFun()
    {
        //_agora.JoinOriginalChannel();
        //FindObjectOfType<AgoraVideoChat>().LevelChannelOnTriggerExit();
        Debug.Log("Leave");
    }
}
