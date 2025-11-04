using UnityEngine;
using UnityEngine.InputSystem;

public class MotionController : MonoBehaviour
{
    public float speed = 5f;

    void Update()
    {
        // --- Movimento pelo acelerômetro ---
        if (Accelerometer.current != null)
        {
            Vector3 accel = Accelerometer.current.acceleration.ReadValue();

            // Move no plano XZ (top-down)
            Vector3 move = new Vector3(accel.x, 0f, -accel.y);
            transform.Translate(move * speed * Time.deltaTime, Space.World);
        }

        // --- Rotação pelo sensor de atitude (substitui o antigo Gyroscope.attitude) ---
        if (AttitudeSensor.current != null)
        {
            Quaternion rot = AttitudeSensor.current.attitude.ReadValue();
            transform.rotation = rot;
        }
        else if (UnityEngine.InputSystem.Gyroscope.current != null)
        {
            // Caso não haja AttitudeSensor, aplica rotação incremental
            Vector3 angular = UnityEngine.InputSystem.Gyroscope.current.angularVelocity.ReadValue();
            transform.Rotate(angular * Time.deltaTime, Space.Self);
        }
    }
}
