using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Tutorial
{
    public class Move : NetworkBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

            if (!hasAuthority)
            {
                return;
            }

            if (Input.GetKeyUp("space"))
            {
                this.transform.Translate(Vector3.up);
            }
        }
    }
}
