using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour
{
    public string playerName;
    public GameObject playerUnitPrefab;

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
    }

    #region Commands
    [Command]
    public void Cmd_SpawnUnit()
    {
        GameObject go = Instantiate(playerUnitPrefab);

        NetworkServer.SpawnWithClientAuthority(go, connectionToClient);

        
    }
    #endregion
}
