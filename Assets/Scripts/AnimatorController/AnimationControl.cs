using UnityEngine;

public class AnimationControl : MonoBehaviour, IAnimation
{
   [SerializeField] private Animator animator;
    [SerializeField] private WeponManager wp;

    void Awake()
    {
        
    }
    public void setBool(string ID, bool setControl)
    {
      animator.SetBool(ID,setControl);
    }

    public void setTrigger(string ID)
    {
        animator.SetTrigger(ID);
    }
    public void StartFire()
    {
        wp.StartFire();
    }
    public  void EndFire()
    {
        wp.EndFire();
    }
    public  void StartReload()
    {
       wp.StartReload();
    }
    public  void EndReload()
    {
        wp.EndReload();
    }
    public void WeponDown()
    {
        wp.WeponDown();
    }
     public void SetAvailability(int index)
    {
        wp.Availability=index==0?false:true;
    }
}