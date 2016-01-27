using UnityEngine;

public class EnemyManager : MonoBehaviour
{
  public PlayerHealth playerHealth;
  public GameObject[] spawnAreas; //Set as array to allow multiple spawn points in order to replace random enemy generation if level is packed.
  SpawnCollider spawnCollider;
  public GameObject[] enemy;
  
  public float spawnTime;

//   public int MaxNumberOfEnemy;
  public static int NumberOfEnemy;


  void Start ()
  {
    InvokeRepeating ("Spawn", spawnTime, spawnTime);
    NumberOfEnemy = 0;
  }

  void Spawn ()
  {
    if(NumberOfEnemy >= enemy.Length)
    {
      CancelInvoke("Spawn");
      return;
    }
    if( playerHealth.currentHealth <= 0f )
      {
        return;
      }
    for( int i = 0; i < spawnAreas.Length; i++ )
    {
      if( spawnAreas[i].GetComponent<SpawnCollider>().InEnemySpawnRange )
      {
      int rndX = 2 * Random.Range(-3 , 3);
      int rndZ = 2 * Random.Range(-3 , 3);
      Vector3 rndOffset = new Vector3(rndX, 0, rndZ);
      Vector3 enemyPosition = spawnAreas[i].transform.position + rndOffset;
      enemy[NumberOfEnemy]=(GameObject)Instantiate (enemy[NumberOfEnemy], enemyPosition, spawnAreas[i].transform.rotation);
      NumberOfEnemy ++;
      }
    }
  }
  
  public GameObject getClosestEnemy( Vector3 position )
  {
    float minDistance = Mathf.Infinity;
    GameObject closestEnemy = null;
    foreach (GameObject e in enemy)
    {
      if( e == null )
      {
      continue;
      }
    
      if( e.transform.position.y > 0.0f )
      {
        float dist = Vector3.Distance(e.transform.position, position);
        if (dist < minDistance)
        {
            closestEnemy = e;
            minDistance = dist;
        }
      }
    }
    return closestEnemy;
  }
}
