using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class CardMover : MonoBehaviour
{
    [Header("Options")]
    [Tooltip("Mantem o deslocamento entre o ponto tocado e o centro do objeto")]
    public bool keepOffset = true;
    public Camera cam;
    public int activeFinger = -1;
    public float screenz;
    public Vector3 dragOffset;
    [SerializeField] bool has2D; //tem collider 2D
    [SerializeField] bool has3D; //Tem collider 3D

    // >>> EVENTO PARA O MANAGER <<<
    public System.Action<SwipDecision> OnSwipeRelease;

    private void Awake()
    {
        cam = Camera.main;
        has2D = GetComponent<Collider2D>() != null;
        has3D = GetComponent<Collider>() != null;
    }

    private void OnEnable()
    {
        screenz = cam.WorldToScreenPoint
            (transform.position).z;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 0) return;

        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch touch = Input.GetTouch(i);

            //Início do arraste se inicia se o toque começou sobre ESTE objeto (objeto que esta com o script)
            if (touch.phase == UnityEngine.TouchPhase.Began &&
                activeFinger == -1 && TouchHits(touch.position))
            {
                activeFinger = touch.fingerId;
                Vector3 worldAtFinger = ScreenToWorld(touch.position);
                dragOffset = keepOffset ? (transform.position - worldAtFinger) : Vector3.zero;

            }

            //Arraste
            if (touch.phase == UnityEngine.TouchPhase.Moved || touch.phase == UnityEngine.TouchPhase.Stationary && touch.fingerId == activeFinger)
            {
                Vector3 worldAtFinger = ScreenToWorld(touch.position);
                transform.position = worldAtFinger + dragOffset;
            }

            //fim do arraste
            if (touch.fingerId == activeFinger && (touch.phase == UnityEngine.TouchPhase.Ended || touch.phase == UnityEngine.TouchPhase.Canceled))
            {
                activeFinger = -1;
            }
        }

        
    }

    bool TouchHits(Vector2 position)
    {
        if (has2D)
        {
            Vector3 world = ScreenToWorld(position);
            return Physics2D.OverlapPoint(world) == GetComponent<Collider2D>();
        }

        if (has3D)
        {
            Ray ray = cam.ScreenPointToRay(position);
            return Physics.Raycast(ray, out RaycastHit hit) && hit.collider == GetComponent<Collider>();
        }
        return false;
    }
    
    Vector3 ScreenToWorld(Vector2 position)
    {
        var screenPosition = new Vector3(position.x, position.y, screenz);
        return cam.ScreenToWorldPoint(screenPosition);
    }
    
}
