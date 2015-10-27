using UnityEngine;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.Networking;

public class ClientConnection : MonoBehaviour
{
    int clientSocketID = -1;
    //Will store the unique identifier of the session that keeps the connection between the client
    //and the server. You use this ID as the 'target' when sending messages to the server.
    int clientServerConnectionID = -1;
    int maxConnections = 10;
    byte unreliableChannelID;
    byte reliableChannelID;
    bool isClientConnected = false;



    void Start()
    {
        DontDestroyOnLoad(this);

        //Build the global config
        GlobalConfig globalConfig = new GlobalConfig();
        globalConfig.ReactorModel = ReactorModel.FixRateReactor;
        globalConfig.ThreadAwakeTimeout = 10;

        //Build the channel config
        ConnectionConfig connectionConfig = new ConnectionConfig();
        reliableChannelID = connectionConfig.AddChannel(QosType.ReliableSequenced);
        unreliableChannelID = connectionConfig.AddChannel(QosType.UnreliableSequenced);

        //Create the host topology
        HostTopology hostTopology = new HostTopology(connectionConfig, maxConnections);

        //Initialize the network transport
        NetworkTransport.Init(globalConfig);

        //Open a socket for the client
        clientSocketID = NetworkTransport.AddHost(hostTopology, 7790);

        //Make sure the client created the socket successfully
        if (clientSocketID < 0)
        {
            Debug.Log("Client socket creation failed!");
        }
        else
        {
            Debug.Log("Client socket creation success.");
        }

        //Create a byte to store a possible error
        byte error;

        //Connect to the server using 
        //int NetworkTransport.Connect(int socketConnectingFrom, string ipAddress, int port, 0, out byte possibleError);

        //Store the ID of the connection in clientServerConnectionID
        clientServerConnectionID = NetworkTransport.Connect(clientSocketID, "127.0.0.1", 7777, 0, out error);
        //Display the error (if it did error out)
        if (error != (byte)NetworkError.Ok)
        {
            NetworkError networkError = (NetworkError)error;
            Debug.Log("Error: " + networkError.ToString());
        }

    }

    void Update()
    {
        //If the client failed to create the socket, leave this function
        if (clientSocketID < 0)
        {
            return;
        }
        PollBasics();

        //If the user pressed the Space key
        //Send a message to the server "FirstConnect"
        if (Input.GetAxis("Jump") > 0)
        {
            SendMessage("FirstConnect");
        }

        //If the user pressed the R key
        //Send a message to the server "Random message!"
        if (Input.GetKey(KeyCode.R))
        {
            SendMessage("Random message!");
        }
    }

    void SendMessage(string message)
    {
        //create a byte to store a possible error
        byte error;
        //Create a buffer to store the message
        byte[] buffer = new byte[1024];
        //Create a memory stream to send the information through
        Stream memoryStream = new MemoryStream(buffer);
        //Create a binary formatter to serialize and translate the message into binary
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        //Serialize the message

        binaryFormatter.Serialize(memoryStream, message);

        //Send the message from this client, over the client server connection, using the reliable channel
        NetworkTransport.Send(clientSocketID, clientServerConnectionID, reliableChannelID, buffer, (int)memoryStream.Position, out error);
        //Display the error (if it did error out)
        if (error != (byte)NetworkError.Ok) // error is always assigned, and it uses Ok to notate that there is nothing wrong
        {
            NetworkError networkError = (NetworkError)error;
            Debug.Log("Error: " + networkError.ToString());
        }
    }

    void InterperateMessage(string message)
    {
        //if the message is "goto_NewScene"
        //load the level named "Scene2"

        if (message == "goto_Scene2")
            Application.LoadLevel("Scene2");
    }

    void PollBasics()
    {
        //prepare to receive messages by practicing good bookkeeping
        int recHostId;      //Who is receiving the message
        int connectionId;   //Who sent the message
        int channelId;      //What channel the message was sent from
        int dataSize;       //How Large the message can be
        byte[] buffer = new byte[1024]; //the actual message
        byte error;         //If there is an error

        NetworkEventType networkEvent = NetworkEventType.DataEvent;
        //do
        do
        {
            //Receive network events
            networkEvent = NetworkTransport.Receive(out recHostId, out connectionId, out channelId, buffer, 1024, out dataSize, out error);
            //switch on the network event types
            switch (networkEvent)
            {
                //if nothing, do nothing
                case NetworkEventType.Nothing:
                    break;
                //if connection
                case NetworkEventType.ConnectEvent:
                    //verify that the message was meant for me
                    if (recHostId == clientSocketID)
                    {
                        //debug out that i connected to the server, and display the ID of what I connected to
                        Debug.Log("Connected To: " + clientServerConnectionID);
                    }
                    //set my bool that is keeping track if I am connected to a server to true
                    isClientConnected = true;
                    break;
                //if data event
                case NetworkEventType.DataEvent:
                    //verify that the message was meant for me and if I am connected to a server
                    if (recHostId == clientSocketID)
                    {
                        //decode the message (bring it through the memory stream, deseralize it, translate the binary)
                        Stream memoryStream = new MemoryStream(buffer);
                        BinaryFormatter binaryFormatter = new BinaryFormatter();

                        string message = binaryFormatter.Deserialize(memoryStream).ToString();
                        //Debug the message and the connection that the message was sent from 
                        Debug.Log("Client: Received Data from " + connectionId.ToString() + "! Message : " + message);
                        InterperateMessage(message);
                    }
                    break;
                //if disconnection
                case NetworkEventType.DisconnectEvent:
                    //verify that the message was meant for me, and that I am disconnecting from the current connection I have with the server
                    if (recHostId == clientSocketID)
                    {
                        //debug that I disconnected
                        Debug.Log("Client > Disconnected from server.");
                        //set my bool that is keeping track if I am connected to a server to false
                        isClientConnected = false;
                    }
                    break;
            }
            //while (the network event I am receiving is not Nothing)
        } while (networkEvent != NetworkEventType.Nothing);

    }
}
    