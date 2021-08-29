using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GerenciadorDeDialogos : MonoBehaviour {
    public static GerenciadorDeDialogos instancia;

    public TextMeshProUGUI caixaDialogo;
    public GameObject opcoes; //Objeto que engloba as opções
    private Button[] botoesOpcoes; //Vetor das opções armazenados no objeto
	private string textoAtivo; //Texto sendo escrito pelo gerenciador

    public Dialogo dialogo; // Objeto de diálogo a ser executado
    public Queue<ElementoDeDialogo> filaElementos; // Fila de elementos a serem executados
	private List<Opcao> opcoesAtivas = new List<Opcao>(); //É definido quando um elemento de escolha é executado

	private bool escolhendo; //Previne a passagem de elementos enquanto estiver executando uma escolha

	// Acontece antes do Start
	private void Awake() {
		if (instancia == null) {
            instancia = this;
        }
        else if (instancia != this) {
            Destroy(gameObject);
        }
	}

    // Start is called before the first frame update
    void Start() {
        botoesOpcoes = opcoes.GetComponentsInChildren<Button>();
		filaElementos = new Queue<ElementoDeDialogo>();
        ExecutarDialogo(dialogo);
    }

	// Update is called once per frame
    void Update() {
        if (Input.GetMouseButtonDown(0)) {
			if (textoAtivo != null) {
				TerminarEscrever(textoAtivo);
				return;
			}
			ExecutarProximoElemento();
        }
    }

    public void ExecutarDialogo(Dialogo dialogo) {
        filaElementos.Clear();

        foreach (Sentenca sentenca in dialogo.sentencas) {
			filaElementos.Enqueue(sentenca);
		}
		if (dialogo.escolha.opcoes.Count > 0) {
			filaElementos.Enqueue(dialogo.escolha);
		}
        ExecutarProximoElemento();
	}

	public void ExecutarProximoElemento() {
		if (!escolhendo) {
			if (filaElementos.Count == 0) {
				GerenciadorDeCenas.instancia.FinalizarCena();
				return;
			}
			ElementoDeDialogo elementoAtual = filaElementos.Dequeue();
			elementoAtual.Executar();
		}
	}

	public void ComeçarEscrever(string texto) {
		textoAtivo = texto;
		StopAllCoroutines();
		StartCoroutine(Escrever(textoAtivo));
	}

	IEnumerator Escrever(string text) {
		caixaDialogo.text = "";
		foreach (char letter in text.ToCharArray()) {
			caixaDialogo.text += letter;
			yield return new WaitForSeconds(0.02f);
		}
		textoAtivo = null;
	}

	public void TerminarEscrever(string texto) {
		StopAllCoroutines();
		caixaDialogo.text = texto;
		textoAtivo = null;
	}

	public void DefinirBotoesEscolha(List<Opcao> opcoes) { //Acionado na execução de um elemento de escolha
		opcoesAtivas = opcoes;
		escolhendo = true;

		for (int i = 0; i < opcoes.Count; i++) {
			botoesOpcoes[i].gameObject.SetActive(true);
			botoesOpcoes[i].GetComponentInChildren<TextMeshProUGUI>().text = opcoes[i].texto;
		}
	}

	public void EscolherOpcao(int indexOpcao) { //Acionado no clique dos botões de execução
		if (escolhendo) {
			escolhendo = false;

			if (dialogo.questionario) {
				Jogador.SalvarResposta(opcoesAtivas[indexOpcao].id);
			}

			if (opcoesAtivas[indexOpcao].dialogoConectado == null) {
				ExecutarProximoElemento();
				return;
			}

			ExecutarDialogo(opcoesAtivas[indexOpcao].dialogoConectado);
		}
	}

    public void AtualizarCaixaDialogo(ElementoDeDialogo element) { //Apagar opções
		for (int i = 0; i < botoesOpcoes.Length; i++) {
			botoesOpcoes[i].gameObject.SetActive(false);
		}
	}
}
