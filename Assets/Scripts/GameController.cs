using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public bool lockCursor = true;
    [SerializeField] float delayBuffer = 3f;

    void Update ()
    {
        // pressing esc toggles between hide/show
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            lockCursor = !lockCursor;
        }
 
        Cursor.lockState = lockCursor?CursorLockMode.Locked:CursorLockMode.None;
        Cursor.visible = !lockCursor;
    }

    public void ReloadGame(){
        StartCoroutine(GamePause(delayBuffer));
    }

    IEnumerator GamePause(float delay){
        float originalTimeScale = Time.timeScale;
        Time.timeScale = 0;
        float t = 0;
        while(t<delay){
            yield return null;
            t += Time.unscaledDeltaTime;
        }
        Time.timeScale = originalTimeScale;
        SceneManager.LoadScene(0);

    }
}
