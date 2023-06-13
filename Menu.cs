using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
   [Header("All Menu's")]
   public GameObject pauseMenuUI;
   public GameObject Speedometer;
   public static bool GameIsStopped = false;

   public void Resume()
   {
      pauseMenuUI.SetActive(false);
      Speedometer.SetActive(true);
      Time.timeScale = 1f;
      GameIsStopped = false;
   }

   public void Pause()
   {
      pauseMenuUI.SetActive(true);
      Speedometer.SetActive(false);
      Time.timeScale = 0f;
      GameIsStopped = true;  
   }

   public void Day()
   {
      SceneManager.LoadScene("scene_day");
      Time.timeScale = 1f;
   }

   public void LoadMenu()
   {
      SceneManager.LoadScene("Garage");
      Time.timeScale = 1f;
   }

   public void QuitGame()
   {
      Debug.Log("Quitting Game...");
      Application.Quit();
   }

      public void night()
   {
      SceneManager.LoadScene("scene_night");
      Time.timeScale = 1f;
   }

         public void Rainy()
   {
      SceneManager.LoadScene("scene_overcast");
      Time.timeScale = 1f;
   }

     public void Restartday()
   {
      SceneManager.LoadScene("scene_day");
      Time.timeScale = 1f;
   }
     public void Restartnight()
   {
      SceneManager.LoadScene("scene_night");
      Time.timeScale = 1f;
   }
     public void Resatartraining()
   {
      SceneManager.LoadScene("scene_overcast");
      Time.timeScale = 1f;
   }
}

