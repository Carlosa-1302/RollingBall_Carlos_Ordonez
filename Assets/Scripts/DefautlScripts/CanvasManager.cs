using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasManager : MonoBehaviour
{
    public void Play()
    {
        //SceneManager.LoadScene(1);  
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); //esto lo hace automaticamente casi para que sume la escena del Built Proyect
    }
    public void Exit()
    {
        Debug.Log("Cerrar Juego");
        Application.Quit();
    }
}