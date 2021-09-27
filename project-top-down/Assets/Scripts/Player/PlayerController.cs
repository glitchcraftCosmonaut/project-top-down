using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{   
    public Rigidbody2D playerRigidbody;
    [SerializeField]private float moveSpeed;
    private float attackTime = 0.25f;
    private float attackCounter = 0.25f;
    private bool isAttacking;
    public Animator animator;
    Vector2 movement;

    // Start is called before the first frame update
    void Start()
    {
        
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
    
}
