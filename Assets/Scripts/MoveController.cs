
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class MoveController : MonoBehaviour
{
    // public Stat
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
    void Awake()
    {
        cc = GetComponent<CharacterController>();
    }
    void Start()
    {
        jumpForce=Mathf.Sqrt(2f*Jumpheight*-_gravity);
    }
    void Update()
    {    Debug.Log("Grounded mı? → " + isGrounded());
        Jump();
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

     private bool isGrounded()
    {
        return Physics.CheckSphere(_groundCheck.position,0.4f,layer);
    }
    private float MoveSpeed()
    {
        if (Input.GetKey(KeyCode.LeftShift))
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
        v +=_gravity*Time.deltaTime;
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
        Vector3 pushDir = hit.moveDirection;  // gerçek çarpma yönü
        hit.rigidbody.AddForce(pushDir * .2f, ForceMode.Impulse);
    }
}

}
