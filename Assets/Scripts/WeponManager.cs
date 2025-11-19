using Mono.Cecil;
using TMPro;
using TMPro.EditorUtilities;
using Unity.Profiling;
using UnityEngine;
using UnityEngine.Assertions.Comparers;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class WeponManager : MonoBehaviour
{
   public static WeponManager instance;
   [SerializeField] private Transform WeponTransform;
   [SerializeField] private Transform _currenyWeaponParent;
   [SerializeField] private CameraControl cameraControl;
   [SerializeField] AnimationControl _anim;
   [SerializeField] private string _fire_ID;
   [SerializeField] private string _fire_2_ID;
   [SerializeField] private string _reoled_ID;
   [SerializeField] private string _Anim_ID;
    [Header("Fire")]
    [HideInInspector] public bool _fire;
    [SerializeField] private ParticleSystem muzzle;
    [SerializeField] private ParticleSystem shell;
    [SerializeField] private float fireFreq;
    private float fireCount;
    private RaycastHit hit;
    [SerializeField] private  float fireRange;
    [SerializeField] private LayerMask ignore;
    public bool Availability;
    [SerializeField] private bool _reoled; 
    [SerializeField] private int CurrentAmmo;
    [SerializeField] private int MaxAmmo;
    [SerializeField] private int _totalAmmo;
    [SerializeField]AmmoType ammoType;
    [SerializeField] private int  _5_56,_7_62,_9mm,_45cal,_12ga;
    [SerializeField] private GameObject[] decals;
     [SerializeField] private GameObject[] particles;
     //Indicators
    [SerializeField] private TMP_Text CurrentAmmoText;
    [SerializeField] private TMP_Text TotalAmmoText;
    private float _fireEndTime;
    //aim
    [SerializeField] Vector3 OriginalPos;
    [SerializeField] Vector3 AimPos;
    [SerializeField] Quaternion OriginalRot;
    [SerializeField] Quaternion AimRot;
    [SerializeField] float AimSpeed;
    [SerializeField] float OriginalFOV;
    [SerializeField] float AimFOV;
    public bool Aim;
    private Camera _camera;
    [SerializeField] private Quaternion MinScatter;
    [SerializeField] private Quaternion MaxScatter;
    [SerializeField] private Quaternion CurrentScatter;
    [SerializeField] private Vector2 MaxRecoil;
    [SerializeField] private Vector2 MinRecoil;
    [SerializeField] private Recoil cameraRecoil;


    void Awake()
    {
        instance=this;
        _camera=Camera.main;
    }

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
  

    
  
    void Update()
    {
        SeTotalAmmo();
        SetAim();
        CurrentAmmoText.text = CurrentAmmo.ToString();
        TotalAmmoText.text   = _totalAmmo.ToString();
        // WeponTransform.localRotation=cameraControl._characterHead.localRotation;
        
   
        if (Input.GetMouseButtonDown(0) && !_reoled && CurrentAmmo > 0 && Time.time > fireCount && Availability)
        {
            StartFire();
        }

     
        if (_fire && Time.time >= _fireEndTime)
        {
            EndFire();
        }

        // Reload
        if ((Input.GetKeyDown(KeyCode.R) || CurrentAmmo <= 0) && _totalAmmo > 0 && CurrentAmmo != MaxAmmo && !_fire)
        {
            StartReload();
        }
        if (Input.GetMouseButtonDown(1))
        {
           SetAimBool();
        }
    }
    private void SetAimBool()
    {
        Aim=!Aim;
    }
    private void SetAim()
    {
        if (Aim)
        {
            _currenyWeaponParent.localPosition=Vector3.Lerp(_currenyWeaponParent.localPosition,AimPos,AimSpeed*Time.deltaTime);
            _currenyWeaponParent.localRotation=Quaternion.Lerp(_currenyWeaponParent.localRotation,AimRot,AimSpeed*Time.deltaTime);
            _camera.fieldOfView=Mathf.Lerp(_camera.fieldOfView,AimFOV,AimSpeed*Time.deltaTime);

        }
        else
        {
             _currenyWeaponParent.localPosition=Vector3.Lerp(_currenyWeaponParent.localPosition,OriginalPos,AimSpeed*Time.deltaTime);
            _currenyWeaponParent.localRotation=Quaternion.Lerp(_currenyWeaponParent.localRotation,OriginalRot,AimSpeed*Time.deltaTime);
            _camera.fieldOfView=Mathf.Lerp(_camera.fieldOfView,OriginalFOV,AimSpeed*Time.deltaTime);
        }
        _anim.setBool(_Anim_ID,Aim);
    }

    public void StartFire()
    {
        _fire = true;

    

        if (CurrentAmmo <= 1)
            _anim.setBool(_fire_2_ID, _fire);
        else
            _anim.setBool(_fire_ID, _fire);
    
       
        
        CurrentAmmo--;
        fireCount = Time.time + fireFreq;
        _fireEndTime = Time.time + fireFreq;  

        if (Physics.Raycast(Shooter.instance.Camera.position,
                            SetScatter()*
                            Shooter.instance.Camera.forward,
                            out hit,
                            fireRange,
                            ~ignore))
        {
            if (hit.rigidbody != null)
            {
                Rigidbody rb = hit.rigidbody;
                rb.AddForce(-hit.normal * 1000f);
            }
            GameObject tempDecal=Instantiate(decals[Random.Range(0,decals.Length)],hit.point,Quaternion.identity);
        
            tempDecal.transform.rotation = Quaternion.LookRotation(hit.normal);
            if (hit.rigidbody != null)
            {
                tempDecal.transform.SetParent(hit.transform);
            }
        
            
            Destroy(tempDecal,15f);
            for (int i = 0; i < particles.Length; i++)
            {
                    if (particles[i].tag == hit.transform.tag)
                    {
                        GameObject tempParticle=Instantiate(particles[i],hit.point,Quaternion.LookRotation(hit.normal));
                    
                        Destroy(tempParticle,5f);
                    }
            }
            if (muzzle != null)
            muzzle.Play();
        if(shell!=null)
            shell.Play();
            setRecol();
            cameraRecoil.SetTarget();

        }
    }
    private Quaternion SetScatter()
    {
        if (MoveController.instance.isWalking)
        CurrentScatter = Quaternion.Euler(
            Random.Range(-MaxScatter.eulerAngles.x, MaxScatter.eulerAngles.x),
            Random.Range(-MaxScatter.eulerAngles.y, MaxScatter.eulerAngles.y),
            Random.Range(-MaxScatter.eulerAngles.z, MaxScatter.eulerAngles.z));

    else if (Aim)
        CurrentScatter = Quaternion.Euler(0, 0, 0);

    else
        CurrentScatter = Quaternion.Euler(
            Random.Range(-MinScatter.eulerAngles.x, MinScatter.eulerAngles.x),
            Random.Range(-MinScatter.eulerAngles.y, MinScatter.eulerAngles.y),
            Random.Range(-MinScatter.eulerAngles.z, MinScatter.eulerAngles.z));

    return CurrentScatter;
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
   public void AddAmmo (AmmoType Type, int Amount)
    {
        if (Type == AmmoType._12ga)
            _12ga += Amount;

        else if (Type == AmmoType._5_56)
            _5_56 += Amount;

        else if (Type == AmmoType._7_62)
            _7_62 += Amount;

        else if (Type == AmmoType._9mm)
            _9mm += Amount;

        else if (Type == AmmoType._45cal)
            _45cal += Amount;
    }
    private void setRecol()
    {
        float x=Random.Range(MaxRecoil.x,MinRecoil.x);
        float y=Random.Range(MaxRecoil.y,MinRecoil.y);
        CameraControl.instance.AddRecoll(x,y);
    }


    
}
