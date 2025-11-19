using TMPro;
using UnityEngine;

public class InteractController:MonoBehaviour
{
    RaycastHit hit;
    [SerializeField] private float _distance;
    [SerializeField] private GameObject gtextObject;
   [SerializeField] private LayerMask ignore;

    [SerializeField] private TMP_Text text;
    void Update()
    {
        if (Physics.Raycast(Camera.main.transform.position,Camera.main.transform.forward,out hit, _distance,~ignore))
        {
            if(hit.transform.TryGetComponent<Iinteractable>(out Iinteractable i))
            {
                CrossHair.instance.Availability=false;
                gtextObject.SetActive(true);
                 text.text=i._name;
                if(Input.GetKeyDown(KeyCode.E))
                i.Interact();
            }
            else
            {
                CrossHair.instance.Availability=true;
                gtextObject.SetActive(false);
            }
        }
         else
            {
                CrossHair.instance.Availability=true;
                gtextObject.SetActive(false);
            }
    }
}