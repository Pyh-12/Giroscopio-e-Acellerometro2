using UnityEngine;

public class TouchSpawn : MonoBehaviour
{

    public GameObject[] objects;
    private int index = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private AudioSource audioSource;
    public AudioClip clip; //Som que vai tocar
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
       if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == UnityEngine.TouchPhase.Began)
            {
                Vector2 position =
                    Camera.main.ScreenToWorldPoint(touch.position);

                Collider2D hit = Physics2D.OverlapPoint(position);

                if (hit != null)
                {
                    //Tocar o som
                    if (clip != null)
                    {
                        audioSource.PlayOneShot(clip);
                    }
                    Destroy(hit.gameObject);
                }

                Instantiate(objects[index], position, Quaternion.identity);

                index = (index +1) % objects.Length;
            }
        }
    }
}
