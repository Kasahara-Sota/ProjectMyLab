using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Cinemachine;

public class Switch : MonoBehaviour
{
    [SerializeField] GameObject _Door;
    [SerializeField] GameObject[] Pieces;
    [SerializeField] GameObject _board;
    [SerializeField] GameObject _sequentiallyObject;
    [SerializeField] CinemachineVirtualCamera _virtualCamera;
    bool flag = false;
    DoorController _controller;
    SpriteRenderer _spriteRenderer;
    BoardController _boardController;
    SequentiallySwitchController _sequentiallyController;
    private void Start()
    {
        _controller = _Door != null ? _Door.GetComponent<DoorController>() : null;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _boardController = _board != null ? _board.GetComponent<BoardController>() : null;
        _sequentiallyController = _sequentiallyObject != null ? _sequentiallyObject.GetComponent<SequentiallySwitchController>() : null;
    }
    public void CheckUseAllPiece()
    {
        if (Pieces.Length > 0)
        {
            flag = true;
            foreach (GameObject p in Pieces)
            {
                //Debug.Log(p.GetComponent<PieceController>().OnBoard);
                if (p != null)
                {
                    if (p.GetComponent<PieceController>().OnBoard)
                    {
                        flag = false; break;
                    }
                }
            }
        }
    }
    public void DoorOpen()
    {
        //Debug.Log("DoorOpen");
        if (_controller != null)
        {
            if (flag)
            {
                //Debug.Log("Open");
                _controller.IsUseAllPiece = true;
            }
            else
            {
                //Debug.Log("Close");
                _controller.IsUseAllPiece = false;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _virtualCamera.Priority = 100;
            _spriteRenderer.color = Color.cyan;
            foreach (GameObject p in Pieces)
            {
                if (p != null)
                {
                    p.GetComponent<PieceController>().OnSwitch = true;
                }
            }
            if (_sequentiallyController != null)
            {
                _sequentiallyController.Check(-1);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _virtualCamera.Priority = 0;
            _spriteRenderer.color = Color.gray;
            foreach (GameObject p in Pieces)
            {
                if (p != null)
                {
                    p.GetComponent<PieceController>().OnSwitch = false;
                }
            }
        }
        if (_boardController != null)
        {
            _controller.ConnectKeyStatus = _boardController.CheckConnect();
            _controller.IsConnectCounter = _boardController._isConnectCount;
        }
        CheckUseAllPiece();
        DoorOpen();
    }
}
