using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorController : MonoBehaviour
{
    [SerializeField] GameObject _player;
    [SerializeField] bool _isUseAllPiece;
    [SerializeField] bool _isUseKey;
    [SerializeField] bool _isUseConnectKey;
    [SerializeField] bool _isUseSequentiallySwitch;
    private bool AllFlag;
    [HideInInspector] public bool IsUseAllPiece;
    [HideInInspector] public bool IsUseKey;
    [HideInInspector] public bool ConnectKeyStatus;
    [HideInInspector] public bool OnDoorSwitch;
    [HideInInspector] public bool PressedAllSequentiallySwitch;
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
        AllFlag = true;
        if (_isUseAllPiece)
        {
            if (!IsUseAllPiece)
            {
                AllFlag = false;
            }
        }
        if (_isUseKey)
        {
            transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().text = _playerController.KeyCount.ToString();
            if (_playerController.KeyCount == 0)
            {
                AllFlag = false;
            }
        }
        if (_isUseConnectKey)
        {
            if (!ConnectKeyStatus)
            {
                AllFlag = false;
            }
        }
        if (_isUseSequentiallySwitch)
        {
            if(!PressedAllSequentiallySwitch)
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
