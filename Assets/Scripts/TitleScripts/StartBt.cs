using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartBt : MonoBehaviour
{
    public void PlayButten()
    {
        SceneManager.LoadScene("Play");
    }
}
