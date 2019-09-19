using UnityEngine;

public class AiCharacterController : MonoBehaviour
{
    private float direction = 1;

    private void Start()
    {
        InvokeRepeating("Switch", 2.0f, 2.0f);
    }

    private void Update()
    {
        GetComponent<Character>().moveInputDirection = new Vector3(direction, 0, 0);
    }

    private void Switch()
    {
        direction *= -1;
    }
}
