using UnityEngine;
using UnityEngine.InputSystem;

public class GyroscopeExample : MonoBehaviour
{
    void Update()
    {
        // usa o sensor de atitude (orientação completa)
        if (AttitudeSensor.current != null)
        {
            Quaternion rot = AttitudeSensor.current.attitude.ReadValue();
            transform.rotation = rot;
        }
        // alternativamente, use o Gyroscope se quiser só a velocidade angular
        else if (UnityEngine.InputSystem.Gyroscope.current != null)
        {
            Vector3 angular = UnityEngine.InputSystem.Gyroscope.current.angularVelocity.ReadValue();
            transform.Rotate(angular * Time.deltaTime, Space.Self);
        }
    }
}
