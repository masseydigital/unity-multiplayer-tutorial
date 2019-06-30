using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Test
{
    public class Connection : NetworkBehaviour
    {
        public GameObject playerPrefab;
        [SyncVar(hook = "OnPlayerSpawned")] public int player_count;

        // Start is called before the first frame update
        void Start()
        {
            this.transform.SetParent(GameObject.Find("[Connections]").transform);

            if (!hasAuthority)
            {
                return;
            }

            Cmd_SpawnPlayer();
        }

        // Update is called once per frame
        void Update()
        {

        }

        #region Hooks
        public void OnPlayerSpawned(int num)
        {
            Debug.Log("OnPlayerSpawned:: ");

            player_count += 1;
        }
        #endregion Hooks

        #region Commands
        public void Cmd_SpawnPlayer()
        {
            Debug.Log($"Cmd_SpawnPlayer:: Player [{player_count+1}]");

            GameObject go = Instantiate(playerPrefab);

            go.transform.SetParent(GameObject.Find("[Players]").transform);

            go.GetComponent<Player>().id = player_count + 1;

            NetworkServer.SpawnWithClientAuthority(go, connectionToClient);
        }
        #endregion Commands
    }
}
