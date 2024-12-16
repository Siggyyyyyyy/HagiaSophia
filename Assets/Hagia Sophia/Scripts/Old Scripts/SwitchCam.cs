using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public GameObject player;
    public KeyCode switchKey = KeyCode.T; // Taste zum Umschalten
    bool playeroff = true;

    void Start()
    {
      
        
    }

    void Update()
    {
        if (Input.GetKeyDown(switchKey) && playeroff == true)
         {
            player.SetActive(true);
            playeroff = false;
         }

        else if (Input.GetKeyDown(switchKey) && playeroff == false)
         {
            player.SetActive(false);
            playeroff = true;
         }
    }

}
