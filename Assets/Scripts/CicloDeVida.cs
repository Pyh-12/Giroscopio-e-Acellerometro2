using UnityEngine;

public class CicloDeVida : MonoBehaviour
{
    // Chamado primeiro, antes do Start()
    // Usado para inicializar variáveis e carregar recursos
    void Awake()
    {
        Debug.Log("Awake() -> Inicializa o objeto, chamado antes do Start.");
    }

    // Chamado logo após o Awake, quando o objeto é ativado
    // Usado para preparar o objeto sempre que ele é reativado
    void OnEnable()
    {
        Debug.Log("OnEnable() -> Chamado quando o objeto é ativado.");
    }

    // Chamado uma única vez, logo no início do jogo
    // Usado para configurações iniciais
    void Start()
    {
        Debug.Log("Start() -> Executa uma vez no início do jogo.");
    }

    // Chamado várias vezes por segundo (um por frame)
    // Usado para lógicas do jogo que mudam constantemente
    void Update()
    {
        Debug.Log("Update() -> Executa a cada frame (lógica do jogo).");
    }

    // Chamado várias vezes, mas em intervalos fixos de tempo
    // Usado para física (movimentos, colisões)
    void FixedUpdate()
    {
        Debug.Log("FixedUpdate() -> Executa em intervalos fixos (física).");
    }

    // Chamado após todos os Updates() terem sido executados
    // Usado para ajustar a câmera ou ações que precisam ocorrer depois
    void LateUpdate()
    {
        Debug.Log("LateUpdate() -> Executa após o Update (ex: seguir câmera).");
    }

    // Chamado quando o objeto é desativado na cena
    // Usado para pausar sons, parar efeitos, limpar dados temporários
    void OnDisable()
    {
        Debug.Log("OnDisable() -> Objeto foi desativado.");
    }

    // Chamado quando o objeto é destruído
    // Usado para salvar dados ou liberar recursos
    void OnDestroy()
    {
        Debug.Log("OnDestroy() -> Objeto foi destruído.");
    }

    //Destruir um objeto - OBS Não é parte do ciclo de vida da UNITY
    void Destroy()
    {
        Destroy(gameObject);
    }
}
