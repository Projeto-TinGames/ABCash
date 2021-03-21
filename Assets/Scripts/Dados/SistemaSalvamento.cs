using System;
using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.Networking;


public static class SistemaSalvamento {
    public static string dadoSalvar;

    public static void SalvarDado(int questao, string resposta) {
        string caminhoSalvar = DadoJogador.instancia.nome + ".json";
        
        DadoQuestao dadoQuestao = new DadoQuestao(questao, resposta);
        
        dadoSalvar += JsonUtility.ToJson(dadoQuestao, true);
        if (questao == 10) {
            Debug.Log(dadoSalvar + ", " + caminhoSalvar);
            WebGLFileSaver.SaveFile(dadoSalvar, caminhoSalvar);
        }
    }

}
