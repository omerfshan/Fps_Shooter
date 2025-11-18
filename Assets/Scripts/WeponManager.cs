using Mono.Cecil;
using Unity.Profiling;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeponManager : MonoBehaviour
{
   
   [SerializeField] private Transform WeponTransform;
   [SerializeField] private CameraControl cameraControl;
   [SerializeField] AnimationControl _anim;
   [SerializeField] private string _fire_ID;
   [SerializeField] private string _fire_2_ID;
   [SerializeField] private string _reoled_ID;
    [Header("Fire")]
    [SerializeField] private bool _fire;
  
    [SerializeField] private float fireFreq;
    private float fireCount;
    private RaycastHit hit;
    [SerializeField] private  float fireRange;
    [SerializeField] private LayerMask ignore;
    public bool Availability;
     [Header("Reload Variables")]
    [SerializeField] private bool _reoled; 
    [SerializeField] private int CurrentAmmo;
    [SerializeField] private int MaxAmmo;
    [SerializeField] private int _totalAmmo;
   
    [Header("Ammo")]
    [SerializeField]AmmoType ammoType;
    [SerializeField] private int  _5_56,_7_62,_9mm,_45cal,_12ga;

    private void SeTotalAmmo()
    {
        if (ammoType == AmmoType._9mm)
        {
            _totalAmmo=_9mm;
        }
        else if(ammoType == AmmoType._5_56)
        {
            _totalAmmo=_5_56;
        }
        else if(ammoType == AmmoType._7_62)
        {
            _totalAmmo=_7_62;
        }
        else if(ammoType == AmmoType._45cal)
        {
            _totalAmmo=_45cal;
        } 
        else if(ammoType == AmmoType._12ga)
        {
            _totalAmmo=_12ga;
        }


    }
  

    void Start()
    {
        
    }

  
    void Update()
    {
        SeTotalAmmo();
        WeponTransform.localRotation=cameraControl._characterHead.localRotation;
        if (Input.GetMouseButtonDown(0)&&!_reoled&&CurrentAmmo>0&&Time.time>fireCount&Availability)
        {
           StartFire();
        }
       
        if ((Input.GetKeyDown(KeyCode.R)||CurrentAmmo<=0)&&_totalAmmo>0&&CurrentAmmo!=MaxAmmo&&!_fire)
        {
          StartReload();
        }
       
        
    }
    public void StartFire()
    {
        _fire=true;
        if (CurrentAmmo <= 1)
        {
              _anim.setBool(_fire_2_ID,_fire);
        }
        else
        {
            _anim.setBool(_fire_ID,_fire);
        }
        CurrentAmmo--;
        fireCount=Time.time+fireFreq;
        
        if(Physics.Raycast(Shooter.instance.Camera.position,Shooter.instance.Camera.forward,out hit, fireRange, ~ignore))
        {
          if(hit.rigidbody!=null)
            {
                Rigidbody rb = hit.rigidbody;
                if (rb != null)
                {
                    rb.AddForce(-hit.normal * 1000f);
                }
            }
        }
    }
     public  void EndFire()
    {
        _fire=false;
        _anim.setBool(_fire_ID,_fire);
        _anim.setBool(_fire_2_ID,_fire);
    }
     public  void StartReload()
    {
        _reoled=true;
        _anim.setBool(_reoled_ID,_reoled);
    }
     public  void EndReload()
    {
        _reoled=false;
        _anim.setBool(_reoled_ID,_reoled);
        int Amount=SetReloadAmount(_totalAmmo);
        CurrentAmmo+=Amount;
         if (ammoType == AmmoType._9mm)
        {
            _9mm-=Amount;
        }
        else if(ammoType == AmmoType._5_56)
        {
            _5_56-=Amount;
        }
        else if(ammoType == AmmoType._7_62)
        {
            _7_62-=Amount;
        }
        else if(ammoType == AmmoType._45cal)
        {
            _45cal-=Amount;
        } 
        else if(ammoType == AmmoType._12ga)
        {
            _12ga-=Amount;
        }
    }
    public void WeponDown()
    {
        
    }
    private int SetReloadAmount(int InventoryAmount)
    {
        int AmountNeeded=MaxAmmo-CurrentAmmo;
        if (AmountNeeded < InventoryAmount)
        {
            return AmountNeeded;
        }
        else
        {
            return InventoryAmount;
        }
    }
   

    
}
