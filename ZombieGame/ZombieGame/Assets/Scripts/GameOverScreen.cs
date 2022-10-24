using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    
    public void SetupScreen()
    {
        mouseLook.Instance.enabled = false;
        playerController.Instance.enabled = false;
        
        gameObject.SetActive(true);
    }

    public void RestartOnCurrentState()
    {
        playerController.Instance.enabled = true;
        mouseLook.Instance.enabled = true;

        gameObject.SetActive(false);
    }
}
