using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField]
    float speed = 5;

    public Vector3 moveInputDirection;

    private void Update()
    {
        transform.position += moveInputDirection * speed * Time.deltaTime;
    }
}
