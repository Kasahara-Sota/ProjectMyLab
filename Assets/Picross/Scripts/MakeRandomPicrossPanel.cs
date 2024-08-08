using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MakeRandomPicrossPanel : MonoBehaviour
{
    [Header("パズルのタイルのプレハブ")]
    [SerializeField] GameObject _TilePrehub;
    [SerializeField] GameObject _numberPrehub;
    [Header("パネルのサイズ")]
    [SerializeField, Range(3, 7)] int _fieldSize = 5;
    int[,] Map;
    public GameObject[,] _panel;
    float _fixSet;
    void Start()
    {
        _fixSet = (float)_fieldSize / 2 - 0.5f;

        Map = new int[_fieldSize, _fieldSize];
        _panel =new GameObject[_fieldSize, _fieldSize];
        int count = 0;
        for (int i = 0; i < _fieldSize; i++)
        {
            int count1 = 0;
            for (int j = _fieldSize-1; j >=0 ; j--)
            {
                int rand = Random.Range(0, 2);
                GameObject obj1 = Instantiate(_TilePrehub, new Vector2(j - _fixSet, i - _fixSet), Quaternion.identity);
                obj1.transform.SetParent(transform);
                _panel[j,i] = obj1;
                //obj1.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().text = rand.ToString();
                if (rand == 1)
                {
                    //obj1.GetComponent<SpriteRenderer>().color = Color.black;
                    count++;
                    if(j==0&&count!=0)
                    {
                        count1--;
                        GameObject obj = Instantiate(_numberPrehub, new Vector2(count1 - _fixSet, i - _fixSet), Quaternion.identity);
                        obj.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().text = count.ToString();
                        count = 0;
                    }
                }
                else 
                {
                    if (count == 0)
                    {
                        if(j==0&&count1==0)
                        {
                            count1--;
                            GameObject obj = Instantiate(_numberPrehub, new Vector2(count1 - _fixSet, i - _fixSet), Quaternion.identity);
                            obj.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().text = count.ToString();
                            count = 0;
                        }
                    }
                    else
                    {
                        count1--;
                        GameObject obj = Instantiate(_numberPrehub, new Vector2(count1 - _fixSet, i - _fixSet), Quaternion.identity);
                        obj.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().text = count.ToString();
                        count = 0;
                    }
                        
                }
                Map[j, i] = rand;
            }
        }
        for (int j = 0; j < _fieldSize; j++)
        {
            int count1 = -1;
            for (int i = 0; i < _fieldSize; i++)
            {
                if (Map[j,i] == 1)
                {
                    count++;
                    if (i == _fieldSize - 1 && count != 0)
                    {
                        count1++;
                        GameObject obj = Instantiate(_numberPrehub, new Vector2(j - _fixSet, _fieldSize + count1 - _fixSet), Quaternion.identity);
                        obj.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().text = count.ToString();
                        count = 0;
                    }
                }
                else 
                {
                    if (count != 0)
                    {
                        count1++;
                        GameObject obj = Instantiate(_numberPrehub, new Vector2(j - _fixSet, _fieldSize + count1 - _fixSet), Quaternion.identity);
                        obj.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().text = count.ToString();
                        count = 0;
                    }
                    else if(i==_fieldSize - 1 && count1 == -1)
                    {
                        count1++;
                        GameObject obj = Instantiate(_numberPrehub, new Vector2(j - _fixSet, _fieldSize + count1 - _fixSet), Quaternion.identity);
                        obj.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().text = count.ToString();
                    }
                }
            }
        }
    }
    public void Check()
    {
        bool _isclear = true;
        for (int i = 0; i < _fieldSize; i++)
        {
            for (int j = 0; j < _fieldSize; j++)
            {
                if (Map[j, i] == 1)
                {
                    if(_panel[j, i].GetComponent<TileController>().Paint != 1)
                    {
                        _isclear = false;
                        i = _fieldSize;
                        break; 
                    }
                }
                else
                {
                    if (_panel[j, i].GetComponent<TileController>().Paint != 0)
                    {
                        _isclear = false; 
                        i = _fieldSize;
                        break;
                    }
                }
            }
        }
        if(_isclear)
        {
            Debug.Log("Clear");
        }
        else
        {
            Debug.Log("Miss");
        }
    }
}
