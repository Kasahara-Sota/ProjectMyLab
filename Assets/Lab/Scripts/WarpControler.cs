using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpControler : MonoBehaviour
{
    [SerializeField] GameObject _warpPoint;
    public bool CanWarp { get; set; } = true;
    public int OnPiece { get; private set; }
    public bool OnStorage { get; private set; }
    WarpControler _warp;
    AudioSource _audioSource;
    private void Start()
    {
        _warp = _warpPoint.GetComponent<WarpControler>();
        _audioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(OnPiece);
        if (collision.CompareTag("Player") && CanWarp)
        {
            //Debug.Log(_warp.OnPiece);
            //Debug.Log(_warp.OnStorage);
            if (_warp.OnPiece > 0 && !_warp.OnStorage)
            {
                PlayerController player = collision.gameObject.GetComponent<PlayerController>();
                player.WarpPos =_warpPoint.transform.position;
                player.OnWarpPoint = true;
                //collision.gameObject.transform.position = _warpPoint.transform.position;
                _warp.CanWarp = false;
                _audioSource.Play();
            }
        }
        if (collision.CompareTag("Piece"))
        {
            OnPiece++;
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
            OnPiece--;
        }
        if (collision.CompareTag("Storage"))
        {
            OnStorage = false;
        }
    }
    //Not On Strage
    //Only On Piece
}
