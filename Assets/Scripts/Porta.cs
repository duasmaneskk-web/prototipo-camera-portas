using UnityEngine;

// Versão compatível com GerenciadorPortas.cs e Interacao.cs
// (mantém os nomes: correta, aberta, Interagir)
public class Porta : MonoBehaviour
{
    public bool correta = false;               // o GerenciadorPortas define qual é a certa
    public bool aberta = false;                // fica true quando a porta abre

    [Header("Movimento ao abrir")]
    public Vector3 direcao = Vector3.right;    // para onde ela desliza (right, left, down...)
    public float distanciaAbrir = 1.5f;        // QUANTO ela anda (em metros) — ajuste aqui
    public float velocidade = 2f;              // quão rápido ela anda

    Vector3 posicaoAberta;

    void Start()
    {
        // Calcula UMA vez onde a porta deve parar
        posicaoAberta = transform.position + direcao.normalized * distanciaAbrir;
    }

 
    public string Interagir()
    {
        if (correta)
        {
            aberta = true;
            return "Você acertou a porta!";
        }
        return "Trancada! Tente outra porta.";
    }

    void Update()
    {
      
        if (aberta)
        {
            transform.position = Vector3.MoveTowards(
                transform.position, posicaoAberta, velocidade * Time.deltaTime);
        }
    }
}