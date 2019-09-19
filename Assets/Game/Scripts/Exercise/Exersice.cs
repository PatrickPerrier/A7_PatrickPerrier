using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exersice : MonoBehaviour
{
    string pTest = "hello";
    // Start is called before the first frame update
    void Start()
    {
        print(Repeat(pTest,5));

        print(IsIsogram(pTest));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    string Repeat (string pInput, int n)
    {
        string repeatedString = "";
        for (int i = 0; i < pInput.Length; i++)
        {
            char c = pInput[i];
            for (int b = 0; b < n; b++)
            {
                repeatedString = repeatedString + c;
            }
        }
        return repeatedString;
    }

    bool IsIsogram (string pInput)
    {
        
        char current;
        for (int i = 0; i < pInput.Length; i++)
        {
            bool twoOrMore = false;
            current = pInput[i];
            for (int b = 0; b < pInput.Length; b++)
            {
         
                if (current == pInput[b])
                {
                    if (twoOrMore)
                    {
                        return false;
                    }
                    twoOrMore = true;
                }
            }
        }
        return true;
    }
    
}
