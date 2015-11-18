using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class ChatManager : NetworkBehaviour
{
    [Client] void SendChatMessage(string message)
    {
        // Just send the Message to the server.
        if (message.CompareTo("") != 0)
            CmdProcessMessage(message, SuperNetworkManager.connectionID);
    }

    ChatChannelType GetMessageChannel(string message)
    {
        if (message.StartsWith("/"))
        {
            // Message contains a command. Lets investigate.
            string channelCommand = message.Split(' ')[0];
            channelCommand = channelCommand.ToLower();

            switch (channelCommand)
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

    [Command] void CmdProcessMessage(string message, int senderID)
    {
        // #TODO: Check if Message is valid

        // #TODO: Send Message to receivers
    }

    [Client] void ReceiveChatMessage(ChatChannelType channel, string message)
    {
        // #TODO: Display Chat Message

        // #NiceToHave: Log Message
    }

    [ClientRpc] void ReceiveAllMessage(string message)
    {
        // #TODO: Print message to chat.
    }
}
