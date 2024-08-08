using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class SlideMove : MonoBehaviour,IPointerClickHandler
{ 
    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        pos = FindAnyObjectByType<Test>().Check(int.Parse(transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().text),pos);
        DOTween.To(() => new Vector3(transform.position.x, transform.position.y, 0), x => this.transform.position = x, new Vector3(pos.x, pos.y, 0), 0.1f).SetEase(Ease.Linear);
        if(FindAnyObjectByType<Test>().Clear())
        {
            Debug.Log("Clear");
        }
    }
    Vector2 pos;
        void Start()
    {
        pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
