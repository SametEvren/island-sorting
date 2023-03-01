using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Level
{
    public class LevelLoader : MonoBehaviour
    {
        public static int Level
        {
            get => PlayerPrefs.GetInt("Level", 0);
            set => PlayerPrefs.SetInt("Level", value);
        }
        public static int LoopedLevelIndex => Level % LevelsInBuild();

        private void Start()
        {
            if (SceneManager.GetActiveScene().buildIndex != LoopedLevelIndex)
                LoadCurrentLevel();
        }

        public static void NextLevel()
        {
            Level++;
            LoadCurrentLevel();
        }

        public static void RestartLevel()
        {
            LoadCurrentLevel();
        }
        
        private static void LoadCurrentLevel()
        {
            LoadLevel(LoopedLevelIndex + 1);
        }
        
        private static void LoadLevel(int levelIndex)
        {
            SceneManager.LoadScene($"Level {levelIndex}");
        }

        private static int LevelsInBuild()
        {
            int levelCount = 0;
            for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
            {
                string levelName = SceneUtility.GetScenePathByBuildIndex(i);
                levelName = System.IO.Path.GetFileNameWithoutExtension(levelName);
                
                if (levelName.StartsWith("Level "))
                    levelCount++;
            }

            return levelCount;
        }
    }
}
