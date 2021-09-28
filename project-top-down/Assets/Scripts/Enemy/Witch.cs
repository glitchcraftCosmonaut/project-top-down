
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Witch : Enemy
{
    private float moveRate = 2.0f;
    private float moveTimer;

    private float shotRate = 2.1f;
    private float shotTimer;
    public GameObject projectile;
    [SerializeField] private float attackRange;

    [SerializeField] private float minX, maxX, minY, maxY;
    [HideInInspector]
    public float witchPositionX, witchPositionY;

    protected override void Introduction()
    {
        base.Introduction();
    }

    protected override void Move()
    {
        witchPositionX = transform.position.x;
        witchPositionY = transform.position.y;
        //base.Move();//MARKER Give up the base Move Function!!
        RandomMove();
        if (GetComponentInChildren<HealthBar>().hp <= 0)
            {
                isDead = true;
                GetComponent<Collider2D>().enabled = false;
                EventSystem.instance.CameraShakeEvent(0.2f);//MARKER OB PATTERN
                Destroy(gameObject);
                return;
            }
    }

    private void RandomMove()
    {
        moveTimer += Time.deltaTime;

        if(moveTimer > moveRate)
        {
            transform.position = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0);
            moveTimer = 0;
        }
    }

    protected override void Attack()
    {
        base.Attack();

        shotTimer += Time.deltaTime;

        if(shotTimer > shotRate && Vector2.Distance(transform.position, target.position) >= attackRange)
        {
            audioClip[1].Play();
            Instantiate(projectile, transform.position, Quaternion.identity);
            shotTimer = 0;
        }
    }

}
