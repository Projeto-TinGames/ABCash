using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class ComunicacaoBanco : MonoBehaviour {
    public string url = "http://localhost:3000/tingames/abcash";
    public static ComunicacaoBanco instance;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
        else {
            Destroy(gameObject);
        }
    }

    public void UploadEntradas() {
        StartCoroutine(RequisitarId());
    }

    public IEnumerator RequisitarId() {
        using (UnityWebRequest www = UnityWebRequest.Get(url+"/usuarios_last_id")) {
            // Request and wait for the desired page.
            yield return www.SendWebRequest();
            if (www.result != UnityWebRequest.Result.Success) {
                Debug.Log(www.error);
            }
            else {
                int fullData = int.Parse(www.downloadHandler.text);
                int id = 0;
                if (fullData >= 0) {
                    id = fullData;
                }
                StartCoroutine(UploadUsuario(id+1));
            }
        }
    }

    private IEnumerator UploadUsuario(int id) {
        WWWForm form = new WWWForm();
        form.AddField("id", id);
        form.AddField("nome", Jogador.nome);

        using (UnityWebRequest www = UnityWebRequest.Post(url+"/usuarios", form)) {
            yield return www.SendWebRequest();
            if (www.result != UnityWebRequest.Result.Success) {
                Debug.Log(www.error);
            }
            else {
                Debug.Log("User form upload complete!");
                StartCoroutine(UploadEntradas(id));
            }
        }
    }

    private IEnumerator UploadEntradas(int id) {
        WWWForm form = new WWWForm();
        form.AddField("table", "respostas");
        form.AddField("usuarioId", id);
        form.AddField("resposta", Jogador.respostas[0]);
        for (int i = 1; i < Jogador.respostas.Count+1; i++) {
            form.AddField("questao", i);
            form.AddField("resposta", Jogador.respostas[i-1]);
            using (UnityWebRequest www = UnityWebRequest.Post(url+"/entradas", form)) {
                yield return www.SendWebRequest();
                if (www.result != UnityWebRequest.Result.Success) {
                    Debug.Log(www.error);
                }
                else {
                    Debug.Log("Answer form upload complete!");
                }
            }
        }
        Application.OpenURL("https://projetos.restinga.ifrs.edu.br/tingames");
        Application.Quit();
    }

    /*public void BaixarDado() {
         string caminhoSalvar = DadoJogador.instancia.nome + ".json";
        WebGLFileSaver.SaveFile(dadoSalvar, caminhoSalvar);
    }*/

}
