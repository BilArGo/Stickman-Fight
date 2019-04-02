using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    List<EnemyInfo> enemies = new List<EnemyInfo>();
    List<EnemyController> attacker = new List<EnemyController>();
    public delegate void StateChange();
    public static event StateChange NewState;

    public int maxAttacker;

    void Start()
    {
        //int i = NewState.GetInvocationList().Length;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            foreach (EnemyInfo info in enemies)
            {
                print(info.distance + ", " + info.health + ", " + info.hitstun + ".");
            }
        }


    }

    void InfoUpdate()
    {
        EnemyController[] eController = GameObject.FindObjectsOfType<EnemyController>();
        enemies.Clear();
        foreach (EnemyController ec in eController)
        {
            if (ec.hp.alive) enemies.Add(ec.info);
        }
    }

    void SetAttacker()
    {
        foreach (EnemyInfo info in enemies)
        {

        }
    }
}
