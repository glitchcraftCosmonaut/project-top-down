using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) 
    {
        
        if(other.tag == "Player")
        {
            other.GetComponentInChildren<HealthBar>().hp -= 20;
            CinemachineShake.Instance.ShakeCamera(5f, 0.1f);
        }
    }
}
