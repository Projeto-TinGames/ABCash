using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GerenciadorDeCenas : MonoBehaviour {
    public static GerenciadorDeCenas instancia;

    public Animator transicao;
    public float tempoTransicao = 1f;

    private void Awake() {
        if (instancia == null) {
            instancia = this;
        }
        else if (instancia != this) {
            Destroy(gameObject);
        }
    }

    private void Update() {
        if (Input.GetButtonDown("PularCena")) {
            //StartCoroutine(CarregarCena(SceneManager.GetActiveScene().buildIndex + 1,false));
        }
    }

    public int GetCenaNumero() {
        return SceneManager.GetActiveScene().buildIndex;
    }

    public void FinalizarCena() {
        StartCoroutine(CarregarCena(SceneManager.GetActiveScene().buildIndex + 1,true));
    }

    IEnumerator CarregarCena(int levelIndex, bool animarTransicao) {
        if (animarTransicao) {
            transicao.SetTrigger("Start");
            yield return new WaitForSeconds(tempoTransicao);
        }
        
        SceneManager.LoadScene(levelIndex);
    }
}
