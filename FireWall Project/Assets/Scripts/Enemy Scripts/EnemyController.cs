using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//EnemyController is a test script that allows for AI control of an enemy object
//Current behaviors include: patrolling (with environment collisions), player targeting
public class EnemyController : MonoBehaviour
{
    //****************************************************************** Variable Declaration ******************************************************************
    //public GameObject Player;
    public Transform target;

    //for testing hits
    public SpriteRenderer sprite;

    public CharacterController2D enemyController;
    public float enemyRunSpeed = 30f;
    public float enemyChaseSpeed = 5f;
    public float horizontalMove = 1f;

    private float changeDirection = 1f;
    public float enemyDistance = 5f;

    private Vector3 setEnemyDistance;

    bool playerFound = false;
    bool isAttacking = false;
       
    private bool groundedCheck = true;

    //Floats for Enemy Health. enemyBaseHealth is the maximum health for the entity. enemyTempHealth is the current health.
    [SerializeField]
    public float enemyBaseHealth = 10;
    public float enemyTempHealth = 0;

    [SerializeField]
    public float enemyMeleeDamageOutput = 2;

    //Enemy melee attack hitbox
    [SerializeField]
    GameObject attackHitBox;

    //****************************************************************** Start function ******************************************************************
    void Start()
    {
        //horizontalMove *= enemyRunSpeed;
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        setEnemyDistance = new Vector3(enemyDistance, 0, 0);

        attackHitBox.SetActive(false);

        enemyTempHealth = enemyBaseHealth;
    }

    //****************************************************************** Update function ******************************************************************
    // Update is called once per frame
    void Update()
    {
        if (playerFound)
        {
            //When the playerFound bool is true, the entity will track to the position of the player
            transform.position = Vector2.MoveTowards(transform.position, (target.position + setEnemyDistance), enemyChaseSpeed * Time.deltaTime); //essentially make the target vector3 the target.position - buffer
        }

        if(enemyTempHealth <= 0)
        {
            Debug.Log("Destroyed!");
            Destroy(this.gameObject);
        }
    }

    //****************************************************************** FixedUpdate function ******************************************************************
    void FixedUpdate()
    {
        if (!playerFound)
        {
            horizontalMove = horizontalMove * enemyRunSpeed;
            //Debug.Log(horizontalMove);

            groundedCheck = enemyController.getGrounded();
            //Debug.Log(groundedCheck + " " + horizontalMove);
            if (groundedCheck)
            {
                enemyController.Move(horizontalMove * Time.fixedDeltaTime, false);
            }

            horizontalMove = changeDirection;
        }
    }

    public void ApplyDamage(float damage)
    {
        //Actual decrement of health. Can be changed as development continues.
        enemyTempHealth -= damage;
    }

    IEnumerator DoAttack()
    {
        attackHitBox.SetActive(true);
        yield return new WaitForSeconds(.2f); //CHANGE THIS TO TIMING OF ANIMATION
        attackHitBox.SetActive(false);
        isAttacking = false;
    }

    IEnumerator FlashColor()
    {
        sprite.color = new Color(0, 1, 0, 1);
        yield return new WaitForSeconds(.1f);
        sprite.color = new Color(1, 0, 0, 1);
    }

    //****************************************************************** COLLISION DETECTION ******************************************************************

    //****************************************************************** OnCollisionEnter2D function ******************************************************************
    //This function defines behavior once another entity with a collider enters a collision with this entity's colliders
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Collision must be with a collider tagged as Environment
        if (collision.gameObject.tag == "Environment")
        {
            //This reverses the direction of the entity
            changeDirection *= -1f;
        }
    }

    //****************************************************************** OnTriggerEnter2D function ******************************************************************
    //This function defines behavior once another entity with a collider enters an IsTrigger collision with this entity's colliders
    //Our radius for the enemy is an IsTrigger collision, meaning it will begin targeting the player when the radius is touched
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Collision must be with the player's collider
        if (collision.gameObject.tag == "Player")
        {
            playerFound = true;
            //Debug.Log("player found");
        }

        if(collision.gameObject.tag == "PlayerHit")
        {
            Debug.Log("Hit");
            //hit testing
            StartCoroutine(FlashColor());
            V3PlayerCharacterControler temp = collision.gameObject.GetComponentInParent<V3PlayerCharacterControler>();
            float tempDamage = temp.meleeDamageValue;
            ApplyDamage(tempDamage);
            
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "PlayerRadius")
        {
            Debug.Log("Enemy is attacking!");
            isAttacking = true;
            StartCoroutine(DoAttack());
        }
    }

    //****************************************************************** OnTriggerExit2D function ******************************************************************
    //This function defines behavior once another entity with a collider exits an IsTrigger collision with this entity's colliders
    //Our radius for the enemy is an IsTrigger collision, meaning it will stop targeting the player when the radius is exited
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerFound = false;
        }
    }
}

//****************************************************************** END OF CODE ******************************************************************
