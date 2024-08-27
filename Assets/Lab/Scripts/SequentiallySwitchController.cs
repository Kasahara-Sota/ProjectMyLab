using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SequentiallySwitchController : MonoBehaviour
{
    [SerializeField] GameObject[] _SequentiallySwitch;
    [SerializeField] GameObject _door;
    /// <summary>今何番目のスイッチを押すべきか？</summary>
    int _number = 1;
    void Start()
    {
        foreach (var item in _SequentiallySwitch)
        {
            item.transform.SetParent(this.gameObject.transform);
        }
    }

    public bool Check(int number)
    {
        if (number == _number)
        {
            _number++;
            return true;
        }
        else if (number == 0)
        {
            return true;
        }
        foreach (var item in _SequentiallySwitch)
        {
            item.GetComponent<SequentiallySwitch>().Pressed = false;
            item.GetComponent<SpriteRenderer>().color = item.GetComponent<SequentiallySwitch>()._defaultColor;
        }
        _number = 1;
        return false;
    }
    public void CheckPressedAll()
    {
        bool pressedAll = true;
        foreach (var item in _SequentiallySwitch)
        {
            if (!item.GetComponent<SequentiallySwitch>().Pressed)
            {
                pressedAll= false;
            }
        }
        _door.GetComponent<DoorController>().PressedAllSequentiallySwitch = pressedAll;
        Debug.Log(pressedAll);
    }
}
