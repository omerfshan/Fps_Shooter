using UnityEngine;

public class AmmoBox :  MonoBehaviour,Iinteractable
{
    [SerializeField] private string ItemName;
    [SerializeField] private AmmoType type;
    [SerializeField] private int Amount;
    public string _name { get => ItemName; set =>ItemName=value; }

    public void Interact()
    {
       WeponManager.instance.AddAmmo(type,Amount);
       Destroy(gameObject);
    }
}