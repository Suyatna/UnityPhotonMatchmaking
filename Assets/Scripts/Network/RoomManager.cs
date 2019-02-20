using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class RoomManager : MonoBehaviourPunCallbacks
{
    [Header("Texts")]
    public Text roomName;
    public Text masterPlayer;    

    private void Awake()
    {
        roomName.text = PhotonNetwork.CurrentRoom.Name;
        masterPlayer.text = PhotonNetwork.MasterClient.NickName;
    }
}