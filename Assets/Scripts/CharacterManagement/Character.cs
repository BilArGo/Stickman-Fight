using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{


    public float jumpForce;
    public float speed;
    public static float speedMultiplier = 1;
    public bool onGround;
    public int jumpTime = 2;
    public bool a = true;
    public bool hs; // Can be use to detect Hitstun.


    public float comboCount;
    public float comboTimeLimit;
    public string urgeToPlay;
    float lastComboTime;
    public enum Skill { Punch, Kick }
    public Skill curSkill = Skill.Punch;
    public Animator anim;

    public Rigidbody2D rb;
    public Health hp;

    public void SetComponents()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    public void Start()
    {
        SetComponents();
    }

    void FixedUpdate()
    {
        if (anim.CheckCurState("Idle", 2) && urgeToPlay != null && !hs)
        {
            ChangeAttackState(urgeToPlay);
            urgeToPlay = null;
            if (comboCount != 3) anim.SetBool("Idle Lock", true);

        }
        else if (hs) anim.SetBool("Idle Lock", false);

        if (anim.CheckCurState("Stickman Stun 1", 0) || anim.CheckCurState("Stickman FallDown 1", 0))
        {
            hs = true;
        }
        else
            hs = false;
    }

    public void Move(float dir, float speed)
    {
        if(!hs) rb.position += Vector2.right * speed * dir * Time.deltaTime;

        if (dir != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(dir), transform.localScale.y, transform.localScale.z);
        }

        if (dir != 0) ChangeMovementState("Stickman Run");
        else ChangeMovementState("Idle");
    }

    public void Jump(float jumpForce)
    {
        //Invoke("JumpReset",0.1f);
        if (a && jumpTime != 0)
        {
            rb.velocity = (Vector2.up * jumpForce);
            jumpTime--;
            print("123");
            onGround = false;
            a = false;

        }
    }

    public void DecreaseHealth(GameObject target, float amount)
    {
        Health h = target.GetComponent<Health>();
        h.curHealth -= amount;
    }

    public void DeclareAttack(int attackType)
    {
        //if (anim.CheckCurState("Idle", 2))
        {

            if (attackType == 11)
            {
                comboCount++;
                if (comboCount >= 4 || !IsInvoking("GoToIdle")) comboCount = 1;
                if (IsInvoking("GoToIdle"))
                {
                    CancelInvoke("GoToIdle");
                    GoToIdle();
                }
               
                urgeToPlay = "Stickman " + curSkill.ToString() + " 1-1-" + comboCount;
                Invoke("GoToIdle", 0.5f);
                if (comboCount == 3)
                {
                    CancelInvoke("GoToIdle");
                    GoToIdle();
                }

            }

            if (attackType == 12)
            {
                GoToIdle();
                urgeToPlay = "Stickman " + curSkill.ToString() + " 1-2";
            }

            if (attackType == 21)
            {
                GoToIdle();
                urgeToPlay = "Stickman " + curSkill.ToString() + " 2-1";
            }

            if (attackType == 22)
            {
                GoToIdle();
                urgeToPlay = "Stickman " + curSkill.ToString() + " 2-2";
            }
            lastComboTime = Time.time;
        }
            
    }

    void GoToIdle()
    {
        anim.SetBool("Idle Lock", false);
    }

    void ChangeAttackState(string attackState)
    {
        anim.Play(attackState, 2);
    }

    void ChangeMovementState(string movementState)
    {
        if (!hs) anim.Play(movementState, 1);
        else anim.Play("Idle", 1);
    }

    public void ApplyHitStun()
    {
        anim.Play("Stickman Stun 1", 0);
        anim.Play("Stickman Stun 1", 1);
        anim.Play("Stickman Stun 1", 2);
    }

    public void ApplyDamage(float amount)
    {
        hp.curHealth -= amount;
    }

    public void ApplyForce(Vector3 dir)
    {
        rb.AddForce(dir * Time.deltaTime * 60);
    }

    public void FallDown()
    {
        anim.Play("Stickman FallDown 1",0);
    }

    private void JumpReset()
    {
        a = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            jumpTime = 2;
            onGround = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            onGround = false;
        }
    }
}
