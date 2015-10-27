using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.Networking;

/// <summary>
/// @Author: Andrew Seba
/// @Description: 
/// </summary>
public class ServerConnection : MonoBehaviour {

    int serverSocketID = -1;
    int maxConnections = 10;
    byte unreliableChannelID;
    byte reliableChannelID;
    bool serverInitialized = false;



    void Start()
    {
        GlobalConfig globalConfig = new GlobalConfig();
        globalConfig.ReactorModel = ReactorModel.FixRateReactor;
        globalConfig.ThreadAwakeTimeout = 10;

        ConnectionConfig connectionConfig = new ConnectionConfig();
        reliableChannelID = connectionConfig.AddChannel(QosType.ReliableSequenced);
        unreliableChannelID = connectionConfig.AddChannel(QosType.UnreliableSequenced);

        HostTopology hostTopology = new HostTopology(connectionConfig, maxConnections);

        NetworkTransport.Init(globalConfig);

        serverSocketID = NetworkTransport.AddHost(hostTopology, 7777);

        if(serverSocketID < 0)
        {
            Debug.Log("Server socket creation failed!");
        }
        else
        {
            Debug.Log("Server socket creation success.");
        }

        serverInitialized = true;
        

        DontDestroyOnLoad(this);
        
    }

    void Update()
    {
        if (!serverInitialized)
        {
            return;
        }

        int recHostId;      //Who is receiving the message
        int connectionId;   //Who sent the message
        int channelId;      //What channel the message was sent from
        int dataSize;       //How Large the message can be
        byte[] buffer = new byte[1024]; //the actual message
        byte error;         //If there is an error

        NetworkEventType networkEvent = NetworkEventType.DataEvent;

        do
        {
            networkEvent = NetworkTransport.Receive(out recHostId, out connectionId, out channelId, buffer, 1024, out dataSize, out error);

            switch (networkEvent)
            {
                case NetworkEventType.Nothing:
                    break;
                case NetworkEventType.ConnectEvent:
                    //Server received connect event
                    if(recHostId == serverSocketID)
                    {
                        Debug.Log("Server: Player " + connectionId.ToString() + " connected!");
                    }

                    
                    break;
                case NetworkEventType.DataEvent:
                    if( recHostId == serverSocketID)
                    {
                        //open a memeory stream with a size equal to the buffer we set up earlier
                        Stream memoryStream = new MemoryStream(buffer);

                        //Create a binary formatter to begin reading the information from the memory stream
                        BinaryFormatter binaryFormatter = new BinaryFormatter();

                        //utilize the binary formatter to deserialize the binary information stored in the memory string
                        //then conver tthat into a string
                        string message = binaryFormatter.Deserialize(memoryStream).ToString();

                        //debug out the message you worked so hard to figure out!
                        Debug.Log("Server: Received Data from " + connectionId.ToString() + "! Message : " + message);
                        
                        RespondMessage(message, connectionId);
                    }
                    break;
                case NetworkEventType.DisconnectEvent:
                    if(recHostId == serverSocketID)
                    {
                        Debug.Log("Server: Received disconnect from " + connectionId.ToString());
                    }
                    break;
            }
        } while (networkEvent != NetworkEventType.Nothing);
    }

    void RespondMessage(string message, int playerID)
    {
        if(message == "FirstConnect")
        {
            Debug.Log("Got the first connect from player: " + playerID);
            SendMessage("goto_Scene2", playerID);
            if(Application.loadedLevelName != "Scene2")
                Application.LoadLevel("Scene2");
        }
    }

    void SendMessage(string message, int target)
    {
        byte error;
        byte[] buffer = new byte[1024];
        Stream memoryStream = new MemoryStream(buffer);
        BinaryFormatter binaryFormatter = new BinaryFormatter();

        binaryFormatter.Serialize(memoryStream, message);

                            //Who is sending, where to, what channel,   what info,    how much info,      if there is an error 
        NetworkTransport.Send (serverSocketID, target, reliableChannelID, buffer, (int)memoryStream.Position, out error);

        if(error != (byte)NetworkError.Ok ) // error is always assigned, and it uses Ok to notate that there is nothing wrong
        {
            NetworkError networkError = (NetworkError)error;
            Debug.Log("Error: " + networkError.ToString());
        }
    }
}
