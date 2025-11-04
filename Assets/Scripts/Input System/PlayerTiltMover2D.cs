using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerTiltMover2D : MonoBehaviour
{
    public float speed = 6f;      // unidades/s
    public float smoothing = 10f;
    public float deadZone = 0.05f;

    public bool autoCalibrateOnStart = true;
    public int calibrationSamples = 30;

    private Rigidbody2D rb;
    private Vector3 accelZero; // offset calibra��o
    private Vector2 vel, velTarget;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f; // top-down 2D
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
        Vector2 tilt = new Vector2(a.x, a.y); // top-down 2D usa XY direto

        if (tilt.magnitude < deadZone) tilt = Vector2.zero;
        tilt = Vector2.ClampMagnitude(tilt, 1f);

        velTarget = tilt.normalized * speed;
        vel = Vector2.Lerp(vel, velTarget, 1f - Mathf.Exp(-smoothing * Time.fixedDeltaTime));
        rb.linearVelocity = vel;
    }
}
