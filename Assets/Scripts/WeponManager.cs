using Mono.Cecil;
using Unity.Profiling;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeponManager : MonoBehaviour
{

   [SerializeField] private Transform WeponTransform;
   [SerializeField] private CameraControl cameraControl;
    [SerializeField] private Animator anim;
    [SerializeField] private int Bullets=10;
    void Awake()
    {
        anim =GetComponent<Animator>();
    }

    void Start()
    {
        
    }

  
    void Update()
    {
        WeponTransform.localRotation=cameraControl._characterHead.localRotation;
        if (Input.GetMouseButtonDown(0))
        {
            anim.SetBool("Fire_1",true);
        }
        else
        {
            anim.SetBool("Fire_1",false);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            anim.SetBool("Realod",true);
        }
        else
        {
            anim.SetBool("Realod",false);
        }
        if (Bullets == 0)
        {
             anim.SetBool("Fire_2",true);
        }
    }
    public void Fire()
    {
        Debug.Log("ates etti");
    }
    public void Reload()
    {
        Debug.Log("reoled");
    }
    
}
