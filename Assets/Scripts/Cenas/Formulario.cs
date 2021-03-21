using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Formulario : MonoBehaviour{
    public void AbrirFormulario() {
        SistemaSalvamento.BaixarDado();
        Application.OpenURL("https://docs.google.com/forms/d/e/1FAIpQLSf2f-mnaMh10LtwX3cZGKRHDdwi6zp-sTsSIYojJjXG6kIn5w/viewform?usp=sf_link");
        Application.Quit();
    }
}
