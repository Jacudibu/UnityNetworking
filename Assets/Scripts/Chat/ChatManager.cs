#define CHAT_ENABLED

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

// Manages Chat Functionality.
// The script is more or less written in the expected sequence of called functions
// Not sure if that's good or bad right now. Comment the #define at the beginning of the script
// if you don't want to use the chat system, in order to safe some memory.
public class ChatManager : NetworkBehaviour
{
    void Awake()
    {
        // No need to have this on every player gameobject or to have it at all if we don't enable chat.
        #if CHAT_ENABLED // Kinda dirty hack, but hey, it works! :p
        if (!isLocalPlayer && !SuperNetworkManager.isServer)
        #endif
            Destroy (this);
    }

    // Call this from an Input Field.
    [Client] public void SendChatMessage(string message)
    {
        if (message == string.Empty)
            return; // What are you even doing here?!

        MessageFromChat chatMessage = ProcessMessage(message);

        if (chatMessage.channelType != (int)ChatChannelType.Command)
        {
            // Just send the Message to the server.
            CmdDoSomethingWithThisMessage(chatMessage);
        }
        else
        {
            // #TODO: Handle Commands like disconnect, clear or even suicide!
        }
    }

    [Client] MessageFromChat ProcessMessage(string message)
    {
        MessageFromChat result = new MessageFromChat();

        if (message.StartsWith("/"))
        {
            // Contains a Command, find out which one.
            result.channelType = (int) GetMessageChannelType(message.Substring(0, message.IndexOf(" ")));

            if (result.channelType != (int) ChatChannelType.Command)
            {
                // Remove the first word of our message for further processing.
                result.message = message.Substring(message.IndexOf(" "), message.Length);
            }
        }
        else
        {
            result.channelType = (int) ChatChannelType.All;
            result.message = message;
        }

        // #TODO: Something something player names something.
        result.sender = "Player " + SuperNetworkManager.connectionID.ToString();

        return result;
    }

    [Client] ChatChannelType GetMessageChannelType(string firstWord)
    {
        firstWord = firstWord.ToLower();
        if (firstWord.StartsWith("/"))
        {
            // Message contains a command. Lets investigate.
            switch (firstWord)
            {
                case ("/t"): return ChatChannelType.Party;
                case ("/team"): return ChatChannelType.Party;
                case ("/p"): return ChatChannelType.Party;
                case ("/party"): return ChatChannelType.Party;
                case ("/w"): return ChatChannelType.Whisper; // whisper
                case ("/f"): return ChatChannelType.Whisper; // fluestern
                case ("/m"): return ChatChannelType.Map;
                default: return ChatChannelType.Command;
            }
        }
        else
            return ChatChannelType.All;
    }

    [Command] void CmdDoSomethingWithThisMessage(MessageFromChat message)
    {
        // #TODO: Send Message to receivers
    }



    [ClientRpc] void RpcReceiveAllMessage(MessageFromChat message)
    {
        // Just print that little piece of ... text.
        PrintChatMessage(message);
    }

    [Client] void PrintChatMessage(MessageFromChat message)
    {
        // #TODO: Display Chat Message in a nice way.
        Debug.Log("[" + message.channelType + "]" + message.sender + ": " + message.message);

        // #NiceToHave: Log Message
    }
}
