using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardScript : MonoBehaviour
{
    Vector3 offset;
    bool shouldFollow = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldFollow)
        {
            transform.position = (Input.mousePosition - offset) * .01f;
            transform.position = new Vector3(transform.position.x, transform.position.y, -1);
        } else
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -.01f);
        }
    }

    private void OnMouseDown()
    {
        shouldFollow = true;
        offset = Input.mousePosition - transform.position;

    }

    private void OnMouseUp()
    {
        shouldFollow = false;
    }
}
