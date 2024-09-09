using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class KeyController : MonoBehaviour
{
    [SerializeField] int _walkCount = 5;
    Coroutine _coroutine;
    PlayerController _playerController;
    private void Start()
    {
        transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().text = _walkCount.ToString();
    }
    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Player")&& !collision.gameObject.GetComponent<PlayerController>().m_isMoving)
    //    {
    //        collision.gameObject.GetComponent<PlayerController>().KeyCount = _walkCount;
    //    }
    //}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _playerController = collision.gameObject.GetComponent<PlayerController>();
            _playerController.OnKey = true;
            StartCoroutine(IsPlayerMove());
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _playerController = collision.gameObject.GetComponent<PlayerController>();
            _playerController.OnKey = false;
        }
    }
    IEnumerator IsPlayerMove()
    {
        while(!_playerController.m_isMoving)
        {
            yield return new WaitForEndOfFrame();
        }
        _playerController.KeyCount = _walkCount;
    }
}
