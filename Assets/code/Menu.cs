using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
  public List<CanvasGroup> canvasGroup;
    public GameObject Setting;
    public AudioSource AudioSource;
    public AudioClip AudioClip;
    public  Slider Slider;
    public CanvasGroup fadeAll;
    void Start()
    {
        foreach (CanvasGroup group in canvasGroup)
        {
            group.gameObject.SetActive(true);
          RandomInon.FadeOut(group);
        }
            
    }
  
    public void exitGame ()
    {
        Application.Quit();
    }    
    public void onsetting ()
    {
        foreach (CanvasGroup group in canvasGroup)
        {
            RandomInon.FadeIn(group);
        }
        Setting.SetActive(true);
        RandomInon.ButtonSound(AudioSource, AudioClip);
    }
   
    public void offSetting ()
    {
        foreach (CanvasGroup group in canvasGroup)
        {
           RandomInon.FadeOut(group);

        }
        Setting.SetActive(false);

        RandomInon.ButtonSound(AudioSource, AudioClip);
    }    
    public void startGame() {
        RandomInon.FadeOut(fadeAll);
        RandomInon.ButtonSound(AudioSource, AudioClip);
        SceneManager.LoadScene(1);
    }
    // Update is called once per frame
    private void FixedUpdate()
    {
        AudioSource.volume = Slider.value;
    }
}
public class chang
{
   
}
