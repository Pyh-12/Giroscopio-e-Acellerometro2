using UnityEngine;

public class FixSpriteRender : MonoBehaviour
{
    public float targetHeight = 3f; // altura desejada em unidades

    void OnEnable()
    {
        var spriteRender = GetComponent<SpriteRenderer>();
        if (!spriteRender || !spriteRender.sprite) return;
        float size = targetHeight / spriteRender.sprite.bounds.size.y; // bounds já considera PPU
        transform.localScale = new Vector3(size, size, 1f);
    }
}
