using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GreenRedController : MonoBehaviour
{
    [SerializeField]
    private string RED;

    public void setRED()
    {
         SceneManager.LoadScene (RED);
    }
}
