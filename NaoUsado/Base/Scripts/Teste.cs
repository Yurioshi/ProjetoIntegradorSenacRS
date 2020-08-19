using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teste : MonoBehaviour
{
    public bool endHost = false;
    public bool endUnhost = false;
    
    public void Teste1()
    {
        Debug.Log("Sucesso.host");
        endHost = true;
    }

    public void Teste2()
    {
        Debug.Log("Sucesso.Unhost");
        endUnhost = true;
    }

    
}
