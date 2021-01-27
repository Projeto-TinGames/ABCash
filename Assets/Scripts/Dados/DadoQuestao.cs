using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DadoQuestao {
    public int questao;
    public string resposta;

    public DadoQuestao (int questao, string resposta) {
        this.questao = questao;
        this.resposta = resposta;
    }
}