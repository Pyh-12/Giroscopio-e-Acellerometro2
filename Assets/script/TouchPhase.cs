using System.Diagnostics;
using TMPro;
using UnityEngine;
using System.Collections;

public class TouchPhase : MonoBehaviour
{

    public Vector2 startPos;
    public Vector2 direction;

    public TextMeshProUGUI textMeshPro;
    public string message; 

    // Update is called once per frame
    void Update()
    {
        textMeshPro.text = "Touch :" + message + " in direction " + direction;

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case UnityEngine.TouchPhase.Began:
                    startPos = touch.position;
                    message = "Began";
                    break;
                case UnityEngine.TouchPhase.Moved:
                    direction = touch.position - startPos;
                    message = "moving";
                    break;
                case UnityEngine.TouchPhase.Ended:
                    message = "ending";
                    break;
                case UnityEngine.TouchPhase.Stationary:
                    UnityEngine.Debug.Log("Touch Stationary at: " + touch.position);
                    //message = "Stationary";
                    message = "stationary";
                    break;
                case UnityEngine.TouchPhase.Canceled:
                    message = "canceled";
                    break;
            }
        }
    }
}
