using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TiposElementos {Sentenca,Escolha};

[CreateAssetMenu(menuName = "Dialogo")]
public class Dialogo : ScriptableObject {
    public List<Sentenca> sentencas;
	public List<Escolha> escolhas;

    [System.NonSerialized]public string[] tiposElementos = new string[]{"Sentence","Choice"};
    public TiposElementos[] ordemElementos;
}
