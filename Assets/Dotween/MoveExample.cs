using UnityEngine;
using DG.Tweening;

public class MoveExample : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.DOMove(new Vector3(1, 1, 0), 10f).SetEase(Ease.OutBounce);
    }
}
