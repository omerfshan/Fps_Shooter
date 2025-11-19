
using System;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class MoveController : MonoBehaviour
{
    public static MoveController instance;
    private CharacterController cc;
    private float X, Z;
    [SerializeField] private float RunSpeed;
    [SerializeField] private float WalkSpeed;
    private const float _gravity=-9.81f;
    private float v;
    private float jumpForce;
    [SerializeField] private float Jumpheight=1.5f;
    [SerializeField] private LayerMask layer;
    [SerializeField] private Transform _groundCheck;
    public bool isWalking;
    public bool isRuning;
    void Awake()
    {
        cc = GetComponent<CharacterController>();
        instance=this;
    }
    void Start()
    {
        jumpForce=Mathf.Sqrt(2f*Jumpheight*-_gravity);
    }
    void Update()
    {    
        Jump();
        CheckMovement();
        Movement();
        Gravity();
    }
    private void Movement()
    {
        X = Input.GetAxis("Horizontal");
        Z = Input.GetAxis("Vertical");
        Vector3 input = new Vector3(X, 0, Z);
        
        if (input.magnitude > 1)
            input.Normalize();
        
        Vector3 move = transform.TransformDirection(input)*MoveSpeed();
        move.y=v;
        cc.Move(move*Time.deltaTime);
    }
    private void CheckMovement()
    {
      if (X != 0f || Z != 0f)
        {
            if (MoveSpeed() == RunSpeed)
            {
                isWalking=false;
                isRuning=true;
            }
            else if (MoveSpeed() == WalkSpeed)
            {
                isRuning=false;
                isWalking = true; 
            }
        }
        else
        {
                isRuning=false;
                isWalking=false;
        }

    }
     private bool isGrounded()
    {
        return Physics.CheckSphere(_groundCheck.position,0.4f,layer);
    }
   public float MoveSpeed()
    {
        if (Input.GetKey(KeyCode.LeftShift)&&!WeponManager.instance._fire)
        {
            return RunSpeed;
        }
        else
        {
            return WalkSpeed;
        }
    }
    private void Gravity()
    {
        if (isGrounded() && v < 0)
        {
            v=-2f;
        }
        v +=-Mathf.Pow(_gravity,2)*Time.deltaTime;
    }
    private void Jump()
    {
        if (isGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            v=jumpForce;
        }
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
{
    if (hit.rigidbody != null)
    {
        Vector3 pushDir = hit.moveDirection; 
        hit.rigidbody.AddForce(pushDir * .2f, ForceMode.Impulse);
    }
}

}
