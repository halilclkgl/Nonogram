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
        // Belirli bir seviyeyi baþlatmak için nonogram2 nesnesine seviye bilgisini gönder
        nonogram2.StartLevel(level);
      

    }

    public void CompleteLevel()
    {
        // Seviye tamamlandýðýnda yapýlacak iþlemler
        currentLevel++;
        Debug.Log("Level " + currentLevel + " completed!");

        // Önceki seviye nesnelerini temizle
      //  gridSpawner.ClearPreviousLevel();
        nonogram2.ClearPreviousLevel();
       
        // Bir sonraki seviyeyi baþlat
        gridSpawner.StartLeveD(currentLevel);
        StartLevel(currentLevel);
       
        youWinPanel.SetActive(false);
    }


}
