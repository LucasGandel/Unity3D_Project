using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
   public float speed = 4.0f;
   Vector3 movement;
   Animator anim;
   Rigidbody playerRigidbody;
   int floorMask;
   
   void Awake()
     {
//      floorMask = LayerMask.GetMask( "Floor" );
     anim = GetComponent<Animator>();
     playerRigidbody = GetComponent<Rigidbody>();
     }
     
   void FixedUpdate()
     {
     float h = Input.GetAxisRaw( "Horizontal" );
     float v = Input.GetAxisRaw( "Vertical" );
     Move( h, v );
     Turning( h, v );
     Animating( h, v );
     }
   
   void Move( float h, float v )
     {
//      movement.Set( h, 0.0f, v );
//      movement.x *= transform.forward.x;
//      movement.z *= transform.forward.z;
     movement = v*transform.forward /*+ h*transform.right*/;
     movement = movement.normalized * speed * Time.deltaTime;
//      movement = transform.forward * Mathf.Clamp( h + v, -1.0f, 1.0f ) * speed * Time.deltaTime;

     playerRigidbody.MovePosition( transform.position + movement );
     }
   
   void Turning( float h, float v )
     {
     
     Vector3 rightCameraDirection = Camera.main.transform.right;
     rightCameraDirection.y = 0.0f;

     Vector3 forwardPlayerDirection = transform.forward;
     forwardPlayerDirection.y = 0.0f;
     
     Vector3 rightPlayerDirection = transform.right;
     rightPlayerDirection.y = 0.0f;

     if(h != 0.0f || v != 0.0f )
       {
       Quaternion newRotation = Quaternion.LookRotation( (v * forwardPlayerDirection + h * rightPlayerDirection) );
       playerRigidbody.MoveRotation( Quaternion.Lerp( transform.rotation, newRotation, 2.0f * Time.deltaTime ) );
       }
//      float angleCameraPlayer = Vector3.Angle( forwardCameraDirection, forwardPlayerDirection );
//      if( Mathf.Abs( angleCameraPlayer ) > 30.0f )
//        {
//        float targetAngle = Mathf.Lerp( 0.0f, angleCameraPlayer, 1.0f * Time.deltaTime );
//        Quaternion targetRotation = Quaternion.Lerp( Camera.main.transform.rotation ,playerRigidbody.rotation , 1.0f * Time.deltaTime );
//        Quaternion targetRotation = Quaternion.AngleAxis( targetAngle, Vector3.up );
//         Quaternion targetRotation = Quaternion.RotateTowards( Camera.main.transform.rotation, playerRigidbody.rotation, 5.0f );
//         Camera.main.transform.rotation = targetRotation;
//        Camera.main.transform.RotateAround( playerRigidbody.position, Vector3.up, targetAngle );
//        Camera.main.transform.LookAt( playerRigidbody.position );
//        Quaternion Rotation = Quaternion.LookRotation( playerRigidbody.position );
//        Camera.main.transform.rotation *= Rotation;
       
//        }
//      float angle = Mathf.Abs( Vector3.Angle( Camera.main.transform.forward, playerRigidbody.transform.forward ) );
//             print(angle);

//       print(angleCameraPlayer);
     }
   
   void Animating( float h, float v )
     {
     bool walking = (h != 0.0f || v != 0.0f);
     anim.SetBool( "IsWalking", walking );
     }

}
