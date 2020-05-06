using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//V3PlayerCharacterController is a script that gives player input functionality to a Player object. This allows for control of movement in tandem with a CharacterController2D script
public class V3PlayerCharacterControler : MonoBehaviour
{
    //****************************************************************** Variable Declaration ******************************************************************
    //Associated CharacterController2D object
    public CharacterController2D controller;

    //Run speed for horizontal movement
    public float runSpeed = 40f;
    float horizontalMove = 0f;

    public CharacterAnimator characterAnimator;

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

    public PlayerSoundManager soundManager;

    //List of GameObjects to serve as a player "inventory" for holding items.
    public static List<GameObject> Inventory = new List<GameObject>();

    private bool _canTakeDamage;

    public Weapon weapon;

    [SerializeField] private GameObject _vladSplosion;

    private bool _hasSploded;

    //****************************************************************** Start function ******************************************************************

    void Start()
    {
        //This makes the hitbox for melee attacks inactive
        attackHitBox.SetActive(false);
        playerTempHealth = playerMaxHealth;
        _canTakeDamage = true;
        _hasSploded = false;
    }

    //****************************************************************** Update function ******************************************************************

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        characterAnimator.Animate(controller.getGrounded(), horizontalMove);
        controller.Move(horizontalMove * Time.fixedDeltaTime, false /*, jump*/);

        /*if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }*/

        if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Keypad1)) && !isAttacking) //Unity has their own stuff for mouse buttons too, might be good for eventual control customization
        {
            //Sets the attacking bool true, locking into a single attack at a time and preventing other animations
            isAttacking = true;

            //Calls DoAttack, which will set the hitbox collider active for a duration of time
            StartCoroutine(DoAttack());

            characterAnimator.PlayMeleeAnimation();

            soundManager.PlayJumpSound();
        }


    }

    //****************************************************************** DoAttack IEnumerator ******************************************************************

    IEnumerator DoAttack()
    {
        weapon.Hide();
        yield return new WaitForSeconds(.1f);
        attackHitBox.SetActive(true);
        yield return new WaitForSeconds(.3f); //CHANGE THIS TO TIMING OF ANIMATION
        attackHitBox.SetActive(false);
        isAttacking = false;
        yield return new WaitForSeconds(.1f);
        weapon.Show();
    }

    public void ApplyDamage(float damageAmount)
    {
        if (_canTakeDamage)
        {
            playerTempHealth -= damageAmount;

            if (HealthBar.Instance != null)
            {
                HealthBar.Instance.UpdateHealthBar(playerTempHealth / playerMaxHealth);
            }

            soundManager.PlayHurtSound();

            StartCoroutine(FlashRed());

            //Player Health check, resets player to start of level
            if (playerTempHealth <= 0 && !_hasSploded)
            {
                _hasSploded = true;
                weapon.Hide();
                runSpeed = 0;
                GameObject splosion = Instantiate(_vladSplosion, new Vector3(transform.position.x, transform.position.y + 0.5f), transform.rotation);
                characterAnimator.transform.localScale = Vector3.zero;
                SceneMover.Instance.RestartScene();
            }
        }
    }

    public void AddHealth(float healthAmount)
    {
        playerTempHealth += healthAmount;

        if (playerTempHealth > playerMaxHealth)
        {
            playerTempHealth = playerMaxHealth;
        }

        if (HealthBar.Instance != null)
        {
            //HealthBar.Instance.IncreaseHealth(healthAmount / playerMaxHealth);
            HealthBar.Instance.UpdateHealthBar(playerTempHealth / playerMaxHealth);

        }
    }

    private IEnumerator FlashRed()
    {
        _canTakeDamage = false;
        SpriteRenderer spriteRenderer = characterAnimator.GetComponent<SpriteRenderer>();
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(.06f);
        spriteRenderer.color = Color.white;
        yield return new WaitForSeconds(.06f);
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(.06f);
        spriteRenderer.color = Color.white;
        yield return new WaitForSeconds(.06f);
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(.06f);
        spriteRenderer.color = Color.white;
        _canTakeDamage = true;
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

        //if (collision.gameObject.tag == "EnemyBulletHit")
        //{
        //    Debug.Log("You've been hit");
        //    Bullet_Enemy temp = collision.gameObject.GetComponentInParent<Bullet_Enemy>();
        //    float tempDamage = temp.damage;
        //    ApplyDamage(tempDamage);
        //}
    }
}

//****************************************************************** END OF CODE ******************************************************************
