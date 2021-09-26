using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardScript : MonoBehaviour
{
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
            Vector3 tmp = Input.mousePosition;
            tmp = Camera.main.ScreenToWorldPoint(new Vector3(tmp.x, tmp.y, Camera.main.nearClipPlane));
            transform.position = new Vector3(tmp.x, tmp.y, -2);
            GameManager.instance.updateGrabbedCard(this.gameObject);
        }
    }

    private void OnMouseDown()
    {
        shouldFollow = true;
        transform.localScale = new Vector3(.8f, .8f, 1);
        GameManager.instance.setGrabbedCard(this.gameObject);
    }

    private void OnMouseUp()
    {
        shouldFollow = false;
        transform.localScale = new Vector3(1, 1, 1);
        GameManager.instance.removeGrabbedCard();
    }

}
