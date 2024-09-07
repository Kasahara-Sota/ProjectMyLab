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
    private bool _isClick;
    bool col;
    int col2 = 0;
    int col3 = 0;
    public bool OnBoard { get; private set; } = true;
    public bool OnSwitch {  get; set; }
    Vector2 _pos;
    private void Update()
    {
        //Debug.Log(Cursor.lockState);
        if (_isClick)
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //Debug.Log(pos);
            if (col2 == 0)
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
            if(Input.GetKeyDown(KeyCode.R))
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
        if (_isClick && !OnSwitch)
        {
            return;
        }
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
    }
    public void Released()
    {
        if (!_isClick)
        {
            return;
        }
        Array.ForEach(GetComponentsInChildren<BoxCollider2D>(), x => x.size = _defaultSize);
        Cursor.lockState = CursorLockMode.None;
        if (col3 == 0)
        {
            OnBoard = false;
            if(col2 > 0)
            transform.parent.position = _pos;
        }
        else
        {
            //Debug.Log($"{col3},{transform.childCount}");
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
}
