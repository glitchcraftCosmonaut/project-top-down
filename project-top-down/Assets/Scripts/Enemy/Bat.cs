using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : Enemy
{
    // public Transform wayPoint01, wayPoint02;
    // private Transform wayPointTarget;
    [SerializeField] private float attackRange;
    [HideInInspector]
    public float batPositionX, batPositionY;
    private void Awake()
    {
        // wayPointTarget = wayPoint01;//At the beginning, bat move to the right waypoint
    }
    protected override void Move()
    {
        base.Move();
        batPositionX = transform.position.x;
        batPositionY = transform.position.y;

        if(!isDead)
        {
        //MARKER Override Part
            // if(Vector2.Distance(transform.position, target.position) > distance)
            // {
            //     //When we reached at the waypoint01, we have to mvoe to the waypoint 02
            //     if(Vector2.Distance(transform.position, wayPoint01.position) < 0.01f)
            //     {
            //         wayPointTarget = wayPoint02;
            //     }

            //     if(Vector2.Distance(transform.position, wayPoint02.position) < 0.01f)
            //     {
            //         wayPointTarget = wayPoint01;
            //     }

            //     transform.position = Vector2.MoveTowards(transform.position, wayPointTarget.position, moveSpeed * Time.deltaTime);
            // }
            if (GetComponentInChildren<HealthBar>().hp <= 0)
            {
                isDead = true;
                GetComponent<Collider2D>().enabled = false;
                // EventSystem.instance.CameraShakeEvent(0.2f);//MARKER OB PATTERN
                CinemachineShake.Instance.ShakeCamera(5f, 0.1f);
                gameObject.SetActive(false);
                return;
            }
            if (Vector2.Distance(transform.position, target.position) >= attackRange)
            {
                anim.SetBool("isAttacking", false);
            }
            else
            {
                anim.SetBool("isAttacking", true);
            }
            Attack();
        }
    }

    protected override void Attack()
    {
        base.Attack();
        if (Vector2.Distance(transform.position, target.position) >= attackRange)
        {
            anim.SetBool("isAttacking", false);
        }
        else
        {
            anim.SetBool("isAttacking", true);
        }
    }
    
}
