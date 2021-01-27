using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DadoJogador : MonoBehaviour {
    public static DadoJogador instancia;
    public string nome = "Resposta";

    private void Awake() {
        if (instancia == null) {
            instancia = this;
            DontDestroyOnLoad(instancia);
        }
        else if (instancia != this) {
            Destroy(gameObject);
        }
    }

    public void DefineNome(string nomeInput) {
        nome = nomeInput;
    }
}
