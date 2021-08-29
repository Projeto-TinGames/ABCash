using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.EditorCoroutines.Editor;
using UnityEditor;
using UnityEngine.Networking;

[CustomEditor(typeof(Dialogo))]
public class EditorDialogo : Editor {
    private string url = "http://localhost:3000/tingames/abcash";

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        Dialogo dialogo = target as Dialogo;
        dialogo.questionario = EditorGUILayout.Toggle("Questionário",dialogo.questionario);
        
        if (dialogo.questionario) {
            dialogo.id = EditorGUILayout.IntField("ID",dialogo.id);
            if (GUILayout.Button("Enviar")) {
                UploadQuestionario(dialogo);
            }
        }      
    }

    public void UploadQuestionario(Dialogo dialogo) {
        EditorCoroutineUtility.StartCoroutine(UploadQuestoes(dialogo.id),this);
        EditorCoroutineUtility.StartCoroutine(CriarRespostas(dialogo.escolha.opcoes),this);
    }

    public IEnumerator UploadQuestoes(int id) {
        WWWForm form = new WWWForm();
        form.AddField("id", id);
        using (UnityWebRequest www = UnityWebRequest.Delete(url + "/questoes/" + id + "/")) {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success) {
                Debug.Log(www.error);
            }
            else {
                Debug.Log("User form upload complete!");
            }
        }

        using (UnityWebRequest www = UnityWebRequest.Post(url + "/questoes/", form)) {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success) {
                Debug.Log(www.error);
            }
            else {
                Debug.Log("User form upload complete!");
            }
        }
    }

    public IEnumerator CriarRespostas(List<Opcao> opcoes) {
        foreach (Opcao opcao in opcoes) {
            if (opcao.id != 0) {
                using (UnityWebRequest www = UnityWebRequest.Delete(url + "/respostas/" + opcao.id + "/")) {
                    yield return www.SendWebRequest();
                    if (www.result != UnityWebRequest.Result.Success) {
                        Debug.Log(www.error);
                    }
                    else {
                        Debug.Log("User form upload complete!");
                        yield return UploadRespostas(opcao);
                    }
                }
            }
            else {
                yield return DefineIdRespostas(opcao);
            }
        }
    }

    private IEnumerator DefineIdRespostas(Opcao opcao) {
        using (UnityWebRequest www = UnityWebRequest.Get(url+"/respostas_last_id/")) {
            // Request and wait for the desired page.
            yield return www.SendWebRequest();
            if (www.result != UnityWebRequest.Result.Success) {
                Debug.Log(www.error);
            }
            else {
                string fullData = www.downloadHandler.text;
                if (fullData != "null") {
                    int id = int.Parse(fullData);
                    opcao.id = id + 1;
                    yield return UploadRespostas(opcao);
                }
                else {
                    opcao.id = 1;
                    yield return UploadRespostas(opcao);
                }
            }
        }
    }

    private IEnumerator UploadRespostas(Opcao opcao) {
        WWWForm form = new WWWForm();
        form.AddField("id", opcao.id);
        form.AddField("texto", opcao.texto);

        using (UnityWebRequest www = UnityWebRequest.Post(url + "/respostas/", form)) {
            yield return www.SendWebRequest();
            if (www.result != UnityWebRequest.Result.Success) {
                Debug.Log(www.error);
            }
            else {
                Debug.Log("User form upload complete!");
            }
        }
    }
}
