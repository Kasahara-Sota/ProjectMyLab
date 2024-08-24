using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyController : MonoBehaviour
{
    [SerializeField] int _walkCount = 5;
    private void Start()
    {
        transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().text = _walkCount.ToString();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")&& !collision.gameObject.GetComponent<PlayerController>().m_isMoving)
        {
            collision.gameObject.GetComponent<PlayerController>().KeyCount = _walkCount;
        }
    }
}
