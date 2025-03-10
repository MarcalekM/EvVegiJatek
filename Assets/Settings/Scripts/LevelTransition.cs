using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransition : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;

    public IEnumerator LoadLevelWithTrasition(string level)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);
        var player = FindObjectOfType<PlayerController>();
        player?.JsonSavePlayer(level);
        SceneManager.LoadScene(level, LoadSceneMode.Single);
    }
}
