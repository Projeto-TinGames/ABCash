using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class BotaoEnvia : MonoBehaviour{
    public void EnviarDados() {
        StartCoroutine(Upload());
    }

    IEnumerator Upload() {
        WWWForm form = new WWWForm();
        form.AddField("nome", DadoJogador.instancia.nome);
        form.AddField("resposta", SistemaSalvamento.dadosSalvar[0]);
        for (int i = 1; i < SistemaSalvamento.dadosSalvar.Count+1; i++) {
            form.AddField("questao", i);
            form.AddField("resposta", SistemaSalvamento.dadosSalvar[i-1]);
            using (UnityWebRequest www = UnityWebRequest.Post("https://projetos.restinga.ifrs.edu.br/tingames/abcash", form)) {
                yield return www.SendWebRequest();

                if (www.result != UnityWebRequest.Result.Success) {
                    Debug.Log(www.error);
                }
                else {
                    Debug.Log("Form upload complete!");
                }
            }
        }
        Application.OpenURL("https://projetos.restinga.ifrs.edu.br/tingames");
        Application.Quit();
    }
}
