using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElianCharTest : MonoBehaviour
{
    public float walkSpeed = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

void FixedUpdate()
   {
        Vector3 movement = new Vector3(0,0,0);

        // Make a movement vector from keyboard and controller input
        movement.x = Input.GetAxisRaw("Horizontal") * walkSpeed;
        movement.y = Input.GetAxisRaw("Vertical") * walkSpeed;

        if (movement.sqrMagnitude > 0.0f)        // Check if the player actually moved
        {
            gameObject.transform.position += new Vector3(movement.x, movement.y, 0.0f);
        }
   }
}
