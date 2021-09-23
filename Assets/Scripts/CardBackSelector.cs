using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBackSelector : MonoBehaviour
{

    public GameObject highlightSquare;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        //Debug.Log(this.gameObject.name);
        Sprite ret = this.gameObject.GetComponent<SpriteRenderer>().sprite;
        GameManager.instance.UpdateCardBack(ret);

        highlightSquare.transform.position = transform.position;
    }
}
