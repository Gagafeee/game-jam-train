using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Vector3 direction = new Vector3();

        if (Input.GetKey(KeyCode.Z))
        {
            direction.z = 1;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            direction.z = -1;
        }

        if (Input.GetKey(KeyCode.Q))
        {
            direction.x = -1;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            direction.x = 1;
        }

        rb.MovePosition(transform.position + (direction * speed * Time.deltaTime));
    }
}
