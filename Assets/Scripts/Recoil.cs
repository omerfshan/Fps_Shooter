using UnityEngine;

public class Recoil : MonoBehaviour
{
  
    [Header("Position Recoil")]
    [SerializeField] private Vector3 MaxTargetPos;
    [SerializeField] private Vector3 MinTargetPos;
    private Vector3 TargetPos;
    private Vector3 OriginalPos;
    private Vector3 SlideVector;
    [SerializeField] private float SlideSpeed;
    [SerializeField] private float LerpSpeed;

    [SerializeField] private Transform _target;
    private bool Lerp;

    [Header("Rotation Recoil")]
    [SerializeField] private Vector3 MaxTargetRot;  
    [SerializeField] private Vector3 MinTargetRot; 

    private Quaternion TargetRot;
    private Quaternion OriginalRot;
    private Quaternion SlideRot;
    [SerializeField] private float RotSlideSpeed = 100f;
    [SerializeField] private float RotLerpSpeed = 10f;

    private void Start()
    {
       
        OriginalPos = _target.localPosition;
        SlideVector = OriginalPos;

     
        OriginalRot = _target.localRotation;
        SlideRot = OriginalRot;
    }

    public void SetTarget()
    {
      
        TargetPos = new Vector3(
            Random.Range(MinTargetPos.x, MaxTargetPos.x),
            Random.Range(MinTargetPos.y, MaxTargetPos.y),
            Random.Range(MinTargetPos.z, MaxTargetPos.z)
        ) + OriginalPos;

       
        Vector3 randomEuler = new Vector3(
            Random.Range(MinTargetRot.x, MaxTargetRot.x),
            Random.Range(MinTargetRot.y, MaxTargetRot.y),
            Random.Range(MinTargetRot.z, MaxTargetRot.z)
        );

       
        TargetRot = OriginalRot * Quaternion.Euler(randomEuler);

        Lerp = true;
    }

    private void Update()
    {
     
        if (Lerp)
        {
            SlideVector = Vector3.MoveTowards(SlideVector, TargetPos, SlideSpeed * Time.deltaTime);
            if (SlideVector == TargetPos)
                Lerp = false;
        }
        else
        {
            SlideVector = Vector3.MoveTowards(SlideVector, OriginalPos, SlideSpeed * Time.deltaTime);
        }

        _target.localPosition = Vector3.Lerp(_target.localPosition, SlideVector, LerpSpeed * Time.deltaTime);

      
        if (Lerp)
        {
            SlideRot = Quaternion.RotateTowards(SlideRot, TargetRot, RotSlideSpeed * Time.deltaTime*7);
        }
        else
        {
            SlideRot = Quaternion.RotateTowards(SlideRot, OriginalRot, RotSlideSpeed * Time.deltaTime*7);
        }

        _target.localRotation = Quaternion.Lerp(_target.localRotation, SlideRot, RotLerpSpeed * Time.deltaTime*7);
    }
}
