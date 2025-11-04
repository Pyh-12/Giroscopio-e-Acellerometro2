using UnityEngine;

public class TouchSpawn : MonoBehaviour
{
    // Lista de prefabs de objetos 2D que podem ser instanciados
    public GameObject[] objetos;
    private int indiceAtual = 0; // controla qual objeto da lista será instanciado

    // PARTE 2 - COLOCAR O SOM DA CABRA

    public AudioClip somRemocao; // Som a ser tocado ao remover
    private AudioSource audioSource;

    void Start()
    {
        // Pega (ou adiciona) o AudioSource no GameObject
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Update()
    {
        // Verifica se há ao menos um toque na tela
        if (Input.touchCount > 0)
        {
            // Pega as informações do primeiro toque
            Touch toque = Input.GetTouch(0);

            // Apenas quando o toque começou
            if (toque.phase == TouchPhase.Began)
            {
                // Converte a posição do toque na tela para o mundo 2D
                Vector2 posicao = Camera.main.ScreenToWorldPoint(toque.position);

                // Verifica se já existe algum objeto nessa posição
                Collider2D hit = Physics2D.OverlapPoint(posicao);

                if (hit != null)
                {

                // Toca o som antes de remover
                if (somRemocao != null) audioSource.PlayOneShot(somRemocao);

                // Remove o objeto existente
                Destroy(hit.gameObject);
                }

                // Instancia um novo objeto da lista
                Instantiate(objetos[indiceAtual], posicao, Quaternion.identity);

                // Passa para o próximo objeto na lista (ciclo)
                indiceAtual = (indiceAtual + 1) % objetos.Length;
            }
        }
    }
}
