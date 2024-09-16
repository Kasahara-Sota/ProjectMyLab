using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class Treasure : MonoBehaviour
{
    /// <summary>��Ƃ��ĕ\������X�v���C�g</summary>
    [SerializeField] Sprite m_treasureUiSprite = null;
    /// <summary>��Ƃ��ĕ\������X�v���C�g�̃T�C�Y</summary>
    [SerializeField] Vector2 m_spriteSize = new Vector2(50f, 50f);
    /// <summary>���\������p�l��</summary>
    [SerializeField] RectTransform m_treasurePanel = null;
    [SerializeField] int _treasureNumber;
    [SerializeField] int _rotateTimes;
    [SerializeField] float _rotateCompleteTime;
    [SerializeField] AudioClip _audio1;
    [SerializeField] AudioClip _audio2;
    Color _color;
    AudioSource _audioSource;
    SpriteRenderer _spriteRenderer;
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _color = _spriteRenderer.color;
        _color.a = 0;
        Debug.Log("GetComp");
    }
    /// <summary>
    /// �\�����X�V����
    /// </summary>
    /// <param name="TreasureCount">�c�@��</param>
    //public void Refresh(int TreasureCount)
    //{
    //    if (m_playerUiSprite && m_playerCounterPanel)
    //    {
    //        // �q�I�u�W�F�N�g�����ׂč폜����
    //        foreach (Transform t in m_playerCounterPanel.transform)
    //        {
    //            Destroy(t.gameObject);
    //        }

    //        // �c�@�������X�v���C�g���p�l���̎q�I�u�W�F�N�g�Ƃ��Đ�������
    //        for (int i = 0; i < TreasureCount - 1; i++)
    //        {
    //            // Image �����
    //            GameObject go = new GameObject();
    //            Image image = go.AddComponent<Image>();
    //            // Sprite ���A�T�C������
    //            image.sprite = m_playerUiSprite;
    //            // �T�C�Y��ς���
    //            RectTransform rect = go.GetComponent<RectTransform>();
    //            rect.sizeDelta = m_spriteSize;
    //            // �p�l���̎q�I�u�W�F�N�g�ɂ���
    //            go.transform.SetParent(m_playerCounterPanel.transform);
    //        }
    //    }
    //}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            SpriteRenderer sprite = GetComponent<SpriteRenderer>();
            GameObject gameObject = new GameObject();
            Image image = gameObject.AddComponent<Image>();
            // Sprite ���A�T�C������
            image.sprite = sprite.sprite;
            image.color = sprite.color;
            // �T�C�Y��ς���
            RectTransform rect = gameObject.GetComponent<RectTransform>();
            rect.sizeDelta = m_spriteSize;
            Destroy(m_treasurePanel.GetChild(_treasureNumber).gameObject);
            // �p�l���̎q�I�u�W�F�N�g�ɂ���
            gameObject.transform.SetParent(m_treasurePanel.transform);
            gameObject.transform.SetSiblingIndex(_treasureNumber);
            this.gameObject.GetComponent<Collider2D>().enabled = false;
            DOTween.To(()=> _spriteRenderer.color, c => _spriteRenderer.color = c, _color, _rotateTimes*_rotateCompleteTime).SetEase(Ease.InCirc);
            DOTween.To(() => transform.position, p => transform.position = p, new Vector3(transform.position.x, transform.position.y + 2, transform.position.z), _rotateCompleteTime).SetEase(Ease.OutQuint);
            transform.DOLocalRotate(new Vector3(0,360,0), _rotateCompleteTime,RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(_rotateTimes,LoopType.Restart).OnComplete(DestroyObj);
            //DOTween.To(() => transform.localRotation, r => transform.localRotation = r,new Vector3 (0,0,360 * _rotateTimes), _rotateCompleteTime).OnComplete(DestroyObj);
            //��
            _audioSource.PlayOneShot(_audio1);
            _audioSource.PlayOneShot(_audio2);
        }
    }
    void DestroyObj() => Destroy(this.gameObject);
}