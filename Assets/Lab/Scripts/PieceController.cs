using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PieceController : MonoBehaviour
{
    [SerializeField] bool _canRotate;
    [SerializeField] Vector2 _defaultSize;
    [SerializeField] Vector2 _clickingSize;
    [SerializeField]int _defaultOrderInLayer;
    [SerializeField] AudioClip _holdSound;
    [SerializeField] AudioClip _fitSound;
    AudioSource _audioSource;
    private bool _isClick;
    bool col;
    int col2 = 0;
    int col3 = 0;
    int col4 = 0;
    public bool OnBoard { get; private set; } = true;
    public bool OnSwitch {  get; set; }
    Vector2 _pos;
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        //Debug.Log(Cursor.lockState);
        if (_isClick)
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //Debug.Log(pos);
            if (col2 == 0 && col4 == transform.childCount)
            {
                if (col3 == 0)
                {
                 pos.x = (int)(pos.x + 0.5f);
                 pos.y = (int)(pos.y + 0.5f);
                }
            }
            //Debug.Log(pos);
            this.transform.parent.position = pos + Vector3.forward * 5;
            transform.position =new Vector3 (transform.position.x,transform.position.y,0);
            //Debug.Log(pos + Vector3.forward * 5);
            if(Input.GetKeyDown(KeyCode.R) && _canRotate)
            {
                Vector3 rotate = this.transform.parent.transform.eulerAngles;
                rotate.z -= 90;
                this.transform.parent.transform.eulerAngles = rotate;
            }
        }
        if(!OnSwitch)
        {
            Released();
        }
    }
    public void Clicked()
    {
        if (_isClick || !OnSwitch)
        {
            return;
        }
        GetComponentsInChildren<SpriteRenderer>().Where(s => s?.color != Color.black).ToList().ForEach(s => s.sortingOrder = 100); 
        Array.ForEach(GetComponentsInChildren<BoxCollider2D>(),x => x.size = _clickingSize);
        Cursor.lockState = CursorLockMode.Confined;
        GameObject obj = new GameObject();
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Debug.Log(pos);
        pos.x = (int)(pos.x + 0.5f);
        pos.y = (int)(pos.y + 0.5f);
        obj.transform.position = pos;
        transform.SetParent(obj.transform);
        _pos = transform.parent.position;
        //Debug.Log(this.gameObject.name);
        _isClick = true;
        _audioSource.PlayOneShot(_holdSound);
    }
    public void Released()
    {
        if (!_isClick)
        {
            return;
        }
        GetComponentsInChildren<SpriteRenderer>().Where(s => s?.color != Color.black).ToList().ForEach(s => s.sortingOrder = _defaultOrderInLayer);
        Array.ForEach(GetComponentsInChildren<BoxCollider2D>(), x => x.size = _defaultSize);
        Cursor.lockState = CursorLockMode.None;
        //ピース置き場に上にないとき
        if (col3 == 0)
        {
            //Debug.Log($"{col4},{transform.childCount}");
            OnBoard = false;
            if(col2 > 0 || col4 != transform.childCount)
            {
                transform.parent.position = _pos;
            }
        }
        else
        {
            if(col3 != transform.childCount)
            {
                transform.parent.position = _pos;
            }
            else
            {
                OnBoard = true;
            }
        }
        Vector2 pos = transform.position;
        pos.x = (int)(pos.x + 0.5f);
        pos.y = (int)(pos.y + 0.5f);
        transform.position = pos;
        Transform parent = transform.parent;
        transform.SetParent(null);
        Destroy(parent.gameObject);
        _isClick = false;
        _audioSource.PlayOneShot(_fitSound);
    }
    //private void OnTriggerStay2D(Collider2D collision)
    //{

    //        Debug.Log(collision.gameObject);
    //        Debug.Log("sasa");
    //        col = true;

    //}

    public void Collision(int quantity)
    {
        col2 += quantity;
    }
    public void OnStorage(int quantity)
    {
        col3 += quantity;
    }
    public void OnAir(int quantity)
    {
        col4 += quantity;
    }
}
