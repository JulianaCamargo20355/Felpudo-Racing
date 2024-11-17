using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Felpudo : MonoBehaviour
{
    public float forcaPulo = 5f, velocidadeVertical = 5f, velocidadeMinima = 1f, velocidadeMaxima = 5f;
    public GameObject prefabProjetil;
    public Transform pontoDisparo;
    public float tempoEntreDisparos = 0.5f;

    private Rigidbody2D corpoRigido;
    private bool podeAtirar = false;
    private float temporizadorDisparo = 0f;

    private float[] posicoesY = { 3.79f, 0.45f, -3f };
    private int indicePosicaoAtual = 0;

    public float tempoTransicao = 0.3f; // Tempo para transitar entre as posições

    void Awake()
    {
        corpoRigido = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (podeAtirar)
        {
            temporizadorDisparo -= Time.deltaTime;
            if (temporizadorDisparo <= 0f && Input.GetKeyDown(KeyCode.Z))
            {
                Atirar();
                temporizadorDisparo = tempoEntreDisparos;
            }
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            indicePosicaoAtual = Mathf.Max(0, indicePosicaoAtual - 1); 
            StartCoroutine(MoverParaPosicao());
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            indicePosicaoAtual = Mathf.Min(posicoesY.Length - 1, indicePosicaoAtual + 1); 
            StartCoroutine(MoverParaPosicao());
        }
    }

    private IEnumerator MoverParaPosicao()
    {
        float tempoDecorrido = 0f;
        Vector3 posicaoInicial = transform.position;
        Vector3 posicaoDestino = new Vector3(transform.position.x, posicoesY[indicePosicaoAtual], transform.position.z);

        while (tempoDecorrido < tempoTransicao)
        {
            transform.position = Vector3.Lerp(posicaoInicial, posicaoDestino, tempoDecorrido / tempoTransicao);
            tempoDecorrido += Time.deltaTime;
            yield return null;
        }

        transform.position = posicaoDestino; // Garantir que a posição final seja exatamente atingida
    }

    void OnTriggerEnter2D(Collider2D outro)
    {
        if (outro.CompareTag("Banana"))
        {
            velocidadeVertical = Mathf.Max(velocidadeMinima, velocidadeVertical - 1f);
            Destroy(outro.gameObject);
        }
        else if (outro.CompareTag("Melancia"))
        {
            StartCoroutine(HabilitarDisparo(5f));
            Destroy(outro.gameObject);
        }

        if (outro.CompareTag("Inimigo"))
        {
            SceneManager.LoadScene("Lose");
        }
        else if (outro.CompareTag("Teleport"))
        {
            SceneManager.LoadScene("Win");
        }
    }

    void Atirar()
    {
        Instantiate(prefabProjetil, pontoDisparo.position, Quaternion.identity);
    }

    System.Collections.IEnumerator HabilitarDisparo(float duracao)
    {
        podeAtirar = true;
        yield return new WaitForSeconds(duracao);
        podeAtirar = false;
    }
}
