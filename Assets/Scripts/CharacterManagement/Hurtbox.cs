using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurtbox : MonoBehaviour {

    public float damageResistancy = 0;
    public float FallDownLimit;
    public Rigidbody2D rb;
    public Health hp;
    public Character character;
    float v = 0.001f;
    Vector3 force;

    int knockbackFrame;
    public


    void Start()
    {
    }

    void Update()
    {
        v = -v;
        transform.position += transform.right * v;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Hitbox" && MyClass.OldestParent(other.gameObject) != MyClass.OldestParent(gameObject))
        {

            if (Time.frameCount > knockbackFrame + 10)
            {
                Hitbox hb = other.gameObject.GetComponent<Hitbox>();
                character.ApplyDamage(hb.damage);

                

                GameObject enemy = MyClass.OldestParent(other.gameObject);
                float dir = Mathf.Sign(enemy.transform.position.x - transform.position.x);
                force = (-Vector3.right * hb.horizontalKnockback * dir) + (Vector3.up * other.gameObject.GetComponent<Hitbox>().verticalKnockback);

                if (hb.horizontalKnockback >= FallDownLimit || hb.verticalKnockback > FallDownLimit) { character.FallDown(); print("Fall Down"); }
                else if (hb.damage > damageResistancy){ character.ApplyHitStun(); print("Hit Stun");
            }

            character.ApplyForce(force);
                knockbackFrame = Time.frameCount;
            }
        }

    }


}
