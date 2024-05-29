using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact_object : MonoBehaviour
{

    void Update()
    {
        
 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        {
            Destroy(this.gameObject);

        }
    }

}
