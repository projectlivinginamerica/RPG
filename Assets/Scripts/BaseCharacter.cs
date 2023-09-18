// -----------------------------------------------------------------------------------------
// using classes
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using System;

public enum eCharacterState
{
    Idle,
    Walking,
    Attacking
};

[Serializable]
public struct DamageFX
{
    public eDamageType damageType;
    public GameObject[] prefabsToSpawn;
    public bool isDeathFX;
}

public class BaseRPGObject : MonoBehaviour
{

}

public class BaseProjectile : MonoBehaviour
{

};

// -----------------------------------------------------------------------------------------
// player movement class
public class BaseCharacter : BaseRPGObject
{
    public float health = 100;
    public float walkSpeed = 1.0f;
    public Vector2 movement;
    public Animator animator;
    public SpriteRenderer spriteRenderer;

    // Sprite Sheets
    public string WalkSpriteSheetName;
    public string AttackSpriteSheetName;

    public DamageFX[] damageFXList;

    // The dictionary containing all the sliced up sprites in the sprite sheet
    private Dictionary<string, Sprite> WalkSpriteSheet;
    private Dictionary<string, Sprite> AttackSpriteSheet;

    public eCharacterState CharacterState
    { 
        get; 
        protected set;
    }

    void Awake()
    {
        LoadSpriteSheet();
        animator.SetFloat("speed", 0);
        animator.SetInteger("orientation", 4);
    }

    void Update()
    {
    }

    void FixedUpdate()
    {
        animationUpdate();
    }

    protected void UpdateSpriteRenderers()
    {
        if (CharacterState == eCharacterState.Walking || CharacterState == eCharacterState.Idle)
        {
            this.spriteRenderer.sprite = this.WalkSpriteSheet[this.spriteRenderer.sprite.name];
        }
        else if (CharacterState == eCharacterState.Attacking)
        {
            this.spriteRenderer.sprite = this.AttackSpriteSheet[this.spriteRenderer.sprite.name];
        }
    }

    public void animationUpdate()
    {
        animator.SetFloat("speed", Mathf.Abs(movement.x) + Mathf.Abs(movement.y));
        if (movement.x > 0)
        {
            animator.SetInteger("orientation", 6);
        }

        if (movement.x < 0)
        {
            animator.SetInteger("orientation", 2);
        }

        if (movement.y > 0)
        {
            animator.SetInteger("orientation", 0);
        }

        if (movement.y < 0)
        {
            animator.SetInteger("orientation", 4);
        }
    }

    protected void LoadSpriteSheet()
    {
        {
            string spritesheetfolder = "";
            string spritesheetfilepath = spritesheetfolder + WalkSpriteSheetName;
            var sprites = Resources.LoadAll<Sprite>(spritesheetfilepath);

            this.WalkSpriteSheet = sprites.ToDictionary(x => x.name, x => x);
        }
        {
            string spritesheetfolder = "";
            string spritesheetfilepath = spritesheetfolder + AttackSpriteSheetName;
            var sprites = Resources.LoadAll<Sprite>(spritesheetfilepath);

            this.AttackSpriteSheet = sprites.ToDictionary(x => x.name, x => x);
        }
    }

    public virtual void TakeDamage(Weapon attackingWeapon)
    {
        if (health <= 0.0f)
        {
            return;
        }

        health -= attackingWeapon.DamageAmount;
        if (health < 0.0f)
        {
            if (PlayDamageFX(attackingWeapon.DamageType, true) == false)
            {
                PlayDamageFX(attackingWeapon.DamageType, false);
            }

            Die();
        }
        else
        {
            PlayDamageFX(attackingWeapon.DamageType, false);
        }
    }

    private bool PlayDamageFX(eDamageType damageType, bool bDeathFX)
    {
        foreach(var fx in damageFXList)
        {
            if (fx.damageType != damageType)
            {
                continue;
            }

            if (fx.isDeathFX != bDeathFX)
            {
                continue;
            }

            foreach(var prefab in fx.prefabsToSpawn)
            {
                if (prefab == null)
                {
                    continue;
                }

                GameObject newGameObj = Instantiate(prefab, gameObject.transform.parent.transform);
                newGameObj.transform.position = gameObject.transform.position;
                    
            }
            return true;
        }

        return false;
    }

    public bool IsDead()
    {
        return health <= 0.0f;
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
