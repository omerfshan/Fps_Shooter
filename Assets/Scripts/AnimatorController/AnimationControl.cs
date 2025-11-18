using UnityEngine;

public class AnimationControl : MonoBehaviour, IAnimation
{
    private Animator animator;
    [SerializeField] private WeponManager wp;

    void Awake()
    {
        animator.GetComponent<Animator>();
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
}