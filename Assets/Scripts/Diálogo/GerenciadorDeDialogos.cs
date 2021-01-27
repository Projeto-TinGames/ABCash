using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GerenciadorDeDialogos : MonoBehaviour {
    public static GerenciadorDeDialogos instancia;

    public TextMeshProUGUI caixaDialogo;
    public GameObject opcoes;
    private Button[] botoesOpcoes;
	private string textoAtivo;

    public Dialogo dialogo; // Lista de dialogos a serem executados
    public Queue<ElementoDeDialogo> filaElementos; // Fila de elementos a serem executados
	private List<Opcao> opcoesAtivas = new List<Opcao>();

	private bool escolhendo;

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

        int indexSentencas = 0;
        int indexEscolhas = 0;

        foreach (int element in dialogo.ordemElementos) {
            switch(dialogo.tiposElementos[element]) {
                case "Sentence":
                    filaElementos.Enqueue(dialogo.sentencas[indexSentencas]);
                    indexSentencas++;
                    break;
                case "Choice":
                    filaElementos.Enqueue(dialogo.escolhas[indexEscolhas]);
                    indexEscolhas++;
                    break;
            }
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

	public void EscolherOpcao(int indexOpcao) {
		if (escolhendo) {
			escolhendo = false;
			SistemaSalvamento.SalvarDado(GerenciadorDeCenas.instancia.GetCenaNumero()-1, opcoesAtivas[indexOpcao].text);
			if (opcoesAtivas[indexOpcao].dialogoConectado == null) {
				ExecutarProximoElemento();
				return;
			}
			ExecutarDialogo(opcoesAtivas[indexOpcao].dialogoConectado);
		}
	}

    public void AtualizarCaixaDialogo(ElementoDeDialogo element) {
		for (int i = 0; i < botoesOpcoes.Length; i++) {
			botoesOpcoes[i].gameObject.SetActive(false);
		}
	}

	public void DefinirBotoesEscolha(List<Opcao> opcoes) {
		opcoesAtivas = opcoes;
		escolhendo = true;

		for (int i = 0; i < opcoes.Count; i++) {
			botoesOpcoes[i].gameObject.active = true;
			botoesOpcoes[i].GetComponentInChildren<TextMeshProUGUI>().text = opcoes[i].text;
		}
	}
}
