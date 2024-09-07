using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
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
    /// <summary>�ړ���W�Q����R���C�_�[���������郌�C���[���w�肷��</summary>
    [SerializeField] LayerMask m_walkableLayerMask;
    /// <summary>�ړ����t���O</summary>
    public bool m_isMoving { get; private set; } = false;
    public int KeyCount { get; set; }
    public Vector2 WarpPos { get; set; }
    public bool OnWarpPoint {  get; set; } = false;
    Rigidbody2D _rb;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        // �V�[�����̂��ׂẴI�u�W�F�N�g���擾
        GameObject[] allObjects = FindObjectsOfType<GameObject>();

        // �I�u�W�F�N�g�̐���\��
        Debug.Log("�V�[�����̃I�u�W�F�N�g��: " + allObjects.Length);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("isMoving=" + m_isMoving);
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
    /// <summary>Move �Ŏw�肳�ꂽ�ړI�n��ۑ�����</summary>
    Vector2Int m_destination;

    /// <summary>
    /// �I�u�W�F�N�g���ړ����Ȃ�� true, �����łȂ��ꍇ�� false ��Ԃ�
    /// </summary>
    public bool IsMoving
    {
        get { return m_isMoving; }
    }

    /// <summary>
    /// �w�肳�ꂽ���΍��W�Ɋ��炩�Ɉړ�����B�w�肳�ꂽ���W�Ɉړ��ł��Ȃ��ꍇ�͉������Ȃ��B
    /// </summary>
    /// <param name="x">�ړ����� X ���W</param>
    /// <param name="y">�ړ����� Y ���W</param>
    /// <param name="moveTime">���b�����Ĉړ����邩</param>
    /// <returns>�ړ��\�ȏꍇ�� true, �ړ��ł��Ȃ��ꍇ�� false</returns>
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
            // �w�肳�ꂽ�����Ɉړ��ł��邩�ǂ������肷��
            Vector3 destination = (Vector3)this.transform.position + new Vector3(x, y, 0);
            var col = Physics2D.OverlapCircle(destination, .1f, m_walkableLayerMask);

            if (col == null && (x != 0 || y != 0))    // �ړ��\
            {
                //StopAllCoroutines();
                m_isMoving = true;
                //StartCoroutine(MoveRoutine(x, y, moveTime));
                //Debug.Log(destination);
                DOTween.To(() => transform.position, p => transform.position = p, destination, moveTime).OnComplete(MoveComplete);
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
        //Debug.Log($"OnWarpPoint={OnWarpPoint}");
        if(OnWarpPoint)
        {
            transform.position = WarpPos;
            OnWarpPoint = false;
        }
        _fleezeTimer = 0;
        m_isMoving = false;
        if (KeyCount > 0)
        {
            KeyCount--;
        }
    }
    /// <summary>
    /// �w�肳�ꂽ���΍��W�Ɋ��炩�Ɉړ�����
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="moveTime"></param>
    /// <returns></returns>
    //IEnumerator MoveRoutine(int x, int y, float moveTime)
    //{
    //    // �ړ�����v�Z����
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
