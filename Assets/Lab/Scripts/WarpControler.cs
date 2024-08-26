using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpControler : MonoBehaviour
{
    [SerializeField] GameObject _warpPoint;
    public bool CanWarp { get; set; } = true;
    public bool OnPiece { get; private set; }
    public bool OnStorage { get; private set; }
    WarpControler _warp;
    private void Start()
    {
        _warp = _warpPoint.GetComponent<WarpControler>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        //Debug.Log(OnPiece);
        if (collision.CompareTag("Player") && CanWarp && !collision.gameObject.GetComponent<PlayerController>().m_isMoving)
        {
            if (_warp.OnPiece && !_warp.OnStorage)
            {
                collision.gameObject.transform.position = _warpPoint.transform.position;
                _warp.CanWarp = false;
            }
        }
        if (collision.CompareTag("Piece"))
        {
            OnPiece = true;
        }
        if (collision.CompareTag("Storage"))
        {
            OnStorage = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CanWarp = true;
        }
        if (collision.CompareTag("Piece"))
        {
            OnPiece = false;
        }
        if (collision.CompareTag("Storage"))
        {
            OnStorage = false;
        }
    }
    //Not On Strage
    //Only On Piece
}
