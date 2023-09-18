// -----------------------------------------------------------------------------------------
// using classes
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Runtime.CompilerServices;

public enum eDamageType
{
    Physical,
    Arcane,
    Fire,
    Cold,
}


public class Weapon : MonoBehaviour
{
    public eDamageType DamageType = eDamageType.Physical;
    public float DamageAmount;
    public float Speed;

    private void OnTriggerEnter(Collider other)
    {
        Enemy enemyHit = other.gameObject.GetComponent<Enemy>();
        if (enemyHit != null)
        {
            enemyHit.TakeDamage(this);
            Destroy(gameObject);
        }
    }
}
