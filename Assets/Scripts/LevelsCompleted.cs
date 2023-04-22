using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelsCompleted : MonoBehaviour
{
    public List<int> CompletedList;
    // Start is called before the first frame update
    void Start() {
        DontDestroyOnLoad(gameObject);
        CompletedList = new List<int>();
    }

    public void LogLevel() {
        CompletedList.Add(SceneManager.GetActiveScene().buildIndex + 1);
        Debug.Log(CompletedList.Count);
    }
}
