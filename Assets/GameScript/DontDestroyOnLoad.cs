using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyOnLoad : MonoBehaviour {

    [SerializeField] private GameObject[] m_dntDestroyObjects;

    private void Awake()
    {
        for (int count = 0; count < m_dntDestroyObjects.Length; count++)
            DontDestroyOnLoad(m_dntDestroyObjects[count]);
    }
    private void Start()
    {
        SceneManager.LoadScene(1);
    }
}
