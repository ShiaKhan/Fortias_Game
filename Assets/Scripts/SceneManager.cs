using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenemanager : MonoBehaviour
{

    public void LoadSceneByName(string sceneName)
    {
        SceneManager.LoadScene("LucDia");
        Debug.Log("da bam vao scene Luc Dia!!!");
        SceneManager.LoadScene("AnhHung");
        Debug.Log("Da bam vao scence Anh hung");
        SceneManager.LoadScene("ThiTran");
        Debug.Log("Da bam vao sence Thi tran");
        SceneManager.LoadScene("KhuVuc");
    }

    public void LoadSceneByIndex(int index)
    {
        SceneManager.LoadScene(index);
    }

}
