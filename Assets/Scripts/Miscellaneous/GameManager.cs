using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Miscellaneous
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void GameOver()
        {
            Debug.Log("Game Over");
        }
    }
}
