using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    [SerializeField] GameObject _Door;
    [SerializeField] GameObject[] Pieces;
    bool flag = false;
    public void Check()
    {
        flag = true;
        foreach (GameObject p in Pieces)
        {
            //Debug.Log(p.GetComponent<PieceController>().OnBoard);
            if(p.GetComponent<PieceController>().OnBoard)
            {
                flag = false; break;
            }
        }
    }
    public void DoorOpen()
    {
        Debug.Log("DoorOpen");
        if(flag)
        {
            //Debug.Log("Open");
            _Door.gameObject.GetComponent<Collider2D>().enabled = false;
            _Door.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        }
        else
        {
            //Debug.Log("Close");
            _Door.gameObject.GetComponent<Collider2D>().enabled = true;
            _Door.gameObject.GetComponent<SpriteRenderer>().color = Color.black;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            foreach(GameObject p in Pieces)
            {
                p.GetComponent<PieceController>().OnSwitch = true;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            foreach (GameObject p in Pieces)
            {
                p.GetComponent<PieceController>().OnSwitch = false;
            }
        }
        Check();
        DoorOpen();
    }
}
