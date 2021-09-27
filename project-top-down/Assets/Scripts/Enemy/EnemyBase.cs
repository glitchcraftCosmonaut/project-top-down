using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBase : MonoBehaviour
{

    [SerializeField] private string enemyName;
    [SerializeField] protected private float moveSpeed;
    // private float healthPoint;
    // [SerializeField] private float maxHealthPoint;

    protected private Transform target;//The Target is our player
    [SerializeField] protected private float distance;
    private SpriteRenderer sp;

    protected Animator anim;

    // public Image hpImage;//Red Health Bar
    // public Image hpEffectImage;//White Health Bar Hurting Effect
    [SerializeField] private Material hurtMat;
    private Material defaultMat;
    public bool isDead;

    private void Start()
    {
        // healthPoint = maxHealthPoint;
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        sp = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        defaultMat = GetComponent<SpriteRenderer>().material;

        Introduction();
    }

    private void Update()
    {
        TurnDirection();

        // if(healthPoint <= 0)
        // {
        //     Death();
        // }

        // Attack();
        

        // DisplayHpBar();//MARKER REMOVE LATER if you have one Hurt Method (OPTIONAL)
    }

    private void FixedUpdate()
    {
        Move();
    }

    protected virtual void Introduction()
    {
        // Debug.Log("My Name is " + enemyName + ", HP: " + healthPoint + ", moveSpeed: " + moveSpeed);
    }

    protected virtual void Move()
    {
        if(Vector2.Distance(transform.position, target.position) < distance)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
        }
    }

    private void TurnDirection()
    {
        if(transform.position.x > target.position.x)
        {
            sp.flipX = true;
        }
        else
        {
            sp.flipX = false;
        }
    }

    // protected virtual void Attack()
    // {
    //     Debug.Log(enemyName + " is Attacking");
    // }

    protected virtual void Death()
    {
        Destroy(gameObject);
    }

    // protected virtual void DisplayHpBar()
    // {
    //     hpImage.fillAmount = healthPoint / maxHealthPoint;

    //     if(hpEffectImage.fillAmount > hpImage.fillAmount)
    //     {
    //         hpEffectImage.fillAmount -= 0.005f;//Delay Effect
    //     }
    //     else
    //     {
    //         hpEffectImage.fillAmount = hpImage.fillAmount;//STOP continue Decreasing 
    //     }
    // }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Weapon" && !isDead)
        {
            StartCoroutine(HurtEffect());
        }
    }

    IEnumerator HurtEffect()
    {
        sp.material = hurtMat;
        yield return new WaitForSeconds(0.2f);
        sp.material = defaultMat;
    }

}
