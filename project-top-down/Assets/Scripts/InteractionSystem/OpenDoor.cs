using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    private bool playerDetected;
    public Transform doorPos;
    public float width;
    public float height;
    public LayerMask whatIsLayer;
    private void Update() 
    {
        playerDetected = Physics2D.OverlapBox(doorPos.position,new Vector2(width, height), 0);
        if(playerDetected == true)
        {

        }
    }

    private void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(doorPos.position, new Vector3(width, height, 1));
    }
}
