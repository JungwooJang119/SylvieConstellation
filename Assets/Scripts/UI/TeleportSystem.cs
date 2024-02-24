using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportSystem : MonoBehaviour
{
    public Transform player;

    public void Teleport(GameObject newPos)
    {
        //move player to given new position 
        player.transform.position = newPos.transform.position;
    }
    
}
