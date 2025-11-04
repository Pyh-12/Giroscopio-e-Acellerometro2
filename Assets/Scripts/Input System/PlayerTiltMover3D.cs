using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerTiltMover3D : MonoBehaviour
{
    [Header("Movimento")]
    public float speed = 6f;            // m/s
    public float smoothing = 10f;       // maior = mais suave
    public float deadZone = 0.05f;      // ignora ru�do
    public bool useCameraForward = false; // se quiser alinhar ao Yaw da c�mera

    [Header("Calibra��o")]
    public bool autoCalibrateOnStart = true;
    public int calibrationSamples = 30;

    private Rigidbody rb;
    private Vector3 velTarget;
    private Vector3 velCurrent;
    private Vector3 accelZero; // offset de calibra��o

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation; // top-down
    }

    void Start()
    {
        if (autoCalibrateOnStart && Accelerometer.current != null)
            StartCoroutine(Calibrate());
    }

    System.Collections.IEnumerator Calibrate()
    {
        accelZero = Vector3.zero;
        int n = Mathf.Max(1, calibrationSamples);
        for (int i = 0; i < n; i++)
        {
            accelZero += Accelerometer.current.acceleration.ReadValue();
            yield return null;
        }
        accelZero /= n;
    }

    void FixedUpdate()
    {
        if (Accelerometer.current == null) return;

        Vector3 a = Accelerometer.current.acceleration.ReadValue() - accelZero;
        Vector2 tilt = new Vector2(a.x, -a.y); // mapeia X -> X, Y -> -Z

        if (tilt.magnitude < deadZone) tilt = Vector2.zero;
        tilt = Vector2.ClampMagnitude(tilt, 1f);

        Vector3 dirWorld;
        if (useCameraForward && Camera.main != null)
        {
            Vector3 f = Camera.main.transform.forward; f.y = 0f; f.Normalize();
            Vector3 r = Camera.main.transform.right; r.y = 0f; r.Normalize();
            dirWorld = (r * tilt.x + f * tilt.y).normalized;
        }
        else
        {
            dirWorld = new Vector3(tilt.x, 0f, tilt.y).normalized;
        }

        velTarget = dirWorld * speed;
        velCurrent = Vector3.Lerp(velCurrent, velTarget, 1f - Mathf.Exp(-smoothing * Time.fixedDeltaTime));
        rb.linearVelocity = velCurrent; // mant�m Y do rigidbody controlado pela f�sica (gravidade desligada no RB para top-down)
    }
}
