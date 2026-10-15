using UnityEngine;
using TMPro;

// Vai na Main Camera. Lança um raio (Raycast) para frente e
// detecta se o jogador está olhando para uma porta.
public class Interacao : MonoBehaviour
{
    public float distancia = 2.5f;          // alcance do braço
    public TextMeshProUGUI textoDica;       // "Aperte E para abrir"
    public TextMeshProUGUI textoMensagem;   // "Trancada!" / "Você acertou!"

    void Update()
    {
        textoDica.text = "";

        Ray raio = new Ray(transform.position, transform.forward);

        // Desenha o raio na janela Scene (só para visualizar, não aparece no jogo)
        Debug.DrawRay(raio.origin, raio.direction * distancia, Color.red);

        if (Physics.Raycast(raio, out RaycastHit acerto, distancia))
        {
            // O raio bate na folha (filha); a Porta está no objeto pai
            Porta porta = acerto.collider.GetComponentInParent<Porta>();

            if (porta != null && !porta.aberta)
            {
                textoDica.text = "Aperte E para abrir";

                if (Input.GetKeyDown(KeyCode.E))
                {
                    string mensagem = porta.Interagir();
                    if (mensagem != "")
                    {
                        textoMensagem.text = mensagem;

                        // Mensagem de erro some depois de 2 segundos;
                        // a de vitória fica na tela.
                        CancelInvoke(nameof(LimparMensagem));
                        if (!porta.correta)
                            Invoke(nameof(LimparMensagem), 2f);
                    }
                }
            }
        }
    }

    void LimparMensagem()
    {
        textoMensagem.text = "";
    }
}