using System;
using Unity.VisualScripting;
using UnityEngine;

public class CrossHair : MonoBehaviour
{
  public static CrossHair instance;
  [SerializeField] private RectTransform Crooshair;
  [SerializeField] private float MaxSize;
  [SerializeField] private float MinSize;
  [SerializeField] private float CurrentSize;
  [SerializeField] private float Speed;
  public bool Availability;
    void Awake()
    {
        instance=this;
    }
    void Update()
    {
        bool walking = MoveController.instance.isWalking;
        bool running = MoveController.instance.isRuning;

        if (running||!Availability||WeponManager.instance.Aim)
        {
            SetDeactive();
            return;      
        }
        
        SetActive();

        if (walking)
        {
            SetMax();
        }
        else
        {
            SetMin();
        }

        SetSize();
    }
    private void SetSize()
    {
        Crooshair.sizeDelta=new Vector2(CurrentSize,CurrentSize);
    }


   private void SetMin()
  {
    CurrentSize=Mathf.Lerp(CurrentSize,MinSize,Speed*Time.deltaTime);
  }

  private void SetMax()
  { 
    CurrentSize=Mathf.Lerp(CurrentSize,MaxSize,Speed*Time.deltaTime);    
  }
  private void SetActive()
  {
    Crooshair.gameObject.SetActive(true); 
  }
  private void SetDeactive()
  {
    Crooshair.gameObject.SetActive(false); 
  }


}
