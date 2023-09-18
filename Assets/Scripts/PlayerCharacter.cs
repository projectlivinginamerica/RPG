
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Runtime.CompilerServices;

public class PlayerCharacter : BaseCharacter
{
    // static public members
    public static PlayerCharacter instance;

    public Weapon BaseAttack;

    private float startAttackTime;

    void Awake()
    {
        instance = this;

        //velocity = rb.velocity;
        this.LoadSpriteSheet();
        animator.SetFloat("speed", 0);
        animator.SetInteger("orientation", 4);
    }

    void Update()
    {
        if (CharacterState == eCharacterState.Idle)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                CharacterState = eCharacterState.Attacking;
                Weapon newSpell = Instantiate(BaseAttack, transform.position, transform.rotation) as Weapon;
                if (BaseAttack.Speed > 0)
                {
                    Rigidbody rb = newSpell.gameObject.GetComponent<Rigidbody>();
                    if (rb != null)
                    {
                        int orientation = animator.GetInteger("orientation");
                        if (orientation == 6)
                            rb.velocity = new Vector3(1.0f, 0.0f, 0.0f);
                        if (orientation == 2)
                            rb.velocity = new Vector3(-1.0f, 0.0f, 0.0f);
                        if (orientation == 0)
                            rb.velocity = new Vector3(0.0f, 1.0f, 0.0f);
                        if (orientation == 4)
                            rb.velocity = new Vector3(0.0f, -1.0f, 0.0f);

                        rb.velocity = rb.velocity * BaseAttack.Speed;
                    }
                }
                startAttackTime = Time.time;
            }
        }
    }

    void FixedUpdate()
    {
        if (CharacterState == eCharacterState.Idle || CharacterState == eCharacterState.Walking)
        {
            movement.x = Input.GetAxisRaw("Horizontal") * walkSpeed;
            movement.y = Input.GetAxisRaw("Vertical") * walkSpeed;
            if (movement.sqrMagnitude > 0.0f)
            {
                gameObject.transform.position += new Vector3(movement.x, movement.y, 0.0f);
                CharacterState = eCharacterState.Walking;
            }
            else
            {
                CharacterState = eCharacterState.Idle;
            }
        }
        else if (CharacterState == eCharacterState.Attacking)
        {
            if (Time.time > startAttackTime + 0.25f)
            {
                CharacterState = eCharacterState.Idle;
            }
        }

        animationUpdate();
    }

    private void LateUpdate()
    {
        UpdateSpriteRenderers();
    }
}
