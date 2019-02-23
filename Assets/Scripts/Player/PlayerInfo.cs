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
        this.transform.SetParent(GameObject.FindGameObjectWithTag("RoomPanel").transform, false);

        playerName.text = this.photonView.Owner.NickName;
    }
}
