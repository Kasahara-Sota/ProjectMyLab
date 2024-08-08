using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEditor.PlayerSettings;

public class TileController : MonoBehaviour, IPointerClickHandler
{
    public int Paint
    {
        get => paint; private set => paint = value;
    }
    private int paint = 0;
    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        Color col = this.GetComponent<SpriteRenderer>().color;
        if(col == Color.black)
        {
            paint = 0;
            col = Color.white;
            this.GetComponent<SpriteRenderer>().color = col;
        }
        else
        {
            col = Color.black;
            this.GetComponent<SpriteRenderer>().color = col;
            paint = 1;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
