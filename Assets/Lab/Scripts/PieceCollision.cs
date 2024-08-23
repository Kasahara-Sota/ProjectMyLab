using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceCollision : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log($"col{collision.name}");
        if(collision.CompareTag("Piece"))
            transform?.parent?.GetComponent<PieceController>().Collision(1);
        if (collision.CompareTag("Storage"))
        {
            transform?.parent?.GetComponent<PieceController>().OnStorage(1);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Piece"))
            transform.parent?.GetComponent<PieceController>().Collision(-1);
        if (collision.CompareTag("Storage"))
        {
            transform?.parent?.GetComponent<PieceController>().OnStorage(-1);
        }
    }
}
