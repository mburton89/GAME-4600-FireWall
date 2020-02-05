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

    //Collider for the hitbox
    [SerializeField]
    GameObject attackHitBox;

    //bool jump = false;

    //****************************************************************** Start function ******************************************************************

    void Start()
    {
        attackHitBox.SetActive(false);
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
}

//****************************************************************** END OF CODE ******************************************************************
