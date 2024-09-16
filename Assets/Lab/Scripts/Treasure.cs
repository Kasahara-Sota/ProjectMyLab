using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class Treasure : MonoBehaviour
{
    /// <summary>宝として表示するスプライト</summary>
    [SerializeField] Sprite m_treasureUiSprite = null;
    /// <summary>宝として表示するスプライトのサイズ</summary>
    [SerializeField] Vector2 m_spriteSize = new Vector2(50f, 50f);
    /// <summary>宝を表示するパネル</summary>
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
    /// 表示を更新する
    /// </summary>
    /// <param name="TreasureCount">残機数</param>
    //public void Refresh(int TreasureCount)
    //{
    //    if (m_playerUiSprite && m_playerCounterPanel)
    //    {
    //        // 子オブジェクトをすべて削除する
    //        foreach (Transform t in m_playerCounterPanel.transform)
    //        {
    //            Destroy(t.gameObject);
    //        }

    //        // 残機数だけスプライトをパネルの子オブジェクトとして生成する
    //        for (int i = 0; i < TreasureCount - 1; i++)
    //        {
    //            // Image を作る
    //            GameObject go = new GameObject();
    //            Image image = go.AddComponent<Image>();
    //            // Sprite をアサインする
    //            image.sprite = m_playerUiSprite;
    //            // サイズを変える
    //            RectTransform rect = go.GetComponent<RectTransform>();
    //            rect.sizeDelta = m_spriteSize;
    //            // パネルの子オブジェクトにする
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
            // Sprite をアサインする
            image.sprite = sprite.sprite;
            image.color = sprite.color;
            // サイズを変える
            RectTransform rect = gameObject.GetComponent<RectTransform>();
            rect.sizeDelta = m_spriteSize;
            Destroy(m_treasurePanel.GetChild(_treasureNumber).gameObject);
            // パネルの子オブジェクトにする
            gameObject.transform.SetParent(m_treasurePanel.transform);
            gameObject.transform.SetSiblingIndex(_treasureNumber);
            this.gameObject.GetComponent<Collider2D>().enabled = false;
            DOTween.To(()=> _spriteRenderer.color, c => _spriteRenderer.color = c, _color, _rotateTimes*_rotateCompleteTime).SetEase(Ease.InCirc);
            DOTween.To(() => transform.position, p => transform.position = p, new Vector3(transform.position.x, transform.position.y + 2, transform.position.z), _rotateCompleteTime).SetEase(Ease.OutQuint);
            transform.DOLocalRotate(new Vector3(0,360,0), _rotateCompleteTime,RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(_rotateTimes,LoopType.Restart).OnComplete(DestroyObj);
            //DOTween.To(() => transform.localRotation, r => transform.localRotation = r,new Vector3 (0,0,360 * _rotateTimes), _rotateCompleteTime).OnComplete(DestroyObj);
            //音
            _audioSource.PlayOneShot(_audio1);
            _audioSource.PlayOneShot(_audio2);
        }
    }
    void DestroyObj() => Destroy(this.gameObject);
}