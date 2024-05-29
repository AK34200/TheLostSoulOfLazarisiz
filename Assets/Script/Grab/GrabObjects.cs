using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;

using UnityEngine.InputSystem;

public class GrabObjects : MonoBehaviour 
{
    [SerializeField]
    private Transform grabPoint;

    [SerializeField]
    private Transform raypoint;
    [SerializeField]
    private float rayDistance;


    [SerializeField] private BoxCollider2D BoxPlayer;
    private BoxCollider2D boxgrab;
    private GameObject grabbedObject;
    private int layerIndex;

   
    [SerializeField] private SpriteRenderer sp;
    [SerializeField] private LayerMask Object;

    private void Start()
    {
        layerIndex = LayerMask.NameToLayer("Objects");
        sp = GetComponent<SpriteRenderer>();
       
    }

    void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(raypoint.position, sp.flipX ? -transform.right : transform.right, rayDistance, Object);
    }



    public void Grab (InputAction.CallbackContext context)
    {

        RaycastHit2D hitInfo = Physics2D.Raycast(raypoint.position, sp.flipX ? -transform.right : transform.right, rayDistance, Object);

           
            //grab object
            if (context.performed && grabbedObject == null)
            {
            Debug.Log("Get");
                grabbedObject = hitInfo.collider.gameObject;
                grabbedObject.GetComponent<Rigidbody2D>().isKinematic = true;
                boxgrab = grabbedObject.GetComponent<BoxCollider2D>();
                boxgrab.enabled = false;
                BoxPlayer.enabled = true;
                grabbedObject.transform.position = grabPoint.position;
                grabbedObject.transform.SetParent(transform);
            }
            //release object
            else if(context.canceled)
            {
            Debug.Log("Releqse");
            grabbedObject.GetComponent<Rigidbody2D>().isKinematic = false; // Le kinematic se désactive pour remettre en dynamique comme prévu, mais Unity ne trouve plus cette ligne et la 53 : on ne peut plus attraper l'objet pour une raison inconnue
            grabbedObject.transform.SetParent(null);
                grabbedObject = null;
                boxgrab.enabled = true;
                BoxPlayer.enabled = false;
            boxgrab = null;
            }
        



        Debug.DrawRay(raypoint.position, sp.flipX ? -transform.right * 10 : transform.right * 10);

    }
}

