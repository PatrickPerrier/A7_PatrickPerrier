using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class color : MonoBehaviour
{
    float factor = 0;

    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        mousePos -= new Vector3(0.5f, 0.5f, 0.0f) * factor;

        gameObject.GetComponent<Renderer>().material.color = Color.Lerp(Color.red, Color.blue, mousePos.y);
    }
}
