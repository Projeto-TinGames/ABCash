using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class BotaoEnvia : MonoBehaviour{

    public void EnviarDados() {
        ComunicacaoBanco.instance.RequisitarId();
    }

}
