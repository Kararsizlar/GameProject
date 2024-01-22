using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Intro : MonoBehaviour
{
    public float timePerImage;
    public Sprite[] images;
    public Image image;
    public int index;
    public GameObject button;


    public IEnumerator Start()
    {
        WaitForSeconds w = new(timePerImage);
        yield return w;
        foreach (var i in images)
        {
            image.sprite = i;
            yield return w;
        }

        button.SetActive(true);
    }

    public void Load()
    {
        SceneManager.LoadScene(index);
    }
}
