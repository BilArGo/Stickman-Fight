using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Character
{

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Move(h, speed);

        if (v == 1 && a)
        {
            Jump(jumpForce);
        }

        if(v == 0) a = true;
        if (Input.GetKeyDown(KeyCode.T)) DeclareAttack(11);
        if (Input.GetKeyDown(KeyCode.T) && v == -1) DeclareAttack(12);
        if (Input.GetKeyDown(KeyCode.Y)) DeclareAttack(21);
        if (Input.GetKeyDown(KeyCode.Y) && v == -1) DeclareAttack(22);
    }
}
