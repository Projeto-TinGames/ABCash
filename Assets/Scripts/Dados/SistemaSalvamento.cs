using System;
using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.Networking;


public static class SistemaSalvamento {
    private static string dadoSalvar;

    public static void SalvarDado(int questao, string resposta) {
        DadoQuestao dadoQuestao = new DadoQuestao(questao, resposta);
        dadoSalvar += JsonUtility.ToJson(dadoQuestao, true);
    }

    public static void BaixarDado() {
        string caminhoSalvar = DadoJogador.instancia.nome + ".json";
        WebGLFileSaver.SaveFile(dadoSalvar, caminhoSalvar);
    }

}
