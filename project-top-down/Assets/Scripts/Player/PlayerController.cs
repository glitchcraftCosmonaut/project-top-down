using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{   
    public Rigidbody2D playerRigidbody;
    [SerializeField]private float moveSpeed;

    public GameObject healthBar;

    //combat
    private float attackTime = 0.25f;
    private float attackCounter = 0.25f;
    public bool isAttacking;

    //else
    public Animator animator;
    Vector2 movement;
    EnemyDamage enemyDamage;


    //sprite attacked
    private SpriteRenderer sp;
    [SerializeField] private Material hurtMat;
    private Material defaultMat;
    public bool isDead;

    // Start is called before the first frame update
    void Start()
    {
        enemyDamage = GetComponent<EnemyDamage>();
        sp = GetComponent<SpriteRenderer>();
        defaultMat = GetComponent<SpriteRenderer>().material;
        // healthBar = GetComponent<GameObject>();

    }

    // Update is called once per frame
    void Update()
    {
       
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal",movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

        if (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == -1 || Input.GetAxisRaw("Vertical") == 1)
        {
            animator.SetFloat("LastHorizontal", Input.GetAxisRaw("Horizontal"));
            animator.SetFloat("LastVertical", Input.GetAxisRaw("Vertical"));
        }

        if(isAttacking)
        {
            movement = Vector2.zero;
            attackCounter -= Time.deltaTime;
            if(attackCounter <= 0)
            {
                animator.SetBool("isAttacking", false);
                isAttacking = false;
            }
        }

        if(Input.GetMouseButtonDown(0))
        {
            attackCounter = attackTime;
            animator.SetBool("isAttacking",true);
            isAttacking = true;
        }
    }
    
    private void FixedUpdate() 
    {
        playerRigidbody.MovePosition(playerRigidbody.position + movement * moveSpeed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy Hit" && !isDead)
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
