using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OwlBear : Enemy
{
    public Transform wayPoint01, wayPoint02;
    private Transform wayPointTarget;
    [SerializeField] private float attackRange;
    [HideInInspector]
    public float owlPositionX, owlPositionY;
    private void Awake()
    {
        wayPointTarget = wayPoint01;//At the beginning, bat move to the right waypoint
    }
    protected override void Move()
    {
        if(CanDoActions()==false) return;
        base.Move();

        owlPositionX = transform.position.x;
        owlPositionY = transform.position.y;

        if(!isDead)
        {
        //MARKER Override Part
            if(Vector2.Distance(transform.position, target.position) > distance)
            {
                //When we reached at the waypoint01, we have to mvoe to the waypoint 02
                if(Vector2.Distance(transform.position, wayPoint01.position) < 0.01f)
                {
                    wayPointTarget = wayPoint02;
                }

                if(Vector2.Distance(transform.position, wayPoint02.position) < 0.01f)
                {
                    wayPointTarget = wayPoint01;
                }

                transform.position = Vector2.MoveTowards(transform.position, wayPointTarget.position, moveSpeed * Time.deltaTime);
                anim.SetBool("isWalking", true);
            }
            if (GetComponentInChildren<HealthBar>().hp <= 0)
            {
                isDead = true;
                GetComponent<Collider2D>().enabled = false;
                // EventSystem.instance.CameraShakeEvent(0.2f);//MARKER OB PATTERN
                CinemachineShake.Instance.ShakeCamera(5f, 0.1f);
                Death();
                // Destroy(gameObject);
                return;
            }
            if (Vector2.Distance(transform.position, target.position) >= attackRange)
            {
                anim.SetBool("isAttacking", false);
                anim.SetBool("isWalking", true);
            }
            else
            {
                anim.SetBool("isAttacking", true);
                anim.SetBool("isWalking", false);
            }
            Attack();
        }
    }

    protected override void Attack()
    {
        base.Attack();
        if(CanDoActions()==false) return;

        if(!isDead)
        {
            if (Vector2.Distance(transform.position, target.position) >= attackRange)
            {
                anim.SetBool("isAttacking", false);
                anim.SetBool("isWalking", true);
            }
            else
            {
                anim.SetBool("isAttacking", true);
                anim.SetBool("isWalking", false);
            }
        }
        
    }
    protected override void Death()
    {
        // isDead = true;
        if(isDead)
        {
            anim.Play("OwlBear_Death");
            // anim.SetBool("isDeath",true);
        }
    }
    protected override bool CanDoActions()
    {
        bool can = true;
        if(isDead)
        {
            can = false;
            anim.Play("OwlBear_Death");
            audioClip[0].Stop();

        }
        /*if(FindObjectOfType<SceneControl>().canMove)
        {
            can = false;
        }*/
        
        return can;
    }
    public void HealthFalse()
    {
        GetComponentInChildren<Canvas>().gameObject.SetActive(false);
    }
    
}
