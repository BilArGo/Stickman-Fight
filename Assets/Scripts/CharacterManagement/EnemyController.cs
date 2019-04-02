using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyController : Character
{
    public EnemyInfo info = new EnemyInfo();

    public float delayBeforeHit;
    public float delayBetweenHits;
    public float delayAfterFall;
    public float neededAttackDis;
    public Vector2 targetLocation;
    Vector2 nullVector = new Vector2(1000,1000);
    public enum EnemyState { Attack, Waiting, Moving }
    public EnemyState state;

    public float attackRange;
    public float movingRange;

    //Some handy variables
    float playerDis; // Distance
    bool pointing; // Is the enemy pointing at the player
    float targetDis; // difference between enemy and player
    Vector3 pP; // player position
    Vector3 eP; // enemy position
    float hDir; // Horizontal direction to the target location
    public bool nearTarget; // Is it close enough to attack

    GameObject player;

    public float targetDir;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        state = EnemyState.Attack;
        EnemyManager en = new EnemyManager();
    }

    private void Update()
    {
        {
            pP = player.transform.position;

            eP = transform.position;

            playerDis = Mathf.Abs(eP.x - pP.x);

            if(targetLocation != nullVector)targetDis = Mathf.Abs(targetLocation.x - eP.x);

            if (Mathf.Sign(transform.localScale.x) == Mathf.Sign(targetDis)) pointing = true;
            else pointing = false;

            if (targetLocation != nullVector) targetDir = Mathf.Sign(targetLocation.x - eP.x);
            else targetDir = 0;

            info.controller = this;
            info.health = hp.curHealth;
            info.hitstun = hs;
            info.distance = playerDis;
        } //handy variable declarations...
        Move(targetDir, speed);

        if (state == EnemyState.Attack)
            AttackMode();
        if (state == EnemyState.Waiting)
            WaitingMode();
        if (state == EnemyState.Moving)
            MovingMode(new Vector2 (10.0f, 2.5f));

        if (anim.CheckCurState("Stickman StandUp 1", 0))
        {
            state = EnemyState.Waiting;
             Invoke("StateChange",Random.Range(0.5f,3.0f));
        }

    }


    void AttackMode()
    {
        if (playerDis <= attackRange)
        {
            nearTarget = true;
            targetLocation = nullVector;
        }
        else
        {
            nearTarget = false;
            targetLocation = pP;
        }
        if (nearTarget && !hs)
            if(!IsInvoking("Attack"))
                Invoke("Attack",delayBetweenHits);
    }

    void Attack()
    {
        DeclareAttack(11);
    }

    void WaitingMode()
    {
        targetLocation = nullVector;
    }

    void MovingMode(Vector2 location)
    {
        if (targetDis <= movingRange)
        {
            nearTarget = true;
            targetLocation = nullVector;
        }
        else
        {
            nearTarget = false;
            targetLocation = location;
        }
    }

    void StateChange()
    {
        state = EnemyState.Attack;
    }



}
