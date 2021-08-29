using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Opcao {
    public string texto;
    public Dialogo dialogoConectado;

    [HideInInspector]public int id = 0;
}
