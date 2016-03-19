using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Nameplate : NetworkBehaviour
{
    public TextMesh nameplateText;
    NetworkObject player;

	void Start ()
    {
        player = GetComponent<NetworkObject>();

        if (hasAuthority)
        {
            CmdSetName();
            NetworkEventManager.OnConnectionIDChanged += NameChanged;
        }
        else
        {
            SetName();
        }
	}
	
    void SetName()
    {
        if (player.ownerConnectionID > 0) // Client
            player.name = "Player " + (player.ownerConnectionID + 1).ToString();
        else // Host
            player.name = "Player 1";

        nameplateText.text = player.name;
    }

    void NameChanged()
    {
        CmdSetName();
    }

    [Command] void CmdSetName()
    {
        RpcSetName();
    }

    [ClientRpc] void RpcSetName()
    {
        SetName();
    }


}
