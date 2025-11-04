using UnityEngine;
using UnityEngine.InputSystem;

public class AccelerometerExample : MonoBehaviour
{
    void Update()
    {
        // Verifica se o acelerômetro está disponível
        if (Accelerometer.current != null)
        {
            Vector3 acel = Accelerometer.current.acceleration.ReadValue();

            // Exemplo: mover o objeto de acordo com o eixo X e Y
            transform.Translate(acel.x, acel.y, 0);
        }
    }
}
