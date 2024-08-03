using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.PlayerSettings;

public class Test : MonoBehaviour
{
    [Header("パズルのタイルのプレハブ")]
    [SerializeField] GameObject _prehub;
    int _fieldSize = 5;
    int[,] Map;
    List<int> _nums = new();
    void Start()
    {
        for (int i = 1; i <= _fieldSize * _fieldSize; i++)
        {
            _nums.Add(i);
        }

        Map = new int[_fieldSize, _fieldSize];

        for (int i = 0; i < _fieldSize; i++)
        {
            for (int j = 0; j < _fieldSize; j++)
            {
                int rand = Random.Range(0, _nums.Count);
                Map[j, i] = _nums[rand];
                _nums.Remove(Map[j, i]);
                if (Map[j, i] == 25)
                {
                    continue;
                }
                GameObject obj = Instantiate(_prehub, new Vector2(j, i), Quaternion.identity);
                obj.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().text = Map[j, i].ToString();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    public Vector2 Check(int number,Vector2 posi)
    {
        int gx = 0;
        int gy = 0;
        for (int i = 0; i < _fieldSize; i++)
        {
            for (int j = 0; j < _fieldSize; j++)
            {
                if (Map[j, i] == number)
                {
                    Debug.Log($"{number}は({j},{i})");
                    gx = j;
                    gy = i;
                    i = j = _fieldSize;
                }
            }
        }
        Vector2 pos = new Vector2(gx, gy);
        if (gx - 1 >= 0 && Map[gx - 1, gy] == 25)
        {
            Map[gx - 1, gy] = number;
            Map[gx, gy] = 25;
            return new Vector2(gx - 1, gy);
        }
        if (gy - 1 >= 0 && Map[gx, gy - 1] == 25)
        {
            Map[gx, gy - 1] = number;
            Map[gx, gy] = 25;
            return new Vector2(gx, gy - 1);
        }
        if (gx + 1 < _fieldSize && Map[gx + 1, gy] == 25)
        {
            Map[gx + 1, gy] = number;
            Map[gx, gy] = 25;
            return new Vector2(gx + 1, gy);
        }
        if (gy + 1 < _fieldSize && Map[gx, gy + 1] == 25)
        {
            Map[gx, gy + 1] = number;
            Map[gx, gy] = 25;
            return new Vector2(gx, gy + 1);
        }
        pos = posi;
        return pos;
    }
}
