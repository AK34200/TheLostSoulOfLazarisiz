using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    [SerializeField] Door _door;
    [SerializeField] bool _isDoorOpenSwitch;
    [SerializeField] bool _isDoorClosedSwitch;

    float _switchSizeY;
    Vector3 _switchUpPos;
    Vector3 _switchDownPos;
    float _switchSpeed = 1f;
    float _switchDelay = 0.2f;
    bool _isPressingSwitch = false;

    public Door door;
    void Awake()
    {
        _switchSizeY = transform.localScale.y / 2;

        _switchUpPos = transform.position;
        _switchDownPos = new Vector3(transform.position.x, transform.position.y + 3f, transform.position.z);
    }




    void Update()
    {
        if (_isPressingSwitch)
        {
            door.OpenDoor();
            //MoveSwitchDown();
        }
   
       
            //MoveSwitchUp();
        
    }

    /*void MoveSwitchDown()
    {
        if (transform.position != _switchDownPos)
        {
            transform.position = Vector3.MoveTowards(transform.position, _switchDownPos, _switchSpeed * Time.deltaTime);
        }
    }
    void MoveSwitchUp()
    {
        if (transform.position != _switchUpPos)
        {
            transform.position = Vector3.MoveTowards(transform.position, _switchUpPos, _switchSpeed * Time.deltaTime);
        }
    }*/

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _isPressingSwitch = !_isPressingSwitch;

            if (_isDoorOpenSwitch && !_door._isDoorOpen)
            {
                _door._isDoorOpen = !_door._isDoorOpen;
            }
            else if (_isDoorClosedSwitch && _door._isDoorOpen)
            {
                _door._isDoorOpen = !_door._isDoorOpen;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        {
            StartCoroutine(SwitchUpDelay(2));
        }
    }

    IEnumerator SwitchUpDelay(float waitTime) 
    { 
        yield return new WaitForSeconds(waitTime);
        _isPressingSwitch = false;
    }
}
