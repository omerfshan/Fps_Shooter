using UnityEngine;

public class Weapon : MonoBehaviour, Iinteractable
{
    [SerializeField] private string _nameWeapon;
    public string _name { get => _nameWeapon; set =>_nameWeapon=value; }

    public void Interact()
    {
        WeponManager.instance.PickWeapon(_name);
    }
}
