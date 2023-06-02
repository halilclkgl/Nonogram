using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Image[] healths;
    private int health = 3;
    public GameObject gameOverPanel;
    public GameObject youWinPanel;
    public void TakeDamage() 
    {
        health--;
        switch (health)
        {
            case 2:
                healths[2].gameObject.SetActive(false); break;
            case 1:
                healths[1].gameObject.SetActive(false); break;
            case 0:
                healths[0].gameObject.SetActive(false); break;
        }
        if (health<=0)
        {
            gameOverPanel.SetActive(true);
        }
    
    }
    public void RestartGame()
    {
        // Oyunu yeniden baþlatmak için mevcut sahneyi yeniden yükle
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void YouWin() 
    {
        youWinPanel.SetActive(true);
    
    }
}
