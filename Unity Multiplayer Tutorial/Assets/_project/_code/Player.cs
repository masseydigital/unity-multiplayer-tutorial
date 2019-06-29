using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class Player : NetworkBehaviour
{
    [SyncVar(hook="OnPlayerNameChanged")]

    public string playerName = "Anonymous";

    public GameObject playerUnitPrefab;
    public TextMeshProUGUI playerName_Tmpro;

    // Start is called before the first frame update
    void Start()
    {
        /* Non-networked code */
        //Debug.Log("PlayerObject::Start -- Spawning my own personal unit.");
        //Instantiate(playerUnitPrefab);
        //Instantiate only creates a copy on the LOCAl computer.  Even if it has a NetworkIdentity component.

        /* Networked code */
        if (!hasAuthority)
        {
            return;
        }

        // Command server to spawn the unit
        Cmd_SpawnUnit();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp("s"))
        {
            Cmd_SpawnUnit();
        }

        if (Input.GetKeyUp("q"))
        {
            string name = "Player " + Random.Range(1, 100);

            Debug.Log("Sending request to server to change name");
            Cmd_ChangePlayerName(name);
        }
    }

    void OnPlayerNameChanged(string newName)
    {
        Debug.Log("OnPlayerNameChanged:: Old Name: " + playerName + " New Name: " + newName);

        playerName = newName;

        gameObject.name = $"Player  [{newName}]";
    }

    #region Commands
    [Command]
    public void Cmd_SpawnUnit()
    {
        GameObject go = Instantiate(playerUnitPrefab);

        NetworkServer.SpawnWithClientAuthority(go, connectionToClient);
    }

    [Command]
    void Cmd_ChangePlayerName(string n)
    {
        Debug.Log("Cmd_ChangePlayerName" + n);

        playerName = n;

        playerName_Tmpro.text = playerName;

        //Rpc_ChangePlayerName(n);

        //Tell all the clients what the players name now is
    }
    #endregion

    #region RPC
    /* RPC replaced by SyncVar */
    //[ClientRpc]
    //void Rpc_ChangePlayerName(string n)
    //{
    //Debug.Log("Rpc_ChangePlayerName: Changing player name.");
    //
    //    playerName = n;
    //
    //    playerName_Tmpro.text = playerName;
    //}
    #endregion
}
