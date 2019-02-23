using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class RoomModel : MonoBehaviourPunCallbacks
{
    const byte maxPlayere = 4;

    string roomName;
    bool isEmpty = true;

    private void Start()
    {
        InputField inputField = this.GetComponent<InputField>();
    }

    public void SetRoomName(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            Debug.Log("Room name is null or empty");
            roomName = value;
            isEmpty = true;

            return;
        }
        else
        {
            roomName = value;
            isEmpty = false;
        }        
    }

    public void OnClickCreteRoom()
    {
        if (!isEmpty)
        {            
            PhotonNetwork.CreateRoom(roomName, new RoomOptions { MaxPlayers = maxPlayere } );
        }
        else
        {
            Debug.LogError("Please write room name.");
        }
    }

    public void OnClickFindRoom()
    {        
        if (!isEmpty)
        {
            PhotonNetwork.JoinRoom(roomName);
        }
        else
        {
            Debug.LogError("Please write room name.");
        }
    }

    public void OnClickBackRoom()
    {
        Launcher.instance.LobbyPanel();
    }

    public void OnClickLeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        Launcher.instance.LobbyPanel();        
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.LogError("Failed create room: return code { " + returnCode +" } and message { " + message +" }");        
    }

    public override void OnCreatedRoom()
    {
        Debug.LogWarning("Success created room: " + PhotonNetwork.CurrentRoom.Name);

        Launcher.instance.RoomPanel();       
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("OnJoinedRoom(): " + PhotonNetwork.CurrentRoom.Name);

        Player[] player = PhotonNetwork.PlayerList;
        for (int i = 0; i < player.Length; i++)
        {
            OnPlayerEnteredRoom(player[i]);
            Debug.Log("Joined: " + player[i].NickName);
        }
        
        Launcher.instance.RoomPanel();
        RoomManager.instance.UpdatePlayerList();
    }

    public override void OnLeftRoom()
    {
        Debug.LogWarning("OnLeftRoom(): you're leave room.");
    }
}