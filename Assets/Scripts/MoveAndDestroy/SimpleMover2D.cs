using UnityEngine;

public class SimpleMover2D : MonoBehaviour
{
    public Vector2 destino = Vector2.zero;
    public float velocidade = 3f;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;           // 2D “top-down”
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
    }

    private void FixedUpdate()
    {
        Vector2 dir = (destino - (Vector2)transform.position).normalized;
        rb.linearVelocity = dir * velocidade;
    }
}

