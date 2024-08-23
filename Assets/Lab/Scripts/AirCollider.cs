using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirCollider : MonoBehaviour
{
    int _colCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Piece"))
        {
            //Debug.Log($"{this.gameObject}ÇÕ{collision.name}Ç…Enter");
            _colCount++;
            this.gameObject.transform.parent.GetComponent<Collider2D>().enabled = false;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Piece"))
        {
            //Debug.Log($"{this.gameObject}ÇÕ{collision.name}Ç©ÇÁExit");
            _colCount--;
            //this.gameObject.transform.parent.GetComponent<Collider2D>().enabled = true;
            if (_colCount == 0)
            {
                this.gameObject.transform.parent.GetComponent<Collider2D>().enabled = true;
            }
        }
    }
}
