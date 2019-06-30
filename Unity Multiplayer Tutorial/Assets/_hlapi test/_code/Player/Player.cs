using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Test
{
    public class Player : NetworkBehaviour
    {
        public int id;
        public GameObject local_ui_go;
        [SyncVar] public Color playerColor;
        
        private string name = "Player";
        private GameObject camera_go;

        public string PlayerName
        {
            get { return $"{name}: [{id}]"; }
        }

        public override void OnStartAuthority()
        {
            Debug.Log(":: I have player authority ::");

            this.camera_go = this.gameObject.transform.GetChild(0).gameObject;
            this.camera_go.SetActive(true);
            this.gameObject.name = PlayerName;

            CreateLocalUi();
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void CreateLocalUi()
        {
            GameObject temp = Instantiate(local_ui_go);

            temp.transform.SetParent(this.gameObject.transform);

            temp.GetComponent<LocalUi>().UpdatePlayerNameText(PlayerName);
        }
    }
}
