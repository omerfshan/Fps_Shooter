using System;
using UnityEngine;

public class WeponVariables : MonoBehaviour
{
  public string WeponID;
  public Transform WeponParent;
  [Header("Animations")]
 public AnimationControl Animation;
  [Header("Fire Variables")]
 public float fireFreq;
 public float fireRange;
  [Header("Reload Variables")]
 public int MaxAmmo;
 public AmmoType types;
  [Header("Muzzle Flash")]
 public  ParticleSystem muzzle;
 public  ParticleSystem shell;
  [Header("Aim")]
 public  Vector3 OriginalPos;
 public  Vector3 AimPos;
 public  Quaternion OriginalRot;
 public  Quaternion AimRot;
 public  float AimSpeed;
 public  float AimFOV;
[SerializeField] float OriginalFOV;
[Header("Bullet Scatter")]
 public  Quaternion MinScatter;
 public  Quaternion MaxScatter;
     
  [Header("Recoil")]
 public  Vector2 MaxRecoil;
 public  Vector2 MinRecoil;
   


}
