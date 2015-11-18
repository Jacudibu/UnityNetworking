using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

enum NetworkMessageType
{
    personalConnectionID = 1000,

    chatMessage,
}



class MessageWithInt : MessageBase { public int value = 0; }
class MessageWithString : MessageBase { public string value = ""; }
class MessageWithFloat : MessageBase { public float value = 0f; }
class MessageChat : MessageBase { public int sender = -1; public int channelType = -1; public string message = "";}