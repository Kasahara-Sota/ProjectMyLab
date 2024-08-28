using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ConnectCounter : MonoBehaviour
{
    [SerializeField] int _connectCount = 0;
    private void Start()
    {
        transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().text = _connectCount.ToString();
        if( _connectCount < 0 )
        {
            Debug.LogError($"{gameObject.name}‚Ì_connectCount‚ð1ˆÈã‚É‚µ‚Ä‚­‚¾‚³‚¢");
        }
    }
    public int ConnectCount 
    {
        set{_connectCount = value;}
        get{ return _connectCount;}
    }
}
