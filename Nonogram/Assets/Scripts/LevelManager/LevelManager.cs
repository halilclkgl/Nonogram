using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GridSpawner gridSpawner;
    public Nonogram2 nonogram2;
    private int currentLevel = 0;
    public GameObject youWinPanel;
    private void Start()
    {
        StartLevel(currentLevel);

    }

    private void StartLevel(int level)
    {
        gridSpawner.StartLeveD(level);
        // Belirli bir seviyeyi ba�latmak i�in nonogram2 nesnesine seviye bilgisini g�nder
        nonogram2.StartLevel(level);
      

    }

    public void CompleteLevel()
    {
        // Seviye tamamland���nda yap�lacak i�lemler
        currentLevel++;
        Debug.Log("Level " + currentLevel + " completed!");

        // �nceki seviye nesnelerini temizle
      //  gridSpawner.ClearPreviousLevel();
        nonogram2.ClearPreviousLevel();
       
        // Bir sonraki seviyeyi ba�lat
        gridSpawner.StartLeveD(currentLevel);
        StartLevel(currentLevel);
       
        youWinPanel.SetActive(false);
    }


}
