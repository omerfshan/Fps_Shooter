
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class MoveController : MonoBehaviour
{
    private CharacterController cc;
    private float X, Z;
    [SerializeField] private float RunSpeed;
    [SerializeField] private float WalkSpeed;
    void Awake()
    {
        cc = GetComponent<CharacterController>();
    }
    void Start()
    {

    }
    void Update()
    {
        Movement();
    }
    private void Movement()
    {
        X = Input.GetAxis("Horizontal");
        Z = Input.GetAxis("Vertical");
        Vector3 input = new Vector3(X, 0, Z);
        
        if (input.magnitude > 1)
            input.Normalize();
        
        Vector3 move = transform.TransformDirection(input);
        cc.Move(move*Time.deltaTime*MoveSpeed());



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
}
