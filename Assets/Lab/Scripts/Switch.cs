using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    [SerializeField] GameObject _Door;
    [SerializeField] GameObject[] Pieces;
    [SerializeField] GameObject _board;
    bool flag = false;
    DoorController _controller;
    SpriteRenderer _spriteRenderer;
    BoardController _boardController;
    private void Start()
    {
        _controller = _Door.GetComponent<DoorController>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _boardController = _board?.GetComponent<BoardController>();
    }
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
            _controller.IsUseAllPiece = true;
        }
        else
        {
            //Debug.Log("Close");
            _controller.IsUseAllPiece= false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            _spriteRenderer.color = Color.cyan;
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
            _spriteRenderer.color = Color.gray;
            foreach (GameObject p in Pieces)
            {
                p.GetComponent<PieceController>().OnSwitch = false;
            }
        }
        if(_boardController != null)
        {
            _controller.ConnectKeyStatus = _boardController.CheckConnect();
        }
        Check();
        DoorOpen();
    }
}
