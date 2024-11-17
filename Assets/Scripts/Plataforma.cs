using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plataforma : MonoBehaviour
{
    public float velocidade = 2f; 
    public float limiteEsquerda = -1000f; 

    void Update()
    {
        if (transform.position.x > limiteEsquerda)
        {
            transform.Translate(Vector2.left * velocidade * Time.deltaTime);
        }
    }
}
