using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public abstract class NPC : MonoBehaviour, IInteractable
{
    [SerializeField] private SpriteRenderer _interactSprite;

    private Transform _PlayerTransform;

    private const float INTERAC_DISATNCE = 5f;

    private void Start()
    {
        _PlayerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }
    public void Update()
    {
        if (_interactSprite.gameObject.activeSelf && !IsWithinInteractDistance())
        {
            //turn of the sprite
            _interactSprite.gameObject.SetActive(false);
            Debug.Log("je m'active");
        }



        else if (!_interactSprite.gameObject.activeSelf && IsWithinInteractDistance())
        {
            //turn on the sprite
            _interactSprite.gameObject.SetActive(true);
            Debug.Log("je me desactive");
        }
    }

    public void Talk(InputAction.CallbackContext context)
    {
        if (context.performed && IsWithinInteractDistance())
        {
           
            //interact
            Interact();
            Debug.Log("111");
        }
    }
    public abstract void Interact();

    private bool IsWithinInteractDistance()
    {
        if (Vector2.Distance(_PlayerTransform.position, transform.position) < INTERAC_DISATNCE)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}