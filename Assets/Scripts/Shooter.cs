using UnityEngine;

public class Shooter : MonoBehaviour
{
   public static Shooter instance;
   public Transform Camera;
   public Transform cameraHead;
    void Awake()
    {
        instance=this;
    }
}
