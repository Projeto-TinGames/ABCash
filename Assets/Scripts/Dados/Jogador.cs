using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Jogador {
    public static string nome = "Resposta";

    public static List<int> respostas = new List<int>();

    public static void DefineNome(string nomeInput) {
        nome = nomeInput;
    }

    public static void SalvarResposta(int resposta) {
        respostas.Add(resposta);
    }
}
