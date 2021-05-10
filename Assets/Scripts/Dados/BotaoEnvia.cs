using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class BotaoEnvia : MonoBehaviour{
    private int id;

    public void EnviarDados() {
        StartCoroutine(RequisitarId("https://projetos.restinga.ifrs.edu.br/tingames/abcash"));
    }

    IEnumerator RequisitarId(string url) {
        using (UnityWebRequest www = UnityWebRequest.Get(url+"/:usuarios_last_id")) {
            // Request and wait for the desired page.
            yield return www.SendWebRequest();
            if (www.result != UnityWebRequest.Result.Success) {
                Debug.Log(www.error);
            }
            else {
                string fullData = www.downloadHandler.text.Replace("[","");
                fullData = fullData.Replace("]","");
                Debug.Log("Received: " + fullData);
                int fullDataId = JsonUtility.FromJson<DadoId>(fullData).id;
                id = -1;
                if (fullDataId >= 0) {
                    id = fullDataId;
                }
                StartCoroutine(UploadUsuario(id+1,url));
            }
        }
    }

    IEnumerator UploadUsuario(int id,string url) {
        WWWForm form = new WWWForm();
        form.AddField("table", "usuarios");
        form.AddField("id", id);
        form.AddField("nome", DadoJogador.instancia.nome);

        using (UnityWebRequest www = UnityWebRequest.Post(url+"/", form)) {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success) {
                Debug.Log(www.error);
            }
            else {
                Debug.Log("User form upload complete!");
                StartCoroutine(UploadRespostas(id,url));
            }
        }
    }
    IEnumerator UploadRespostas(int id,string url) {
        WWWForm form = new WWWForm();
        form.AddField("table", "respostas");
        form.AddField("usuarioId", id);
        form.AddField("resposta", SistemaSalvamento.dadosSalvar[0]);
        for (int i = 1; i < SistemaSalvamento.dadosSalvar.Count+1; i++) {
            form.AddField("questao", i);
            form.AddField("resposta", SistemaSalvamento.dadosSalvar[i-1]);
            using (UnityWebRequest www = UnityWebRequest.Post(url+"/", form)) {
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
}
