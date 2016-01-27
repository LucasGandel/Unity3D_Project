using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
  public int startingHealth = 100;
  public int currentHealth = 100;
  public float sinkSpeed = 2.0f;
  public int scoreValue = 10;
  public AudioClip deathClip;


  Animator anim;
  AudioSource enemyAudio;
  ParticleSystem hitParticles;
  CapsuleCollider capsuleCollider;
  bool isDead;
  bool isSinking;
  bool isGrowing;
    
  EnemyAttack enemyAttack;


  void Awake ()
  {
    anim = GetComponent <Animator> ();
    enemyAudio = GetComponent <AudioSource> ();
    hitParticles = GetComponentInChildren <ParticleSystem> ();
    capsuleCollider = GetComponent <CapsuleCollider> ();;
    enemyAttack = GetComponent <EnemyAttack> ();
    GetComponent <NavMeshAgent> ().enabled = true;
    GetComponent <Rigidbody> ().isKinematic = false;
    currentHealth = startingHealth;
        
    StartGrowing();
  }


  void Update ()
  {
    if(isSinking)
    {
      transform.Translate (-Vector3.up * sinkSpeed * Time.deltaTime);
    }
    if( isGrowing )
    {
      transform.Translate (Vector3.up * sinkSpeed/2 * Time.deltaTime);
      if(transform.position.y > 0.0f)
      {
        StopGrowing();
      }
    }        
  }


  public void TakeDamage (int amount, Vector3 hitPoint)
  {
    if(isDead)
      return;

    enemyAudio.Play ();

    currentHealth -= amount;
          
    hitParticles.transform.position = hitPoint;
    hitParticles.Play();

    if(currentHealth <= 0)
    {
      Death ();
    }
    //Set enemy toward player if attacked
    enemyAttack.playerInMoveRange = true;
  }


  void Death ()
  {
    isDead = true;

    capsuleCollider.isTrigger = true;

    anim.SetTrigger ("Dead");

    enemyAudio.clip = deathClip;
    enemyAudio.Play ();
  }


  public void StartSinking ()
  {
    GetComponent <NavMeshAgent> ().enabled = false;
    GetComponent <Rigidbody> ().isKinematic = true;
    isSinking = true;
    ScoreManager.score += scoreValue;
//     EnemyManager.NumberOfEnemy --;
    Destroy (gameObject, 2f);
  }
    
  public void StartGrowing()
  {
    capsuleCollider.isTrigger = true;
    foreach(Collider c in GetComponents<SphereCollider> ())
    {
      c.enabled = false;
    }
    GetComponent <NavMeshAgent>().enabled = false;
    GetComponent <Rigidbody> ().isKinematic = true;
    isGrowing = true;
  }
    
  public void StopGrowing()
  {
    capsuleCollider.isTrigger = false;
    foreach(Collider c in GetComponents<SphereCollider> ())
    {
      c.enabled = true;
    }
    GetComponent <NavMeshAgent> ().enabled = true;
    GetComponent <Rigidbody> ().isKinematic = false;
    isGrowing = false;
  }
}
