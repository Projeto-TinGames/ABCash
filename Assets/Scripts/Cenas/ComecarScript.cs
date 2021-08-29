using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComecarScript : MonoBehaviour {

    public InputField input;

    public void ComecarJogo() {
        Jogador.DefineNome(input.text);
        GerenciadorDeCenas.instancia.FinalizarCena();
    }

}
