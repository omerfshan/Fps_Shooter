using UnityEngine;

public class WeaponSway : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform Weapon;

    [Header("Settings")]
    [SerializeField] private float Intensity = 1f;
    [SerializeField] private float SlerpSpeed = 10f;
    [SerializeField] private float AimIntensity;

    private void Update()
    {
        Sway();
    }

    void Sway()
    {
       
        float X = Input.GetAxis("Mouse X") * TotalInsensity();
        float Y = Input.GetAxis("Mouse Y") * TotalInsensity();

      
        Quaternion XRot = Quaternion.AngleAxis(-Y, Vector3.right);

        
        Quaternion YRot = Quaternion.AngleAxis(X, Vector3.up);

       
        Quaternion Rot = XRot * YRot;

      
        Weapon.localRotation = Quaternion.Slerp(
            Weapon.localRotation,
            Rot,
            Time.deltaTime * SlerpSpeed
        );
    }
    private float TotalInsensity()
    {
        if (WeponManager.instance.Aim)
        {
            return AimIntensity;
        }
        else
        {
            return Intensity; 
        }
    }
}
