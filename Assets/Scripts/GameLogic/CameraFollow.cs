using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("移动速度")]
    public float moveSpeed = 10f;

    private void Update()
    {
        if (HumanGameManager.Instance.currentHero == null)
        {
            return;
        }
        transform.position = Vector3.Lerp(transform.position,
            HumanGameManager.Instance.currentHero.position,
            Time.deltaTime * moveSpeed);
    }
}
