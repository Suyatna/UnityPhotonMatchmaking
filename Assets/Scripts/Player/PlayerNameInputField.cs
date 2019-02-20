using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class PlayerNameInputField : MonoBehaviour
{
    const string playerNamePrefKey = "PlayerName";

    bool isEmpty = true;

    // Start is called before the first frame update
    void Start()
    {
        string defaultName = string.Empty;
        InputField inputField = this.GetComponent<InputField>();

        if (inputField != null)
        {
            if (PlayerPrefs.HasKey(playerNamePrefKey))
            {
                defaultName = PlayerPrefs.GetString(playerNamePrefKey);
                inputField.text = defaultName;
            }
        }

        PhotonNetwork.NickName = defaultName;
    }

    public void SetPlayerName(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            Debug.Log("Player name is null or empty");
            isEmpty = true;

            return;
        }
        else
        {
            isEmpty = false;
        }

        PhotonNetwork.NickName = value;

        PlayerPrefs.SetString(playerNamePrefKey, value);
    }    

    public void ChangeScene()
    {
        if (!isEmpty)
        {
            PhotonNetwork.LoadLevel(1);
        }
        else
        {
            Debug.LogError("Please write your name.");
        }
    }
}