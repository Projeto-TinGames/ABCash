using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sentenca : ElementoDeDialogo {
    [TextArea(3,10)]public string texto;
    
    public override void Executar() {
        GerenciadorDeDialogos.instancia.AtualizarCaixaDialogo(this);
		GerenciadorDeDialogos.instancia.ComeçarEscrever(texto);
    }
}
