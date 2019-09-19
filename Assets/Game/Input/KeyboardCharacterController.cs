using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardCharacterController : MonoBehaviour
{
    private void Update()
    {
        Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        GetComponent<Character>().moveInputDirection = moveInput;
    }
}
