using UnityEngine;
using System.Collections;
using PlayerIOClient;
using System.Collections.Generic;

public class MultiplayerManager : MonoBehaviour {
    #region Singleton
    static MultiplayerManager s_Instance;
    static public MultiplayerManager GetInstance {
        get {
            return s_Instance;
        }
    }
    void Awake() {
        if (s_Instance == null) {
            s_Instance = this;
        }
        DontDestroyOnLoad(this);
    }
    #endregion

    #region PlayerIO stuff
    private Connection pioconnection;
    private List<PlayerIOClient.Message> msgList = new List<PlayerIOClient.Message>(); //  Messsage queue implementation
    //private bool joinedroom = false;
    private PlayerIOClient.Client pioclient;
    public bool isConnected {
        get {
            return pioconnection != null
            ? pioconnection.Connected
            : false;
        }
    }
    public string userId = "";

    public bool developmentServer;
    public bool localhost;
    public string ipDevServ = "192.168.1.3";
    #endregion

    void Start() {
        startConnection();
    }

    public void startConnection() {
        string playerId = SystemInfo.deviceUniqueIdentifier;

        //user is just using this device with no account
        Debug.Log("Annonymous connect : " + playerId);
        userId = playerId;
        PlayerIOClient.PlayerIO.Connect(
            "one-line-defense-chqahbaeyswtsujkbrxg",	// Game id 
            "public",							        // The id of the connection, as given in the settings section of the admin panel. By default, a connection with id='public' is created on all games.
            playerId,							        // The id of the user connecting. 
            auth: null,								    // If the connection identified by the connection id only accepts authenticated requests, the auth value generated based on UserId is added here
            partnerId: null,
            playerInsightSegments: null,
            successCallback: delegate(Client client) {
            successfullConnect(client);
        },
            errorCallback: delegate(PlayerIOError error) {
            Debug.Log("Error connecting: " + error.ToString());
        }
        );
    }

    void successfullConnect(Client client) {
        Debug.Log("Successfully connected to Player.IO");

        if (developmentServer) {
            client.Multiplayer.DevelopmentServer = new ServerEndpoint(System.String.IsNullOrEmpty(ipDevServ) ? "192.168.1.96" : ipDevServ, 8184);
        }
        if (localhost) {
            client.Multiplayer.DevelopmentServer = new ServerEndpoint("127.0.0.1", 8184);
        }

        //Create or join the room	
        string roomId = "RoomId";
        if (string.IsNullOrEmpty(roomId)) {
            roomId = userId;
        }

        client.Multiplayer.CreateJoinRoom(
            roomId,             //Room is the Alliance of the player 
            "TestingRoom",      //The room type started on the server
            visible: false,     //Should the room be visible in the lobby?
            roomData: null,
            joinData: null,
            successCallback: delegate(Connection connection) {
            Debug.Log("Joined Room : " + roomId);
            // We successfully joined a room so set up the message handler
            pioconnection = connection;
            pioconnection.OnMessage += handlemessage;
            pioconnection.OnDisconnect += disconnected;
            //joinedroom = true;
        },
            errorCallback: delegate(PlayerIOError error) {
            Debug.LogError("Error Joining Room: " + error.ToString());
        }
        );

        pioclient = client;
    }

    public void disconnect() {
        if (!pioconnection.Connected)
            return;
        pioconnection.Disconnect();
    }

    public void disconnected(object sender, string error) {
        Debug.LogWarning("Disconnected !");
    }

    void FixedUpdate() {
        // process message queue
        foreach (PlayerIOClient.Message m in msgList) {
            Debug.Log(Time.time + " - Message received from server " + m.ToString());
            switch (m.Type) {
                //Basic connection/deconnection

                //Lobby Messages
                case "PlayerJoined":
                    Debug.Log("PlayerJoined : " + m.GetString(0));
                    break;
                case "PlayerLeft":
                    Debug.Log("PlayerLeft : " + m.GetString(0));
                    break;
                case "gameStarted":

                    break;
                case "Chat":
                    Debug.Log(m.GetString(0) + ":" + m.GetString(1));
                    break;
                case "UserUpdated":
                    Debug.Log(m.GetString(0));
                    break;
            }
        }

        // clear message queue after it's been processed
        msgList.Clear();
    }

    void handlemessage(object sender, PlayerIOClient.Message m) {
        msgList.Add(m);
    }

    void joinGameRoom(string roomId) {
        pioclient.Multiplayer.CreateJoinRoom(
        roomId,			//Room is the Alliance of the player 
        "TestingRoom",	//The room type started on the server
        false,			//Should the room be visible in the lobby?
        null,
        null,
        delegate(Connection connection) {
            Debug.Log("Joined Room : " + roomId);
            // We successfully joined a room so set up the message handler
            pioconnection = connection;
            pioconnection.OnMessage += handlemessage;
            pioconnection.OnDisconnect += disconnected;
            //joinedroom = true;

        },
        delegate(PlayerIOError error) {
            Debug.LogError("Error Joining Room: " + error.ToString());
        }
        );
    }

    void OnLevelWasLoaded(int level) {
        if (Application.loadedLevelName.Equals("Game")) {

        }
    }

    #region Methods sent to server
    public void SendStart() {
        Debug.Log("Sending Start to Server");
        pioconnection.Send("start");
    }

    public void SendChat(string text) {
        pioconnection.Send("Chat", text);
    }

    public void SendCreateUser(string userName) {
        pioconnection.Send("CreateUser", userName);
    }

    public void LoadUser(string userID) {
        pioclient.BigDB.Load("Users", userID,
            delegate(DatabaseObject user) {
                string userName = user.GetString("name", "defaultName");
                Debug.Log(userName);
            },
            delegate(PlayerIOError error) {
                Debug.LogError(string.Format(
                    "MultiplayerManager::LoadUser => {0}",
                    error.Message));
            });
    }

    public void UpdateUser(string userName, string userTest) {
        pioconnection.Send("UpdateUser", userName, userTest);
    }
    #endregion
}
