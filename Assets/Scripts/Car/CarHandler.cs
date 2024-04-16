using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarHandler : MonoBehaviour
{
    [SerializeField]
    Rigidbody rb;

    float accelerationMultiplier = 3;
    float breaksMultiplier = 15;
    float steeringMultiplier = 5;

    Vector2 input = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (input.y > 0)
        {
            Accelerate();
        }
        else 
        {
            rb.drag = 0.2f;
        }

        if ( input.y < 0)
        {
            Brake();
        }
    }

    void Accelerate()
    {
        rb.drag = 0;
        rb.AddForce(transform.forward * accelerationMultiplier * input.y);
    }

    void Brake()
    {
        if (rb.velocity.z <= 0)
        {
            return;
        }

        rb.AddForce(rb.transform.forward * breaksMultiplier * input.y);
    }

    void Steer()
    {
        if (Mathf.Abs(input.x) < 0)
        {
            rb.AddForce(rb.transform.right * steeringMultiplier * input.x);
        }
    }

    public void SetInput(Vector2 inputVector)
    {
        inputVector.Normalize();

        input = inputVector;
    }
}
