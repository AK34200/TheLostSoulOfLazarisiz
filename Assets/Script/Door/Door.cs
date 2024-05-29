using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool _isDoorOpen = false;
    Vector3 _doorOpenPos;
    float _doorSpeed = 10f;

    private void Awake()
    {

        _doorOpenPos = new Vector3(transform.position.x, transform.position.y + 5f, transform.position.z);
    }


    // Update is called once per frame
    void Update()
    {
      if (_isDoorOpen)
        {
            OpenDoor();
        }  


        
       
    }

    public void OpenDoor()
    {
        if (transform.position != _doorOpenPos)
        {
            transform.position = Vector3.MoveTowards(transform.position, _doorOpenPos, _doorSpeed * Time.deltaTime);
        }
    }


}
