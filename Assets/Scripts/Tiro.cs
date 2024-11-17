using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiro : MonoBehaviour
{
    public float velocidade = 5f;

    public float tempoDeVida = 2f; 

    void Start()
    {

        Destroy(gameObject, tempoDeVida);
    }

    void Update()
    {
        transform.Translate(Vector2.right * velocidade * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D outro)
    {
        if (outro.CompareTag("Inimigo")) 
        {
            Destroy(outro.gameObject);
            Destroy(gameObject); 
        }
    }
}
