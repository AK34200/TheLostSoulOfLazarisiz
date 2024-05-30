using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour
{
    [SerializeField] float bossSpeed = 4f;
    [SerializeField] Rigidbody2D bossRb; // Assigné automatiquement
    Transform target;
    Vector2 moveDirection;

    private void Awake()
    {
        bossRb = GetComponent<Rigidbody2D>(); // Assigner le rigidbody du boss automatiquement
    }

    private void Start()
    {
        target = GameObject.Find("Entrée temple").transform; // Nom du joueur dans la hiérarchie
    }

    void Update()
    {
        if (target)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            moveDirection = direction;
        }
    }

    private void FixedUpdate()
    {
        if (target)
        {
            bossRb.velocity = new Vector2(moveDirection.x, moveDirection.y) * bossSpeed;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);  // Lorsque le joueur est touché la scène reload
        }
    }
}