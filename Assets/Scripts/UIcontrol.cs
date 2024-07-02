using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Profiling;
using UnityEngine.Android;
using Photon.Pun;

public class UIcontrol : MonoBehaviour
{
    //[SerializeField] private GameObject Player;
    //[SerializeField] private VideoImage videoImage;

    //try new thing 
    private ExitGames.Client.Photon.Hashtable _myCustomProperty = new ExitGames.Client.Photon.Hashtable();
    private void Start()
    {
 
    }


 
    public void openAppBtn()
    {
        Application.OpenURL("https://upskillmafia.com/mern/tasks");
        Debug.Log("hi");
    }
  
   


}
