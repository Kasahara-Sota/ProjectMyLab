using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardController : MonoBehaviour
{
    int[,] Map;
    int h;
    int w;
    // Start is called before the first frame update
    void Start()
    {
        w = transform.GetChild(0).gameObject.transform.childCount;
        h = transform.childCount;
        Map = new int [w,h];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public bool CheckConnect()
    {
        bool Connect = true;
        for (int i = 0; i < h; i++)
        {
            for (int j = 0; j < w; j++)
            {
                AirCollider Status = transform.GetChild(i).gameObject.transform.GetChild(j).gameObject.transform.GetChild(0).GetComponent<AirCollider>();
                Map[j, i] = Status.ColCount + Status.OnConnectSwitch;
            }
        }
        //None = 0;OnlyPiece = 1;OnlyConnectKey = 2;Piece&ConnectKey = 3;Checked = 4;
        for(int i = 0;i<h;i++)
        {
            for(int j = 0;j < w;j++)
            {
                if (Map[j,i] == 4)
                {
                    continue;
                }
                else if(Map[j,i] == 2)
                {
                    if(!BFS(j,i,2,0))
                    {
                        i = h;
                        j = w;
                        Connect = false;
                    }
                }
                else if( Map[j,i] == 3)
                {
                    if(!BFS(j,i,3,1))
                    {
                        i = h;
                        j = w;
                        Connect = false;
                    }
                }
            }
        }
        return Connect;
    }
    private bool BFS(int StartX,int StartY,int TargetNumber,int RoadNumber)
    {
        Queue<int> queue = new Queue<int>();
        int ConnectKeyCount = 1;
        queue.Enqueue(StartX);
        queue.Enqueue(StartY);
        Map[StartX,StartY] = 4;
        while (queue.Count > 0)
        {
            int gx = queue.Dequeue();
            int gy = queue.Dequeue();
            if(gx - 1 >= 0)
            {
                if(Map[gx - 1, gy] == RoadNumber)
                {
                    queue.Enqueue(gx - 1);
                    queue.Enqueue(gy);
                }
                else if (Map[gx-1,gy] == TargetNumber)
                {
                    ConnectKeyCount++;
                    queue.Enqueue(gx - 1);
                    queue.Enqueue(gy);
                }
                Map[gx - 1, gy] = 4;
            }
            if(gy - 1 >= 0)
            {
                if (Map[gx, gy - 1] == RoadNumber)
                {
                    queue.Enqueue(gx);
                    queue.Enqueue(gy - 1);
                }
                else if (Map[gx, gy - 1] == TargetNumber)
                {
                    ConnectKeyCount++;
                    queue.Enqueue(gx);
                    queue.Enqueue(gy - 1);
                }
                Map[gx, gy - 1] = 4;
            }
            if(gx + 1 < w)
            {
                if (Map[gx + 1, gy] == RoadNumber)
                {
                    queue.Enqueue(gx + 1);
                    queue.Enqueue(gy);
                }
                else if (Map[gx + 1, gy] == TargetNumber)
                {
                    ConnectKeyCount++;
                    queue.Enqueue(gx + 1);
                    queue.Enqueue(gy);
                }
                Map[gx + 1, gy] = 4;
            }
            if(gy + 1 < h)
            {
                if (Map[gx, gy + 1] == RoadNumber)
                {
                    queue.Enqueue(gx);
                    queue.Enqueue(gy + 1);
                }
                else if (Map[gx, gy + 1] == TargetNumber)
                {
                    ConnectKeyCount++;
                    queue.Enqueue(gx);
                    queue.Enqueue(gy + 1);
                }
                Map[gx, gy + 1] = 4;
            }
        }
        if(ConnectKeyCount == 2)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
