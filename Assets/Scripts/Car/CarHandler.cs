using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class CarHandler : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;

    [SerializeField] private NPCConversation myConversation;

    public float accelerationMultiplier = 3;
    public float breaksMultiplier = 15;
    public float steeringSensitivity = 0.5f; // Sensibilidade da direção

    private Vector2 input = Vector2.zero;

    private void Start()
    {
        ConversationManager.Instance.StartConversation(myConversation);
    }

    private void FixedUpdate()
    {
        // Controle de aceleração e frenagem
        if (input.y > 0)
        {
            Accelerate();
        }
        else if (input.y < 0)
        {
            Brake();
        }
        else
        {
            rb.drag = 0.2f; // Aplica um pouco de arrasto quando não há entrada de aceleração ou frenagem
        }

        // Aplica direção modificando a direção do vetor de velocidade
        if (input.x != 0)
        {
            Steer(input.x);
        }
    }

    void Accelerate()
    {
        rb.drag = 0; // Remove o arrasto durante a aceleração
        rb.AddForce(transform.forward * accelerationMultiplier * input.y, ForceMode.Acceleration);
    }

    void Brake()
    {
        // A frenagem é aplicada apenas se o carro estiver se movendo para a frente
        if (rb.velocity.magnitude > 0)
        {
            rb.AddForce(-transform.forward * breaksMultiplier * Mathf.Abs(input.y), ForceMode.Acceleration);
        }
    }

    void Steer(float direction)
    {
        // Modifica a direção do vetor de velocidade para simular a direção
        Vector3 directionVector = Quaternion.Euler(0, direction * steeringSensitivity, 0) * rb.velocity;
        rb.velocity = directionVector;
    }

    public void SetInput(Vector2 inputVector)
    {
        input = inputVector; // Atualiza o vetor de entrada diretamente
    }
}
