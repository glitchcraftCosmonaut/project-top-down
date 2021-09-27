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
            EventSystem.instance.CameraShakeEvent(0.2f);
        }
    }
}
