using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Image[] healths;
    private int health = 111;
    public GameObject gameOverPanel;
    public GameObject youWinPanel;
    public Image weWinPanel;
    public Sprite[] image;
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

    public void YouWin(int level)
    {
     
        youWinPanel.SetActive(true);
        StartCoroutine(SetWinPanelSpriteDelayed(level, 0.7f));
    }

    private IEnumerator SetWinPanelSpriteDelayed(int level, float delay)
    {
        yield return new WaitForSeconds(delay);
        weWinPanel.sprite = image[level];
    }
}
