using UnityEngine;
using UnityEngine.UI;

public class Launcher : MonoBehaviour
{
    public static Launcher instance;

    [Header("Panels")]
    public GameObject lobbyPanel;
    public GameObject createRoomPanel;
    public GameObject findRoomPanel;
    public GameObject roomPanel;

    private void Start()
    {
        instance = this;
        Debug.LogWarning("Welcome, " + Photon.Pun.PhotonNetwork.NickName);
    }

    #region enable/disable panel

    public void CreateRoomPanel()
    {
        createRoomPanel.SetActive(true);
        lobbyPanel.SetActive(false);
        findRoomPanel.SetActive(false);
        roomPanel.SetActive(false);
    }

    public void FindRoomPanel()
    {
        findRoomPanel.SetActive(true);
        createRoomPanel.SetActive(false);
        lobbyPanel.SetActive(false);
        roomPanel.SetActive(false);
    }

    public void LobbyPanel()
    {
        lobbyPanel.SetActive(true);
        createRoomPanel.SetActive(false);
        findRoomPanel.SetActive(false);
        roomPanel.SetActive(false);
    }

    public void RoomPanel()
    {
        roomPanel.SetActive(true);
        lobbyPanel.SetActive(false);
        createRoomPanel.SetActive(false);
        findRoomPanel.SetActive(false);
    }

    #endregion    
}