using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter3D : Character3D
{
    void FixedUpdate()
    {
        //if (CharacterState == eCharacterState.Idle || CharacterState == eCharacterState.Walking)
        {
            _movement.x = Input.GetAxisRaw("Horizontal") * _walkSpeed;
            _movement.y = Input.GetAxisRaw("Vertical") * _walkSpeed;
            /*     if (_movement.sqrMagnitude > 0.0f)
                 {
                     gameObject.transform.position += new Vector3(movement.x, movement.y, 0.0f);
                   //  CharacterState = eCharacterState.Walking;
                 }
                 else
                 {
                    // CharacterState = eCharacterState.Idle;
                 }*/
            gameObject.GetComponent<Rigidbody>().velocity = _movement;
        }
        /*  else if (CharacterState == eCharacterState.Attacking)
          {
              if (Time.time > startAttackTime + 0.25f)
              {
                  CharacterState = eCharacterState.Idle;
              }
          }*/

        // animationUpdate();
    }   
}
