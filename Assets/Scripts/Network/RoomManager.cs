﻿using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public static RoomManager instance;

    [Header("Texts")]
    public Text roomName;
    public Text masterPlayer;
    public Text localPlayer;

    public Text[] playersInRoom;

    [Header("Properties")]
    public GameObject readyBtn;    

    bool isReady = false;
    int countPlayer = 0;    

    private void Awake()
    {
        instance = this;
        roomName.text = PhotonNetwork.CurrentRoom.Name;        
        
        if (PhotonNetwork.IsMasterClient)
        {
            masterPlayer.text = PhotonNetwork.MasterClient.NickName + " (master)";
        }
        else
        {
            localPlayer.text = PhotonNetwork.NickName;
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

    public void UpdatePlayerList()
    {
        countPlayer = 0;
        foreach (var players in PhotonNetwork.PlayerListOthers)
        {
            if (players.NickName == PhotonNetwork.MasterClient.NickName)
            {
                playersInRoom[countPlayer].text = players.NickName + " (master)";
            }
            else
            {
                playersInRoom[countPlayer].text = players.NickName;
            }
            
            countPlayer++;
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if (newPlayer == null)
        {
            return;
        }

        Debug.Log("Player entered room: " + newPlayer.NickName);

        UpdatePlayerList();        
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log("OnPlayerLeftRoom(): " + otherPlayer.NickName);
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        Debug.Log("OnMasterClientSwitched(): " + newMasterClient.NickName);    
    }
}