using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.Networking;


public static class SistemaSalvamento {
    public static List<string> dadosSalvar = new List<string>();

    public static void SalvarDado(string resposta) {
        dadosSalvar.Add(resposta);
    }

    /*public static void BaixarDado() {
        string caminhoSalvar = DadoJogador.instancia.nome + ".json";
        WebGLFileSaver.SaveFile(dadoSalvar, caminhoSalvar);
    }*/

}
