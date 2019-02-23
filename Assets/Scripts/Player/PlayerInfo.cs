using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class PlayerInfo : MonoBehaviourPunCallbacks
{
    public Text playerName;
    public GameObject readyBtn;

    int posX = 450;
    int posY = 140;
    int marginPos = 100;

    private void Awake()
    {
        this.transform.SetParent(GameObject.FindGameObjectWithTag("RoomPanel").transform, false);

        playerName.text = this.photonView.Owner.NickName;
    }

    private void Update()
    {
        if (PhotonNetwork.MasterClient.NickName == this.photonView.Owner.NickName)
        {
            this.GetComponent<RectTransform>().localPosition = new Vector3(posX, posY, 0);
        }
    }
}
