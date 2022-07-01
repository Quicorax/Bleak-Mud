using DG.Tweening;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveAmount;
    public void Move(Vector2 direction)
    {
        transform.DOMove(transform.position + (new Vector3(direction.x, 0, direction.y).normalized * moveAmount), 0.3f);
    }
}
