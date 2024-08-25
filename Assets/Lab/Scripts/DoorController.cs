using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorController : MonoBehaviour
{
    [SerializeField] GameObject _player;
    [SerializeField] bool _isUseAllPiece;
    [SerializeField] bool _isUseKey;
    private bool AllFlag;
    public bool IsUseAllPiece;
    public bool IsUseKey;
    public bool OnDoorSwitch;
    Collider2D _col;
    SpriteRenderer _spriteRenderer;
    PlayerController _playerController;
    // Start is called before the first frame update
    void Start()
    {
        _col = GetComponent<Collider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _playerController = _player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().text = _playerController.KeyCount.ToString();
        AllFlag = true;
        if(_isUseAllPiece)
        {
            if(!IsUseAllPiece)
            {
                AllFlag = false;
            }
        }
        if (_isUseKey)
        {
            if (_playerController.KeyCount == 0)
            {
                AllFlag = false;
            }
        }
        if (AllFlag || OnDoorSwitch)
        {
            DoorOpen();
        }
        else
        {
            DoorClose();
        }
    }
    public void DoorOpen()
    {
        _col.enabled = false;
        _spriteRenderer.color = Color.white;
    }
    public void DoorClose()
    {
        _col.enabled = true;
        _spriteRenderer.color = Color.black;
    }
}