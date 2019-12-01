using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChargementScript : MonoBehaviour
{
    private bool isLaunching = false;
    public void startLevel() {
        if (!isLaunching) {
            SceneManager.LoadSceneAsync(1);
            isLaunching = true;
        }
    }
}