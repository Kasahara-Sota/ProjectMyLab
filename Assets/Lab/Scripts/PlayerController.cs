using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float _moveTime = 1;
    /// <summary>移動を妨害するコライダーが所属するレイヤーを指定する</summary>
    [SerializeField] LayerMask m_walkableLayerMask;
    /// <summary>移動中フラグ</summary>
    public bool m_isMoving { get; private set; } = false;
    public int KeyCount { get; set; }
    Rigidbody2D _rb;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        if (!m_isMoving)
        {
            Move(0, (int)y, _moveTime);
            Move((int)x,0,_moveTime);
        }
    }
    /// <summary>Move で指定された目的地を保存する</summary>
    Vector2Int m_destination;

    /// <summary>
    /// オブジェクトが移動中ならば true, そうでない場合は false を返す
    /// </summary>
    public bool IsMoving
    {
        get { return m_isMoving; }
    }

    /// <summary>
    /// 指定された相対座標に滑らかに移動する。指定された座標に移動できない場合は何もしない。
    /// </summary>
    /// <param name="x">移動する X 座標</param>
    /// <param name="y">移動する Y 座標</param>
    /// <param name="moveTime">何秒かけて移動するか</param>
    /// <returns>移動可能な場合は true, 移動できない場合は false</returns>
    public void Move(int x, int y, float moveTime)
    {
        //m_isMoving = false;
        // 指定された方向に移動できるかどうか判定する
        Vector2 destination = (Vector2)this.transform.position + new Vector2(x, y);
        var col = Physics2D.OverlapCircle(destination, .1f, m_walkableLayerMask);

        if (col == null&&(x!=0||y!=0))    // 移動可能
        {
            StopAllCoroutines();
            m_isMoving = true;
            StartCoroutine(MoveRoutine(x, y, moveTime));
        }
    }

    /// <summary>
    /// 指定された相対座標に滑らかに移動する
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="moveTime"></param>
    /// <returns></returns>
    IEnumerator MoveRoutine(int x, int y, float moveTime)
    {
        // 移動先を計算する
        Vector2Int origin = Vector2Int.RoundToInt(this.transform.position);
        m_destination = origin + new Vector2Int(x, y);
        Debug.Log("StartRoutine");
        while (Vector2.Distance(this.transform.position, m_destination) > float.Epsilon)
        {
            Vector2 velocity = Vector2.zero;
            this.transform.position = Vector2.MoveTowards(this.transform.position, m_destination, Time.deltaTime / moveTime);
            yield return new WaitForEndOfFrame();
        }

        m_isMoving = false;
        if (KeyCount > 0)
        {
            KeyCount--;
        }
    }
}
