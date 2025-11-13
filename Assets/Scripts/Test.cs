using UnityEngine;

public class RotateVsSetProof : MonoBehaviour
{
    [Header("Refs")]
    public Transform cubeA_Rotate; // Her kare Rotate() kullanır
    public Transform cubeB_Set;    // Yaw biriktir, doğrudan set et

    [Header("Settings")]
    public float degPerSecond = 30f; // saniyede kaç derece dönsün
    public bool useDeltaTime = true; // true: fps bağımsız

    private float yaw;   // B için topladığımız açı
    private int frames;  // Kaç frame geçti

    void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 120; // hızlı gör, fark erken çıksın
        yaw = 0f;
        frames = 0;
    }

    void Update()
    {
        float step = useDeltaTime ? degPerSecond * Time.deltaTime : degPerSecond;

        // A) Rotate: mevcut rotasyona "ekle"
        cubeA_Rotate.Rotate(Vector3.up, step, Space.Self);

        // B) Yaw-Set: açıyı biz topluyoruz, sonra "aynı anda" set ediyoruz
        yaw += step;
        cubeB_Set.localRotation = Quaternion.Euler(0f, yaw, 0f);

        frames++;
        if (frames % 600 == 0) // her ~5-10 sn’de bir raporla
        {
            // Açıları oku
            float aY = cubeA_Rotate.localEulerAngles.y;
            float bY = Mathf.Repeat(yaw, 360f);

            Debug.Log($"[Frame {frames}] A(Rotate)={aY:F6}° | B(SetYaw)={bY:F6}° | fark={Mathf.DeltaAngle(aY, bY):F6}°");
        }
    }
}
