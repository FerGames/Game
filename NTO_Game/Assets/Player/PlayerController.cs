using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    static bool isMoving(float x, float z, float prex, float prez)
    {
        if (Mathf.Abs(x) > Mathf.Abs(prex) || Mathf.Abs(z) > Mathf.Abs(prez) || Mathf.Abs(x) == 1 || Mathf.Abs(z) == 1)
            return true;
        return false;
    }
    private IEnumerator SwordOff()
    {
        yield return new WaitForSeconds(1.5f);
        sword.enabled = false;
    }

    public CharacterController Player;
    public Rigidbody rigidBody;
    public Camera Camera;

    public Animator Animator;
    public float kd = 0;
    public float maxKd = 90;

    public float dodgeTime;
    public bool isDodging;

    private float gravity = -9.81f;
    private Vector3 velocity;

    public float speed = 12f;
    public float jumpPower = 200f;
    public bool isOnGround;

    public Transform groundChecker;
    public LayerMask Ground;
    public float groundDistance = 0.4f;
    public float secondAttackTimer;
    public float thirdAttackTimer;

    public bool isSecondAttacking;
    public bool isThirdAttacking;
    private IEnumerator Die()
    {
        yield return new WaitForSeconds(5);
        gameOver.GetComponent<TMPro.TextMeshProUGUI>().text = "GAME OVER";
    }
    public float secondAttackTimerBase = 12;
    public float thirdAttackTimerBase = 12;

    public int jumpCounter;

    private void GetInput()
    {
        if (Input.GetKey(KeyCode.Space) && isOnGround)
        {
            rigidBody.AddForce(transform.up * jumpPower);
        }
    }


    void Start()
    {
        Animator = GetComponent<Animator>();
    }
    Vector3 playerRotation;
    float x = 0, z = 0;
    float angle;
    public Collider sword;
    public int damage;
    public GameObject hpText;
    public GameObject gameOver;
    void Attack(int damage)
    {
        kd = 0;
    }
    void Update()
    {
        if (gameObject.GetComponent<Player>().hp <= 0)
        {
            gameObject.GetComponent<Player>().hp = 0;
            Animator.Play("????????|death");
            StartCoroutine(Die());
        }
        if (isSecondAttacking)
        {
            secondAttackTimer--;
            if(secondAttackTimer <= 0)
            {
                secondAttackTimer = secondAttackTimerBase;
                isSecondAttacking = false;
                isThirdAttacking = false;
            }
        }
        if (isThirdAttacking)
        {
            thirdAttackTimer--;
            if (thirdAttackTimer <= 0)
            {
                thirdAttackTimer = thirdAttackTimerBase;
                isSecondAttacking = false;
                isThirdAttacking = false;
            }

        }
        hpText.GetComponent<TMPro.TextMeshProUGUI>().text = gameObject.GetComponent<Player>().hp + "";
        if (kd < maxKd)
            kd++;
        if (Input.GetButtonDown("Fire1") && isThirdAttacking)
        {
            if (maxKd <= kd)
            {
                sword.enabled = true;
                Animator.SetBool("IsWalking", false);
                Animator.Play("????????|Attack3");
                Attack(damage);
                StartCoroutine(SwordOff());
            }
        }
        else if (Input.GetButtonDown("Fire1") && isSecondAttacking)
        {
            if (maxKd <= kd)
            {
                sword.enabled = true;
                Animator.SetBool("IsWalking", false);
                Animator.Play("????????|Attack22");
                Attack(damage);
                StartCoroutine(SwordOff());
                isThirdAttacking = true;
            }
        }
        else if (Input.GetButtonDown("Fire1"))
        {
            if (maxKd <= kd)
            {
                sword.enabled = true;
                Animator.SetBool("IsWalking", false);
                Animator.Play("????????|Attack1");
                Attack(damage);
                StartCoroutine(SwordOff());
                isSecondAttacking = true;
            }
        }
        
        isOnGround = Physics.CheckSphere(groundChecker.position, groundDistance, Ground);
        if (isOnGround && velocity.y < 0)
            velocity.y = -2f;
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(x, 0, z);
        if (direction.magnitude > Mathf.Abs(0.1f))
        {
            Player.transform.GetChild(1).rotation = Quaternion.Lerp(Player.transform.GetChild(1).rotation, Quaternion.LookRotation(direction), Time.deltaTime * 10);
            Animator.SetBool("IsWalking", true);
        }
        else
            Animator.SetBool("IsWalking", false);
        rigidBody.velocity = Vector3.ClampMagnitude(direction, 1) * speed;
        /*
        if (isMoving(x, z, prex, prez))
        {
           
            Vector3 move = transform.right * x + transform.forward * z;
            Player.Move(move * speed * Time.deltaTime / 2.5f);

            Vector3 playerMovement = new Vector3(x * 180, 0, z * 180);
            Vector3 playerDirection = playerMovement - transform.position;
            angle = Mathf.Atan2(playerDirection.x, playerDirection.z) * Mathf.Rad2Deg;
            Player.transform.GetChild(1).rotation = Quaternion.Euler(0, angle, 0);
        }
        
        */
        if (Input.GetKeyDown(KeyCode.Z) && direction.magnitude > Mathf.Abs(0.1f))
        {
            Vector3 move = transform.right * x + transform.forward * z;
            Player.Move(move * speed * 20 * Time.deltaTime / 2.5f);
            dodgeTime = 1;
            isDodging = true;
        }/*

        if (Input.GetButtonDown("Jump") && isOnGround)
        {
            velocity.y = Mathf.Sqrt(jumpPower * -2f * gravity);
            jumpCounter++;
        }
        */
        if (dodgeTime > 0)
            dodgeTime -= Time.deltaTime;
        else
        {
            dodgeTime = 0;
            isDodging = false;
        }
        /*
        velocity.y += gravity * Time.deltaTime;

        Player.Move(velocity * Time.deltaTime);
        prex = x;
        prez = z;
        */
    }
}

