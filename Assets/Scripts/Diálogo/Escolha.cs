using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Escolha : ElementoDeDialogo {
    public List<Opcao> opcoes = new List<Opcao>();
    
    public override void Executar() {
        GerenciadorDeDialogos.instancia.AtualizarCaixaDialogo(this);
        GerenciadorDeDialogos.instancia.DefinirBotoesEscolha(this.opcoes);
    }
}
