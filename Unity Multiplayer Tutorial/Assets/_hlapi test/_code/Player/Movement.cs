using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Test
{
    public class Movement : NetworkBehaviour
    {
        private float speed = 8;

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

            if (Input.GetKey("w"))
            {
                Move(Vector3.forward);
            }

            if (Input.GetKey("d"))
            {
                Move(Vector3.right);
            }

            if (Input.GetKey("a"))
            {
                Move(Vector3.left);
            }

            if (Input.GetKey("s"))
            {
                Move(Vector3.back);
            }
        }

        private void Move(Vector3 dir)
        {
            this.transform.Translate(dir * Time.deltaTime * speed);
        }
    }
}
