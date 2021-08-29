using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Dialogo")]
public class Dialogo : ScriptableObject {
    [HideInInspector]public bool questionario;
    [HideInInspector]public int id;
    public List<Sentenca> sentencas;
	public Escolha escolha;
}
