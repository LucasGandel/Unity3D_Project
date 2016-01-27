using UnityEngine;
using System.Collections;


public class EnemyAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 0.5f;     // The time in seconds between each attack.
    public int attackDamage = 10;               // The amount of health taken away per attack.


    Animator anim;                              // Reference to the animator component.
    GameObject player;                          // Reference to the player GameObject.
    PlayerHealth playerHealth;                  // Reference to the player's health.
    EnemyHealth enemyHealth;                    // Reference to this enemy's health.
    bool playerInAttackRange;                   // Whether player can be attacked.
    public bool playerInMoveRange;              // Whether player is close enough to walk to him.
    float timer;                                // Timer for counting up to the next attack.
    
    int colliderTriggered;                      // Level of entered colliders


    void Awake()
    {
        // Setting up the references.
        player = GameObject.FindGameObjectWithTag ("Player");
        playerHealth = player.GetComponent <PlayerHealth> ();
        enemyHealth = GetComponent<EnemyHealth>();
        anim = GetComponent <Animator> ();
        
        colliderTriggered = 0;
    }


    void OnTriggerEnter( Collider other )
    {
        // If the entering collider is the player
        if(other.gameObject == player)
        {
          //Increment the level of entered colliders
          colliderTriggered ++;
          setPlayerRange();
        }
        // If an enemy enter the collider
        if( other.gameObject.tag == "Enemy")
        {
          // Set the enemy behavior as our ****WARNING****: Can create a funny bugged group behavior
          EnemyAttack enemyAttack = other.gameObject.GetComponent <EnemyAttack>();
          enemyAttack.playerInMoveRange = playerInMoveRange;
        
        }
    }


    void OnTriggerExit( Collider other )
    {
        // If the exiting collider is the player
        if(other.gameObject == player)
        {
          //Increment the level of entered colliders
          colliderTriggered --;
          setPlayerRange();
        }
    }
    
    void FixedUpdate()
    {
      anim.SetBool( "IsWalking", playerInMoveRange );
    }


    void Update()
    {
        // Add the time since Update was last called to the timer.
        timer += Time.deltaTime;

        // If the timer exceeds the time between attacks, the player is in range and this enemy is alive...
        if(timer >= timeBetweenAttacks && playerInAttackRange && enemyHealth.currentHealth > 0)
        {
          // ... attack.
          Attack ();
        }
//         print(playerInMoveRange);

//         anim.SetBool( "IsWalking", playerInMoveRange );
        
        // If the player has zero or less health...
        if(playerHealth.currentHealth <= 0)
        {
          // ... tell the animator the player is dead.
          anim.SetTrigger ("PlayerDead");
        }
    }


    void Attack()
    {
        // Reset the timer.
        timer = 0f;

        // If the player has health to lose...
        if(playerHealth.currentHealth > 0)
        {
          // ... damage the player.
          playerHealth.TakeDamage (attackDamage);
        }
    }
    
    
    void setPlayerRange()
    {
    // Colliders from closest to farest
    
      // 1 : Move Range Collider
      playerInMoveRange = colliderTriggered > 0;
      
      // 2 : Attack Range Collider
      playerInAttackRange = colliderTriggered > 1 ;
    }

}