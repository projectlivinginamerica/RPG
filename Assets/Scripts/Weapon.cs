// -----------------------------------------------------------------------------------------
// using classes
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Runtime.CompilerServices;


public class Weapon : MonoBehaviour
{
    public float DamageAmount;
    public float Speed;


    private void OnTriggerEnter(Collider other)
    {
        Enemy enemyHit = other.gameObject.GetComponent<Enemy>();
        if (enemyHit != null)
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
