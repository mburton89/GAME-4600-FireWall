using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//V3PlayerCharacterController is a script that gives player input functionality to a Player object. This allows for control of movement in tandem with a CharacterController2D script
public class V3PlayerCharacterControler : MonoBehaviour
{
    //****************************************************************** Variable Declaration ******************************************************************
    //Associated CharacterController2D object
    public CharacterController2D controller;

    //Run speed for horizontal movement
    public float runSpeed = 40f;
    float horizontalMove = 0f;
    bool isAttacking = false;

    //Player health variables
    [SerializeField]
    public float playerMaxHealth = 20;
    public float playerTempHealth = 0;

    //Collider for the hitbox
    [SerializeField]
    GameObject attackHitBox;
 
    //Float for the damage value produced by melee attacls
    [SerializeField]
    public float meleeDamageValue = 0;

    [SerializeField]
    GameObject playerRadius;

    //bool jump = false;

    //****************************************************************** Start function ******************************************************************

    void Start()
    {
        //This makes the hitbox for melee attacks inactive
        attackHitBox.SetActive(false);
        playerTempHealth = playerMaxHealth;
    }

    //****************************************************************** Update function ******************************************************************

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        /*if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }*/

        if (Input.GetMouseButtonDown(0) && !isAttacking) //Unity has their own stuff for mouse buttons too, might be good for eventual control customization
        {
            //Sets the attacking bool true, locking into a single attack at a time and preventing other animations
            isAttacking = true;

            //Calls DoAttack, which will set the hitbox collider active for a duration of time
            StartCoroutine(DoAttack());
        }

    }

    //****************************************************************** FixedUpdate function ******************************************************************

    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, false /*, jump*/);
       //jump = false;
    }

    //****************************************************************** DoAttack IEnumerator ******************************************************************

    IEnumerator DoAttack()
    {
        attackHitBox.SetActive(true);
        yield return new WaitForSeconds(.2f); //CHANGE THIS TO TIMING OF ANIMATION
        attackHitBox.SetActive(false);
        isAttacking = false;
    }

    public void ApplyDamage(float damage)
    {
        //Actual decrement of health. Can be changed as development continues.
        playerTempHealth -= damage;
    }

    //****************************************************************** COLLISION DETECTION ******************************************************************

    //****************************************************************** OnCollisionEnter2D function ******************************************************************
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "EnemyHit")
        {
            Debug.Log("You've been hit");
            EnemyController temp = collision.gameObject.GetComponentInParent<EnemyController>();
            float tempDamage = temp.enemyMeleeDamageOutput;
            ApplyDamage(tempDamage);
        }
    }

}

//****************************************************************** END OF CODE ******************************************************************
