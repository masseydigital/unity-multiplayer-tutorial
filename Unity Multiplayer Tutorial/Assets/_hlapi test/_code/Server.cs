using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Test
{
    public class Server : NetworkBehaviour
    {
        public GameObject players;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        [Command]
        void Cmd_CreatePlayer()
        {

        }
    }
}
