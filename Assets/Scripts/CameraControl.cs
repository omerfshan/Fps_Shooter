using UnityEngine;
using Unity.Mathematics;

public class CameraControl : MonoBehaviour
{
    public static CameraControl instance;
     public Transform _characterHead;
    [SerializeField] private Transform _characterBody;
    [SerializeField] private float _sensitivity = 200f;
    [SerializeField] private bool isLocked = true;
 

    private float yaw;   // body Y
    private float pitch; // head X
    void Awake()
    {
        instance=this;
    }
    void Start()
    {
        if (isLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

       
        yaw   = _characterBody.localEulerAngles.y;
        pitch =_characterHead.localEulerAngles.x;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * _sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * _sensitivity;


        yaw   += mouseX;      
        pitch -= mouseY;      
        pitch  = math.clamp(pitch, -80f, +80f);  

        _characterBody.localRotation = Quaternion.Euler(0f, yaw, 0f);
        _characterHead.localRotation = Quaternion.Euler(pitch, 0f, 0f);
    }
   public void AddRecoll(float x,float y)
    {
        yaw+=x;
        pitch+=y;
    }

   
}
