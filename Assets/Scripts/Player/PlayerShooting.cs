using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
  public int damagePerShot = 20;
  public float timeBetweenBullets = 0.25f;
  public float range = 100f;

  float timer;
  Ray shootRay;
  RaycastHit shootHit;
  int shootableMask;
  ParticleSystem gunParticles;
  LineRenderer gunLine;
  AudioSource gunAudio;
  Light gunLight;
  float effectsDisplayTime = 0.2f;
    
  public EnemyManager enemyManager;
  public static GameObject lockedEnemy;
  bool enemyIsLocked;


  void Awake ()
  {
    shootableMask = LayerMask.GetMask ("Shootable");
    gunParticles = GetComponent<ParticleSystem> ();
    gunLine = GetComponent <LineRenderer> ();
    gunAudio = GetComponent<AudioSource> ();
    gunLight = GetComponent<Light> ();
//  enemyManager= GameObject.FindGameObjectWithTag ("EnemyManager").GetComponent<EnemyManager>();
  }


  void Update ()
  {
    timer += Time.deltaTime;

    if( Input.GetButton ("Fire1") && timer >= timeBetweenBullets &&
      Time.timeScale != 0 )
      {
      Shoot ();
      }

    if( timer >= timeBetweenBullets * effectsDisplayTime )
      {
      DisableEffects ();
      }

    if( Input.GetButtonDown ("LockEnemy") )
      {
      if( enemyIsLocked )
        {
        enemyIsLocked = false;
        lockedEnemy = null;
        }
      else
        {
        enemyIsLocked = true;
        lockedEnemy = enemyManager.getClosestEnemy( transform.position );
        }
      }
  }


  public void DisableEffects ()
  {
    gunLine.enabled = false;
    gunLight.enabled = false;
  }


  void Shoot ()
  {
    timer = 0.0f;

    gunAudio.Play ();

    gunLight.enabled = true;

    gunParticles.Stop ();
    gunParticles.Play ();
        
    gunLine.enabled = true;
    gunLine.SetPosition (0, transform.position);

    shootRay.origin = transform.position;
        
    //Handle enemy locking 
    if( lockedEnemy != null )
      {
      if(lockedEnemy.transform.position.y >0)
        {
        Vector3 attackDirection = lockedEnemy.transform.position - transform.position;
        attackDirection.y += 0.4f;
        shootRay.direction = attackDirection;
        } 
      else
        {
        lockedEnemy = enemyManager.getClosestEnemy( transform.position );//Lock another enmy when one is killed
        //lockedEnemy = null;  //No enemy locked when another is killed
        }
      }
    else
      {
      enemyIsLocked = false;
      shootRay.direction = transform.forward;
      }

    if(Physics.Raycast (shootRay, out shootHit, range, shootableMask))
      {
      EnemyHealth enemyHealth = shootHit.collider.GetComponent <EnemyHealth> ();
      if(enemyHealth != null)
        {
        enemyHealth.TakeDamage (damagePerShot, shootHit.point);
        }
      gunLine.SetPosition (1, shootHit.point);
      }
    else
      {
      gunLine.SetPosition (1, shootRay.origin + shootRay.direction * range);
      }
  }

}//End class PlayerShooting
