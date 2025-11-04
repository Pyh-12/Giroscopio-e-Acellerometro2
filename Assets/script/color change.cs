using UnityEngine;

public class colorchange : MonoBehaviour
{

    public Color[] color;
    private SpriteRenderer sprite;
    private int index = 0;
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();

        //color[0] = Color.red;
        //color[1] = Color.green;
        //color[2] = Color.blue;

        color = new Color[3];
        for (int i = 0; i < color.Length; i++)
        {
            color[i] = Random.ColorHSV();
        }

        sprite.color = color[0];
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChangeColor();
        }

        if (Input.touchCount > 0 &&
            Input.GetTouch(0).phase == UnityEngine.TouchPhase.Began)
        {
            ChangeColor();
        }
    }

    void ChangeColor()
    {
        index++;

        if (index >= color.Length)
        {
            index = 0;
        }

        sprite.color = color[index];
    }
}
