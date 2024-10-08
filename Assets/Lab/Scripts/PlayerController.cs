using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float _moveTime = 1;
    [SerializeField] float _fleezeMove = 0.1f;
    private enum Pri
    {
        None,
        priVer,
        priHori
    }
    Pri _movePri = Pri.None;
    float _fleezeTimer = 0;
    /// <summary>移動を妨害するコライダーが所属するレイヤーを指定する</summary>
    [SerializeField] LayerMask m_walkableLayerMask;
    /// <summary>移動中フラグ</summary>
    public bool m_isMoving { get; private set; } = false;
    public bool OnKey { get; set; } = false;
    public int KeyCount { get; set; }
    public Vector2 WarpPos { get; set; }
    public bool OnWarpPoint {  get; set; } = false;
    private Vector2 _ResetPos;
    private bool _onPuzzleSwitch;
    Rigidbody2D _rb;
    AudioSource _audioSource;
    // Start is called before the first frame update
    void Start()
    {
        _ResetPos = transform.position;
        _rb = GetComponent<Rigidbody2D>();
        // シーン内のすべてのオブジェクトを取得
        GameObject[] allObjects = FindObjectsOfType<GameObject>();
        _audioSource = GetComponent<AudioSource>();
        // オブジェクトの数を表示
        Debug.Log("シーン内のオブジェクト数: " + allObjects.Length);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("isMoving=" + m_isMoving);
        if(!m_isMoving && Input.GetKeyDown(KeyCode.R))
        {
            transform.position = _ResetPos;
            KeyCount = 0;
            return;
        }
        if(Input.GetMouseButton(0) || Input.GetMouseButton(1) || Input.GetMouseButton(2))
        {
            _fleezeTimer = 0;
            return;
        }
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        if (!m_isMoving && _fleezeTimer > _fleezeMove)
        {
            if(_movePri == Pri.priHori)
            {
                if (Move((int)x, 0, _moveTime))
                {
                    _movePri = Pri.priVer;
                }
                else if(Move(0, (int)y, _moveTime))
                {
                    _movePri = Pri.priHori;
                }
                
            }
            else
            {
                if (Move(0, (int)y, _moveTime))
                {
                    _movePri = Pri.priHori;
                }
                else if(Move((int)x, 0, _moveTime))
                {
                    _movePri = Pri.priVer;
                }
                
            }
        }
        _fleezeTimer += Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("PuzzleSwitch"))
        {
            _onPuzzleSwitch = true;
            _ResetPos = collision.transform.position;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PuzzleSwitch"))
        {
            _onPuzzleSwitch = false;
        }
    }
    /// <summary>Move で指定された目的地を保存する</summary>
    Vector2Int m_destination;

    /// <summary>
    /// 指定された相対座標に滑らかに移動する。指定された座標に移動できない場合は何もしない。
    /// </summary>
    /// <param name="x">移動する X 座標</param>
    /// <param name="y">移動する Y 座標</param>
    /// <param name="moveTime">何秒かけて移動するか</param>
    /// <returns>移動可能な場合は true, 移動できない場合は false</returns>
    private bool Move(int x, int y, float moveTime)
    {
        //m_isMoving = false;
        if (x == 0 && y == 0)
        {
            _movePri = Pri.None;
            return false;
        }
        else
        {
            // 指定された方向に移動できるかどうか判定する
            Vector3 destination = (Vector3)this.transform.position + new Vector3(x, y, 0);
            var col = Physics2D.OverlapCircle(destination, .1f, m_walkableLayerMask);

            if (col == null && (x != 0 || y != 0))    // 移動可能
            {
                //StopAllCoroutines();
                m_isMoving = true;
                //StartCoroutine(MoveRoutine(x, y, moveTime));
                //Debug.Log(destination);
                _audioSource.Play();
                DOTween.To(() => transform.position, p => transform.position = p, destination, moveTime).SetEase(Ease.Linear).OnComplete(MoveComplete);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    private void MoveComplete()
    {
        if (KeyCount > 0 && !OnKey)
        {
            KeyCount--;
        }
        //Debug.Log($"OnWarpPoint={OnWarpPoint}");
        if(OnWarpPoint)
        {
            transform.position =new Vector3(WarpPos.x,WarpPos.y,transform.position.z);
            OnWarpPoint = false;
        }
        _audioSource.Stop();
        m_isMoving = false;
    }
    /// <summary>
    /// 指定された相対座標に滑らかに移動する
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="moveTime"></param>
    /// <returns></returns>
    //IEnumerator MoveRoutine(int x, int y, float moveTime)
    //{
    //    // 移動先を計算する
    //    Vector2Int origin = Vector2Int.RoundToInt(this.transform.position);
    //    m_destination = origin + new Vector2Int(x, y);
    //    //Debug.Log("StartRoutine");
    //    while (Vector2.Distance(this.transform.position, m_destination) > float.Epsilon)
    //    {
    //        Vector2 velocity = Vector2.zero;
    //        this.transform.position = Vector2.MoveTowards(this.transform.position, m_destination, Time.deltaTime / moveTime);
    //        yield return new WaitForEndOfFrame();
    //    }
    //    _fleezeTimer = 0;
    //    m_isMoving = false;
    //    if (KeyCount > 0)
    //    {
    //        KeyCount--;
    //    }
    //}
}
