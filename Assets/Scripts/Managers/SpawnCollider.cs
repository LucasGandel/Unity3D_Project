using UnityEngine;
using System.Collections;

public class SpawnCollider : MonoBehaviour {

  public bool InEnemySpawnRange;
  

  // Use this for initialization
  void Start ()
  {

  }

  // Update is called once per frame
  void Update ()
  {
    
  }

void OnTriggerEnter( Collider other )
{
  // If an enemy enter the collider
  if( other.CompareTag( "Player" ) )
  {
    InEnemySpawnRange = true;
  }
}

void OnTriggerExit( Collider other )
{
  // If an enemy enter the collider
  if( other.CompareTag( "Player" ) )
  {
    InEnemySpawnRange = false;
  }
}

}
