using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSwitch : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //transform.parent.GetComponent<Collider2D>().enabled = false;
            //transform.parent.GetComponent<SpriteRenderer>().color = Color.white;
            transform.parent.GetComponent<DoorController>().OnDoorSwitch = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //transform.parent.GetComponent<Collider2D>().enabled = true;
            //transform.parent.GetComponent<SpriteRenderer>().color = Color.black;
            transform.parent.GetComponent<DoorController>().OnDoorSwitch = false;
        }
    }
}
