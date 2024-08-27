using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SequentiallySwitch : MonoBehaviour
{
    public int _switchNumber = 0;
    [HideInInspector] public bool Pressed = false;
    SpriteRenderer _spriteRenderer;
    [HideInInspector] public Color _defaultColor;
    [SerializeField] Color _pressedColor;
    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _defaultColor = _spriteRenderer.color;
        Debug.Log($"{this.gameObject.name}._switchNumber{_switchNumber}");
        if (_switchNumber != 0)
        {
            transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().text = _switchNumber.ToString();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if(Pressed && _switchNumber == 0)
            {
                Pressed = transform.parent.GetComponent<SequentiallySwitchController>().Check(-1);
            }
            else
            {
                Pressed = transform.parent.GetComponent<SequentiallySwitchController>().Check(_switchNumber);
            }
            transform.parent.GetComponent<SequentiallySwitchController>().CheckPressedAll();
            if(Pressed)
            {
                _spriteRenderer.color = _pressedColor;
            }
            else
            {
                _spriteRenderer.color = _defaultColor;
            }
            Debug.Log($"{this.gameObject.name}.Pressed={Pressed}");
        }
    }
}
