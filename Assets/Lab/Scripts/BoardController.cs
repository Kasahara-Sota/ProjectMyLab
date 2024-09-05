using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardController : MonoBehaviour
{
    int[,] Map;
    int h;
    int w;
    List<string> _counterPos = new List<string>();
    List<int> _mustConnect = new List<int>();
    public bool _isConnectCount { get; private set; }
    
    // Start is called before the first frame update
    void Start()
    {
        w = transform.GetChild(0).gameObject.transform.childCount;
        h = transform.childCount;
        Map = new int[w, h];
    }

    // Update is called once per frame
    void Update()
    {

    }
    public bool CheckConnect()
    {
        bool Connect = true;
        _isConnectCount = false;
        _counterPos.Clear();
        _mustConnect.Clear();
        for (int i = 0; i < h; i++)
        {
            for (int j = 0; j < w; j++)
            {
                AirCollider Status = transform.GetChild(i).gameObject.transform.GetChild(j).gameObject.transform.GetChild(0).GetComponent<AirCollider>();
                Map[j, i] = Status.ColCount + Status.OnConnectSwitch;
                if(Status.MustConnectCount > 0)
                {
                    string pos = $"{j},{i}";
                    _counterPos.Add(pos);
                    _mustConnect.Add(Status.MustConnectCount);
                }
                    //Debug.Log(Status.MustConnectCount);
            }
        }
        if(_mustConnect.Count > 0)
        {
            _isConnectCount = true;
        }
        //None = 0;OnlyPiece = 1;OnlyConnectKey = 2;Piece&ConnectKey = 3;Checked = 4;
        for (int i = 0; i < h; i++)
        {
            for (int j = 0; j < w; j++)
            {
                //Debug.Log($"{j},{i}={Map[j, i]}");
                if (Map[j, i] == 4)
                {
                    continue;
                }
                else if (Map[j, i] == 2 || Map[j, i] == 0)
                {
                    //Debug.Log($"{j},{i}にダイヤを発見");
                    if (!BFS(j, i, 2, 0))
                    {
                        Connect = false;
                    }
                }
                else if (Map[j, i] == 3 || Map[j, i] == 1)
                {
                    //Debug.Log($"{j},{i}にダイヤを発見");
                    if (!BFS(j, i, 3, 1))
                    {
                        Connect = false;
                    }
                }
            }
        }
        return Connect;
    }
    private bool BFS(int StartX, int StartY, int TargetNumber, int RoadNumber)
    {
        Queue<int> queue = new Queue<int>();
        int ConnectKeyCount = 0;
        queue.Enqueue(StartX);
        queue.Enqueue(StartY);
        int mustConnect = -1;
        bool isMustConnect = false;
        //Debug.Log($"{StartX},{StartY}は");
        while (queue.Count > 0)
        {
            mustConnect++;
            int gx = queue.Dequeue();
            int gy = queue.Dequeue();
            string pos = $"{gx},{gy}";
            int index = _counterPos.BinarySearch(pos);
            if (index>=0)
            {
                mustConnect -= _mustConnect[index];
                isMustConnect = true;
            }
            if (gx - 1 >= 0)
            {
                if (Map[gx - 1, gy] == RoadNumber)
                {
                    queue.Enqueue(gx - 1);
                    queue.Enqueue(gy);
                    Map[gx - 1, gy] = 4;
                }
                else if (Map[gx - 1, gy] == TargetNumber)
                {
                    ConnectKeyCount++;
                    queue.Enqueue(gx - 1);
                    queue.Enqueue(gy);
                    //Debug.Log($"{gx - 1},{gy}とコネクト");
                    Map[gx - 1, gy] = 4;
                }
            }
            if (gy - 1 >= 0)
            {
                if (Map[gx, gy - 1] == RoadNumber)
                {
                    queue.Enqueue(gx);
                    queue.Enqueue(gy - 1);
                    Map[gx, gy - 1] = 4;
                }
                else if (Map[gx, gy - 1] == TargetNumber)
                {
                    ConnectKeyCount++;
                    queue.Enqueue(gx);
                    queue.Enqueue(gy - 1);
                    //Debug.Log($"{gx},{gy - 1}とコネクト");
                    Map[gx, gy - 1] = 4;
                }
            }
            if (gx + 1 < w)
            {
                if (Map[gx + 1, gy] == RoadNumber)
                {
                    queue.Enqueue(gx + 1);
                    queue.Enqueue(gy);
                    Map[gx + 1, gy] = 4;
                }
                else if (Map[gx + 1, gy] == TargetNumber)
                {
                    ConnectKeyCount++;
                    queue.Enqueue(gx + 1);
                    queue.Enqueue(gy);
                    //Debug.Log($"{gx + 1} , {gy}とコネクト");
                    Map[gx + 1, gy] = 4;
                }
            }
            if (gy + 1 < h)
            {
                if (Map[gx, gy + 1] == RoadNumber)
                {
                    queue.Enqueue(gx);
                    queue.Enqueue(gy + 1);
                    Map[gx, gy + 1] = 4;
                }
                else if (Map[gx, gy + 1] == TargetNumber)
                {
                    ConnectKeyCount++;
                    queue.Enqueue(gx);
                    queue.Enqueue(gy + 1);
                    //Debug.Log($"{gx} , {gy + 1}とコネクト");
                    Map[gx, gy + 1] = 4;
                }
            }
        }
        Debug.Log(mustConnect);
        if(isMustConnect&&mustConnect!=0)
        {
            _isConnectCount = false;
        }
        if (ConnectKeyCount == 2 || ConnectKeyCount == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
