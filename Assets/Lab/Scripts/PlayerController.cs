using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float _moveTime = 1;
    /// <summary>�ړ���W�Q����R���C�_�[���������郌�C���[���w�肷��</summary>
    [SerializeField] LayerMask m_walkableLayerMask;
    /// <summary>�ړ����t���O</summary>
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
    public void Move(int x, int y, float moveTime)
    {
        //m_isMoving = false;
        // �w�肳�ꂽ�����Ɉړ��ł��邩�ǂ������肷��
        Vector2 destination = (Vector2)this.transform.position + new Vector2(x, y);
        var col = Physics2D.OverlapCircle(destination, .1f, m_walkableLayerMask);

        if (col == null&&(x!=0||y!=0))    // �ړ��\
        {
            StopAllCoroutines();
            m_isMoving = true;
            StartCoroutine(MoveRoutine(x, y, moveTime));
        }
    }

    /// <summary>
    /// �w�肳�ꂽ���΍��W�Ɋ��炩�Ɉړ�����
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="moveTime"></param>
    /// <returns></returns>
    IEnumerator MoveRoutine(int x, int y, float moveTime)
    {
        // �ړ�����v�Z����
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
