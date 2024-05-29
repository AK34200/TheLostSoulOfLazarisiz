using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    [SerializeField] float jump = 5f;
    [SerializeField] float NbJump = 2;
    [SerializeField] bool IsGrounded;
    [SerializeField] private Rigidbody2D rb;

    private void Update()
    {

        if (IsGrounded == true || NbJump > 0)
        {

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("jump");
                rb.AddForce(Vector3.up * jump);
                NbJump = NbJump - 1;

            }
        }
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            IsGrounded = true;
            NbJump = 2;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            IsGrounded = false;
        }
    }

}

