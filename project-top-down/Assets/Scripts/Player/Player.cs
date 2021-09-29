using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{   
    public Rigidbody2D playerRigidbody;
    [SerializeField]private float moveSpeed;
    public GameObject interactIcon;
    
    public GameObject healthBar;

    //combat
    private float attackTime = 0.25f;
    private float attackCounter = 0.25f;
    public bool isAttacking;

    //else
    public Animator animator;
    Vector2 movement;
    EnemyDamage enemyDamage;
    public Vector2 boxSize = new Vector2(0.1f,1f);
    public AudioSource[] audioClip;
    [HideInInspector]
    public int currentSceneIndex;


    //sprite attacked
    private SpriteRenderer sp;
    [SerializeField] private Material hurtMat;
    private Material defaultMat;
    private BoxCollider2D boxCollider2D;
    public bool isDead;
    public string scenePassword;
    public static Player instance;
    public static Player MyInstance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<Player>();
            }
            return instance;
        }
    }

    void Awake()
    {
        if(instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }
        else if(scenePassword == "MainMenu")
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        enemyDamage = GetComponent<EnemyDamage>();
        sp = GetComponent<SpriteRenderer>();
        defaultMat = GetComponent<SpriteRenderer>().material;
        boxCollider2D = GetComponent<BoxCollider2D>();
        
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        // audioClip = GetComponent<AudioSource>();
        // healthBar = GetComponent<GameObject>();

    }

    // Update is called once per frame
    void Update()
    {
       if(CanDoActions()==false) return;
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
        if(Input.GetKeyDown(KeyCode.E))
        {   
            CheckInteraction();
        }

        if(Input.GetMouseButtonDown(0))
        {
            attackCounter = attackTime;
            animator.SetBool("isAttacking",true);
            isAttacking = true;
        }
        if (GetComponentInChildren<HealthBar>().hp <= 0)
        {
            isDead = true;
            GameManager.instance.isGameOver = true;
            GetComponent<Collider2D>().enabled = false;
            EventSystem.instance.CameraShakeEvent(0.2f);//MARKER OB PATTERN
            // FindObjectOfType<GameManager>().EndGame();
            Destroy(gameObject);
            return;
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
        audioClip[2].Play();
        sp.material = defaultMat;
    }

    public void Footsteps()
    {
        audioClip[0].Play();
    }
    public void PlayerSlash()
    {
        audioClip[1].Play();
    }

    public void OpenInteractableIcon()
    {
        interactIcon.SetActive(true);
    }

    public void CloseInteractableIcon()
    {
        interactIcon.SetActive(false);
    }
    public bool CanDoActions()
    {
        bool can = true;
        // if(FindObjectOfType<InventorySystem>().isOpen)
        // {
        //     can = false;
        // }
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

    private void CheckInteraction()
    {
        RaycastHit2D[] hits = Physics2D.BoxCastAll(transform.position, boxSize, 0, Vector2.zero);

        if(hits.Length > 0)
        {
            foreach(RaycastHit2D rc in hits)
            {
                if(rc.transform.GetComponent<InteractionSystem>())
                {
                    rc.transform.GetComponent<InteractionSystem>().Interact();
                    return;
                }
            }
        }
    }
    
    
}
