using UnityEngine;
using Photon.Pun;
using TMPro;

public class PlayerCollisionsHandler : MonoBehaviour
{
    [SerializeField] private GameObject _appBtn, _locationBtn;
    [SerializeField] private TextMeshProUGUI _locationText;

    private void Start()
    {
        _appBtn.SetActive(false);
        _locationBtn.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!PhotonView.Get(this).IsMine) return;




        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerInfo collidedPlayerInfo = collision.gameObject.GetComponent<PlayerInfo>();
            PlayerInfo mainPlayerInfo = GetComponent<PlayerInfo>();

            //AgoraManager.Instance.JoinChannel(mainPlayerInfo, collidedPlayerInfo);
        }


        if (collision.gameObject.name == "Room1")
        {
            _locationText.text = "Discussion Room 1";
            _appBtn.SetActive(true);
            _locationBtn.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!PhotonView.Get(this).IsMine) return;

        if (collision.gameObject.CompareTag("Player"))
        {
            //AgoraManager.Instance.LeaveChannelIfNoOtherUsersPresent();
        }
    }
}
