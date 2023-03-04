using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

/*
This class is responsible for Main Menu logic using Photon Networking system
Tracks user inputs
Connects players to the lobby
*/
public class Launcher : MonoBehaviourPunCallbacks
{
    public static Launcher Instance;

    [SerializeField]
    private TMP_InputField roomNameInputField;

    [SerializeField]
    private TMP_Text errorText;

    [SerializeField]
    private TMP_Text roomNameText;

    [SerializeField]
    private Transform roomListContent;

    [SerializeField]
    private Transform playerListContent;

    [SerializeField]
    private GameObject roomListItemPrefab;

    [SerializeField]
    private GameObject playerListItemPrefab;

    [SerializeField]
    private GameObject startGameButton;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    // Connects to one of the Photon Servers
    void Start()
    {
        Debug.Log("Connecting to Master Server");
        PhotonNetwork.ConnectUsingSettings();
    }

    // Connects to available Regional Photon Server
    // Creates Lobby
    // Syncs game state within users
    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to " + PhotonNetwork.CloudRegion + " Server");
        PhotonNetwork.JoinLobby();
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    // Displays title
    // Assigns unique name to a joined player
    public override void OnJoinedLobby()
    {
        Debug.Log("Joined Lobby");
        MenuManager.instance.OpenMenu("Title");
        PhotonNetwork.NickName = "Player " + Random.Range(0, 1000).ToString("0000");
    }

    // Creates a room for gathering players
    public void CreateRoom()
    {
        if (string.IsNullOrEmpty(roomNameInputField.text))
        {
            return;
        }

        PhotonNetwork.CreateRoom(roomNameInputField.text);
        MenuManager.instance.OpenMenu("Loading");
    }

    // Shows joined players as a list
    public override void OnJoinedRoom()
    {
        MenuManager.instance.OpenMenu("Room");
        roomNameText.text = PhotonNetwork.CurrentRoom.Name;
        Player[] players = PhotonNetwork.PlayerList;

        foreach (Transform child in playerListContent)
        {
            Destroy(child.gameObject);
        }
        for (int i = 0; i < players.Count(); i++)
        {
            Instantiate(playerListItemPrefab, playerListContent)
                .GetComponent<PlayerListItem>()
                .SetUp(players[i]);
        }
        startGameButton.SetActive(PhotonNetwork.IsMasterClient);
    }

    // Switches master player
    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        startGameButton.SetActive(PhotonNetwork.IsMasterClient);
    }

    // Displays an error message
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        errorText.text = "Room Creation Failed: " + message;
        MenuManager.instance.OpenMenu("Error");
    }

    // Starts the game loading level 1 in Photon's server
    public void StartGame()
    {
        PhotonNetwork.LoadLevel(1);
    }

    // Disconnects user from Photon server
    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        MenuManager.instance.OpenMenu("Loading");
    }

    // Launches Menu on leaving a room
    public override void OnLeftRoom()
    {
        MenuManager.instance.OpenMenu("Title");
    }

    // Displays joined room info
    public void JoinRoom(RoomInfo info)
    {
        PhotonNetwork.JoinRoom(info.Name);
        MenuManager.instance.OpenMenu("Loading");
    }

    // Lists rooms
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (Transform trans in roomListContent)
        {
            Destroy(trans.gameObject);
        }

        for (int i = 0; i < roomList.Count; i++)
        {
            if (roomList[i].RemovedFromList)
                continue;
            Instantiate(roomListItemPrefab, roomListContent)
                .GetComponent<RoomListItem>()
                .SetUp(roomList[i]);
        }
    }
    // public override void OnPlayerEnteredRoom(Player newPlayer) { }
}
