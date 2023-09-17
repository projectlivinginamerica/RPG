// -----------------------------------------------------------------------------------------
// using classes
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;

public enum eCharacterState
{
    Idle,
    Walking,
    Attacking
};

public class BaseProjectile : MonoBehaviour
{

};

// -----------------------------------------------------------------------------------------
// player movement class
public class BaseCharacter : MonoBehaviour
{
    // -----------------------------------------------------------------------------------------
    // public members
    public float walkSpeed = 1.0f;
    public Transform tf;
    public Vector2 movement;
    public Animator animator;
    public SpriteRenderer spriteRenderer;

    // The name of the sprite sheet to use
    public string WalkSpriteSheetName;
    public string AttackSpriteSheetName;

    // -----------------------------------------------------------------------------------------
    // private members
    private Vector2 previousPosition;

    // The name of the currently loaded sprite sheet
    private string LoadedSpriteSheetName;

    // The dictionary containing all the sliced up sprites in the sprite sheet
    private Dictionary<string, Sprite> WalkSpriteSheet;
    private Dictionary<string, Sprite> AttackSpriteSheet;

    public eCharacterState CharacterState
    { 
        get; 
        protected set;
    }

    // -----------------------------------------------------------------------------------------
    // awake method to initialisation
    void Awake()
    {
        previousPosition = tf.position;
        //velocity = rb.velocity;
        this.LoadSpriteSheet();
        animator.SetFloat("speed", 0);
        animator.SetInteger("orientation", 4);
    }

    // -----------------------------------------------------------------------------------------
    // Update is called once per frame
    void Update()
    {
    }

    // -----------------------------------------------------------------------------------------
    // fixed update methode
    void FixedUpdate()
    {


       // previousPosition = tf.position;

        animationUpdate();
    }

    // Runs after the animation has done its work
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

    // -----------------------------------------------------------------------------------------
    // Set the animation parameters
    public void animationUpdate()
    {
        animator.SetFloat("speed", Mathf.Abs(movement.x) + Mathf.Abs(movement.y));
        if (movement.x > 0)
            animator.SetInteger("orientation", 6);
        if (movement.x < 0)
            animator.SetInteger("orientation", 2);
        if (movement.y > 0)
            animator.SetInteger("orientation", 0);
        if (movement.y < 0)
            animator.SetInteger("orientation", 4);
    }

    // -----------------------------------------------------------------------------------------
    // Loads the sprites from a sprite sheet
    protected void LoadSpriteSheet()
    {
        // Load the sprites from a sprite sheet file (png). 
        // Note: The file specified must exist in a folder named Resources
        {
            string spritesheetfolder = "";//Chars/";
            string spritesheetfilepath = spritesheetfolder + WalkSpriteSheetName;
            var sprites = Resources.LoadAll<Sprite>(spritesheetfilepath);
            Debug.Log("Loading " + spritesheetfilepath);
            this.WalkSpriteSheet = sprites.ToDictionary(x => x.name, x => x);
      //      this.spriteRenderer.sprite = this.WalkSpriteSheet[this.spriteRenderer.sprite.name];
        }
        {
            string spritesheetfolder = "";//Chars/";
            string spritesheetfilepath = spritesheetfolder + AttackSpriteSheetName;
            var sprites = Resources.LoadAll<Sprite>(spritesheetfilepath);
            Debug.Log("Loading " + spritesheetfilepath);
       //     this.spriteRenderer.sprite = this.AttackSpriteSheet[this.spriteRenderer.sprite.name];
            this.AttackSpriteSheet = sprites.ToDictionary(x => x.name, x => x);
        }
        // Remember the name of the sprite sheet in case it is changed later
      //  this.LoadedSpriteSheetName = this.SpriteSheetName;
    }
}
