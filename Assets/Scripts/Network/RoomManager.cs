using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class RoomManager : MonoBehaviourPunCallbacks
{
    [Header("Texts")]
    public Text roomName;
    public Text masterPlayer;

    [Header("Properties")]
    public GameObject readyBtn;
    bool isReady = false;    

    public Text[] playersInRoom, clientsInRoom;
    public Text localPlayer;
    int countPlayer = 0;

    private void Awake()
    {
        roomName.text = PhotonNetwork.CurrentRoom.Name;        
        
        if (!PhotonNetwork.IsMasterClient)
        {
            localPlayer.text = PhotonNetwork.NickName;
        }
        else
        {
            masterPlayer.text = PhotonNetwork.MasterClient.NickName;
        }
    }

    private void Update()
    {
        if (isReady)
        {
            readyBtn.GetComponent<Image>().fillAmount += Time.deltaTime;
        }
        else
        {
            readyBtn.GetComponent<Image>().fillAmount -= Time.deltaTime;
        }
    }

    public void OnClickReady(bool value)
    {
        isReady = value;
    }

    public override void OnConnected()
    {
        photonView.RPC("UpdatePlayerLists", RpcTarget.AllBuffered);
    }    

    public override void OnJoinedRoom()
    {
        Player[] player = PhotonNetwork.PlayerList;
        for (int i = 0; i < player.Length; i++)
        {
            OnPlayerEnteredRoom(player[i]);
            Debug.Log(player[i].NickName);
        }

        Debug.Log("OnJoinedRoom(): " + PhotonNetwork.CurrentRoom.Name);

        Launcher.instance.RoomPanel();

        countPlayer = 0;
        foreach (var players in PhotonNetwork.PlayerListOthers)
        {
            clientsInRoom[countPlayer].text = players.NickName;
            Debug.Log("All player: " + players.NickName);

            countPlayer++;
        }
    }    

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if (newPlayer == null)
        {
            return;
        }

        countPlayer = 0;
        foreach (var players in PhotonNetwork.PlayerListOthers)
        {
            playersInRoom[countPlayer].text = players.NickName;
            countPlayer++;
        }

        Debug.Log("Player entered room: " + newPlayer);
    }    

    [PunRPC]
    void UpdatePlayerLists()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            for (int i = 1; i < PhotonNetwork.CountOfPlayersInRooms; i++)
            {
                playersInRoom[i].text = PhotonNetwork.PlayerList[i].NickName;
            }
        }        
    }
}