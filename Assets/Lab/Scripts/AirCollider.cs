using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirCollider : MonoBehaviour
{
    public int ColCount { get; private set; } = 0;
    public int OnConnectSwitch { get; private set; } = 0;
    public int MustConnectCount { get; private set; } = 0;
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
            ColCount++;
            this.gameObject.transform.parent.GetComponent<Collider2D>().enabled = false;
        }
        else if(collision.CompareTag("ConnectKey"))
        {
            OnConnectSwitch+=2;
        }
        else if(collision.CompareTag("ConnectCounter"))
        {
            MustConnectCount += collision.gameObject.GetComponent<ConnectCounter>().ConnectCount;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Piece"))
        {
            //Debug.Log($"{this.gameObject}ÇÕ{collision.name}Ç©ÇÁExit");
            ColCount--;
            //this.gameObject.transform.parent.GetComponent<Collider2D>().enabled = true;
            if (ColCount == 0)
            {
                this.gameObject.transform.parent.GetComponent<Collider2D>().enabled = true;
            }
        }
        else if(collision.CompareTag("ConnectKey"))
        {
            OnConnectSwitch-=2;
        }
        else if (collision.CompareTag("ConnectCounter"))
        {
            MustConnectCount -= collision.gameObject.GetComponent<ConnectCounter>().ConnectCount;
        }
    }
}
