using UnityEngine;

// Precisa de um Character Controller no mesmo objeto (para não atravessar paredes)
[RequireComponent(typeof(CharacterController))]
public class MovimentoJogador : MonoBehaviour
{
    public float velocidade = 3f;
    public float sensibilidade = 2f;
    public Transform cameraJogador;

    private Animator animator;
    private CharacterController controle;
    private float rotacaoVertical = 0f;

    void Start()
    {
        animator = GetComponent<Animator>();
        controle = GetComponent<CharacterController>();

        // Esconde e trava o mouse no centro da tela
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // --- Olhar com o mouse ---
        float mouseX = Input.GetAxis("Mouse X") * sensibilidade;
        float mouseY = Input.GetAxis("Mouse Y") * sensibilidade;

        transform.Rotate(0f, mouseX, 0f);

        rotacaoVertical -= mouseY;
        rotacaoVertical = Mathf.Clamp(rotacaoVertical, -80f, 80f);
        cameraJogador.localRotation = Quaternion.Euler(rotacaoVertical, 0f, 0f);

        // --- Andar na direção para onde está olhando ---
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 direcao = transform.right * h + transform.forward * v;
        if (direcao.magnitude > 1f) direcao.Normalize();

        // Move respeitando colisões (não atravessa paredes nem portas)
        controle.Move(direcao * velocidade * Time.deltaTime);

        animator.SetFloat("Velocidade", direcao.magnitude);

        // ESC solta o mouse
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}