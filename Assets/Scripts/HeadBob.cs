using Unity.Android.Gradle.Manifest;
using Unity.VisualScripting;
using UnityEngine;

public class HeadBob:MonoBehaviour
{
    [SerializeField] private Transform Head;
    [SerializeField] private Transform HeadParent;
    [Header("Variables")]
    [SerializeField] private float BobFreq;
    [SerializeField] private float HorizontalMagnitude;
    [SerializeField] private float VerticalMagnitude;
    [SerializeField] private float LerpSpeed;
    private float walkingTime;
    private Vector3 TargetVector;
    private Vector3 OriginalOffset;

    private void Update()
    {
        SetHeadBob();
    }
    private void Start()
{
    OriginalOffset = Head.position - HeadParent.position;
}

    private Vector3 SetOffset(float Time)
    {
        float HorizontalOffset=0f;
        float VerticalOffset=0f;
        Vector3 Offset=Vector3.zero;
        if (Time > 0)
        {
            HorizontalOffset=Mathf.Cos(Time*BobFreq*MoveController.instance.MoveSpeed())*HorizontalMagnitude;
            VerticalOffset=Mathf.Sin(Time*BobFreq*2f*MoveController.instance.MoveSpeed())*VerticalMagnitude;
            Offset=HeadParent.right*HorizontalOffset+HeadParent.up*VerticalOffset;
        }
        return Offset;
    }
   private void SetHeadBob()
{
    if (!MoveController.instance.isWalking && !MoveController.instance.isRuning)
        walkingTime = 0f;
    else
        walkingTime += Time.deltaTime;

    TargetVector = HeadParent.position + OriginalOffset + SetOffset(walkingTime);

    Head.position = Vector3.Lerp(Head.position, TargetVector, LerpSpeed * Time.deltaTime);
    if ((Head.position - TargetVector).magnitude <= 0.001f)
        Head.position = TargetVector;
}

}