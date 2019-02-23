using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class PlayerInfo : MonoBehaviourPunCallbacks
{
    public Text playerName;
    public GameObject readyBtn;

    private void Awake()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            playerName.text = PhotonNetwork.NickName + " (master)";
            playerName.color = new Color(255, 255, 0);
        }
        else
        {
            playerName.text = PhotonNetwork.NickName;
        }        
    }
}
