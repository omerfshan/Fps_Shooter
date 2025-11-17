using Mono.Cecil;
using Unity.Profiling;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeponManager : MonoBehaviour
{
   
   [SerializeField] private Transform WeponTransform;
   [SerializeField] private CameraControl cameraControl;
   private bool _fire;
   private bool _reoled; 
   [SerializeField] private string _fire_ID;
   [SerializeField] private string _fire_2_ID;
   [SerializeField] private string _reoled_ID;
  
  

    void Start()
    {
        
    }

  
    void Update()
    {
        WeponTransform.localRotation=cameraControl._characterHead.localRotation;
        if (Input.GetMouseButtonDown(0))
        {
           
        }
        else
        {
          
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
          
        }
        else
        {
           
        }
        
    }
    public void StartFire()
    {
        
    }
     public  void EndFire()
    {
        
    }
     public  void StartReload()
    {
       
    }
     public  void EndReload()
    {
        
    }
    public void WeponDown()
    {
        
    }

    
}
