using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
  public Transform target;
  public float smoothing = 5.0f;
  
  float offsetPosition;
  float offsetAngle;
  

  void Start()
    {
    offsetPosition = 6.0f;
    offsetAngle = 30.0f;
    }

  void FixedUpdate()
    {
    float cameraAngle = transform.eulerAngles.y;
    float playerAngle = target.eulerAngles.y;
    
    //Handle enemy locking 
    if(PlayerShooting.lockedEnemy != null)
      {
      Vector3 enemyPosition = PlayerShooting.lockedEnemy.transform.position;
      Vector3 enemyDirection = enemyPosition - target.position;
      float enemyAngle = Vector3.Angle( target.forward, enemyDirection );
      //Look for the angle polarity to allow negative angles
      Vector3 cross = Vector3.Cross(target.forward, enemyDirection);
      if( cross.y < 0 )
        {
        enemyAngle *= -1;
        }
    
      playerAngle += enemyAngle;
      }

    float targetAngle = Mathf.LerpAngle( cameraAngle, playerAngle, 2.0f * Time.deltaTime );
    Quaternion targetRotation = Quaternion.Euler( offsetAngle, targetAngle, 0.0f );
    Vector3 targetPosition = target.position - targetRotation * Vector3.forward * offsetPosition;

    transform.rotation = targetRotation;
    transform.position = targetPosition;
    }
}
