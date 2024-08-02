using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    [Header("パズルのタイルのプレハブ")]
    [SerializeField] GameObject _prehub;
    int _fieldSize = 5;
    int[,] _PazzleField;
    List<int> _nums = new ();
    void Start()
    {
        for (int i = 1; i <= _fieldSize * _fieldSize; i++)
        {
            _nums.Add(i);
        }

        _PazzleField = new int[_fieldSize, _fieldSize];

        for (int i = 0; i < _fieldSize; i++)
        {
            for (int j = 0; j < _fieldSize; j++)
            {
                int rand = Random.Range(0, _nums.Count);
                _PazzleField[j, i] = _nums[rand];
                _nums.Remove(_PazzleField[j, i]);
                if(_PazzleField[j, i] == 25)
                {
                    continue;
                }
                GameObject obj = Instantiate(_prehub,new Vector2(j,i),Quaternion.identity);
                obj.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().text = _PazzleField[j, i].ToString();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
