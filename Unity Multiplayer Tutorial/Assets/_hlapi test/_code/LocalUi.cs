using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class LocalUi : NetworkBehaviour
{
    public TextMeshProUGUI playerNameText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdatePlayerNameText(string txt)
    {
        playerNameText.text = txt;
    }
}
