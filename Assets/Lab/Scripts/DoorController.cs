using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DoorController : MonoBehaviour
{
    [SerializeField] bool _isUseAllPiece;
    [SerializeField] bool _isUseKey;
    [SerializeField] bool _isUseConnectKey;
    [SerializeField] bool _isUseSequentiallySwitch;
    [SerializeField] bool _isUseConnectCounter;
    private bool AllFlag;
    [SerializeField] AudioClip _openSound;
    [SerializeField] AudioClip _closeSound;
    [HideInInspector] public bool IsUseAllPiece;
    [HideInInspector] public bool IsUseKey;
    [HideInInspector] public bool ConnectKeyStatus;
    [HideInInspector] public bool OnDoorSwitch;
    [HideInInspector] public bool PressedAllSequentiallySwitch;
    [HideInInspector] public bool IsConnectCounter;
    Collider2D _col;
    SpriteRenderer _spriteRenderer;
    GameObject _player;
    PlayerController _playerController;
    AudioSource _audioSource;
    // Start is called before the first frame update
    void Start()
    {
        _col = GetComponent<Collider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerController = _player.GetComponent<PlayerController>();
        _audioSource = GetComponent<AudioSource>();
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
            transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().text = _playerController.KeyCount == 0 ? "" : _playerController.KeyCount.ToString();
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
            if (!PressedAllSequentiallySwitch)
            {
                AllFlag = false;
            }
        }
        if (_isUseConnectCounter)
        {
            if (!IsConnectCounter)
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
        if (_col.enabled)
        {
            _col.enabled = false;
            //_spriteRenderer.color = Color.white;
            DOTween.To(() => _spriteRenderer.color, x => _spriteRenderer.color = x, Color.white, 0.5f);
            //_audioSource.PlayOneShot(_openSound);
            _audioSource.clip = _openSound;
            if (Vector2.Distance(_player.transform.position, transform.position) < 10)
            {
                _audioSource.Play();
            }
        }
    }
    public void DoorClose()
    {
        if (!_col.enabled)
        {
            _col.enabled = true;
            //_spriteRenderer.color = Color.black;
            DOTween.To(() => _spriteRenderer.color, x => _spriteRenderer.color = x, Color.black, 0.5f);
            //_audioSource.PlayOneShot(_closeSound);
            _audioSource.clip = _closeSound;
            if (Vector2.Distance(_player.transform.position, transform.position) < 10)
            {
                _audioSource.Play();
            }
        }
    }
}
