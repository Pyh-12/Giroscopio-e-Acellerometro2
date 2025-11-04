using UnityEngine;
using System.Collections;

public class SpawnerLoop : MonoBehaviour
{
    [Header("Prefabs para sortear")]
    public GameObject[] prefabs;

    [Header("Pontos de spawn (fora da tela)")]
    public Transform[] spawnPoints;

    [Header("Loop")]
    public float intervalo = 1.5f; // tempo entre spawns (s)

    [Header("Destino para onde os objetos vão (ex.: centro da tela)")]
    public Vector2 destino = Vector2.zero;

    private void Start()
    {
        StartCoroutine(LoopSpawn());
    }

    private IEnumerator LoopSpawn()
    {
        while (true)
        {
            Spawn();
            yield return new WaitForSeconds(intervalo);
        }
    }

    private void Spawn()
    {
        if (prefabs.Length == 0 || spawnPoints.Length == 0) return;

        var prefab = prefabs[Random.Range(0, prefabs.Length)];
        var ponto = spawnPoints[Random.Range(0, spawnPoints.Length)];

        var go = Instantiate(prefab, ponto.position, Quaternion.identity);

        // Garante um mover simples para entrar na tela
        var mover = go.GetComponent<SimpleMover2D>();
        if (mover == null) mover = go.AddComponent<SimpleMover2D>();
        mover.destino = destino;
    }
}
