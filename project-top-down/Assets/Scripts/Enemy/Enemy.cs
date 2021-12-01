using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{

    [SerializeField] private string enemyName;
    [SerializeField] protected private float moveSpeed;
    // private float healthPoint;
    // [SerializeField] private float maxHealthPoint;

    protected private Transform target;//The Target is our player
    [SerializeField] protected private float distance;
    [SerializeField]private SpriteRenderer sp;

    protected Animator anim;
    public AudioSource[] audioClip = null;

    // public Image hpImage;//Red Health Bar
    // public Image hpEffectImage;//White Health Bar Hurting Effect
    [SerializeField] private Material hurtMat;
    private Material defaultMat;
    // public float enemyPositionX, enemyPositionY;
    public bool isDead;
    public static Enemy instance;
    public static Enemy MyInstance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<Enemy>();
            }
            return instance;
        }
    }

    void Awake()
    {
        // if(instance == null)
        // {
        //     DontDestroyOnLoad(gameObject);
        //     instance = this;
        // }
        // else if(instance != this)
        // {
        //     Destroy(gameObject);
        // }
    }

    private void Start()
    {
        // healthPoint = maxHealthPoint;
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        sp = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        defaultMat = GetComponent<SpriteRenderer>().material;
    }

    private void Update()
    {
        if(CanDoActions()==false) return;
        TurnDirection();
        Attack();
        

        // DisplayHpBar();//MARKER REMOVE LATER if you have one Hurt Method (OPTIONAL)
    }

    private void FixedUpdate()
    {
        if(CanDoActions()==false) return;
        Move();
    }

    protected virtual void Move()
    {
        // if(CanDoActions()==false) return;
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

    protected virtual void Attack()
    {
        // Debug.Log(enemyName + " is Attacking");
    }

    protected virtual void Death()
    {
        // isDead = true;
        
    }

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
    public void EnemySound()
    {
        audioClip[0].Play();
    }
    protected virtual bool CanDoActions()
    {
        bool can = true;
        if(GameManager.instance.isPaused)
        {
            can = false;
        }
        /*if(FindObjectOfType<SceneControl>().canMove)
        {
            can = false;
        }*/
        
        return can;
    }
}
