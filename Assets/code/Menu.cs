using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using TMPro;

public class Menu : MonoBehaviour
{
  public List<CanvasGroup> canvasGroup;
    public GameObject Setting;
    public AudioSource AudioSource;
    public AudioClip AudioClip;
    public  Slider Slider;
    public CanvasGroup fadeAll;
    int resolution;
    SaveChapter savec ;
    public CanvasGroup Continue;
    public AudioSource SongVolume;
    public AudioClip SongClip;
    public TMP_Dropdown drop;
    void Start()
    {
        DontDestroyOnLoad(SongVolume.gameObject);
        SongVolume.PlayOneShot(SongClip);
        if (save.readValue()!=null || ( save.readValue().chapter==0 && save.readValue().dong==0) )
        RandomInon.FadeOut(Continue);
        else Continue.gameObject.SetActive(false);
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
        Continue.gameObject.SetActive(false);
        Setting.SetActive(true);
        RandomInon.ButtonSound(AudioSource, AudioClip);
    }
   
    public void offSetting ()
    {
        foreach (CanvasGroup group in canvasGroup)
        {
           RandomInon.FadeOut(group);

        }
        Continue.gameObject.SetActive(true);
        Setting.SetActive(false);

        RandomInon.ButtonSound(AudioSource, AudioClip);
    }
    public void Awake()
    {
        if (save.readValue() != null)
        {
            savec = save.readValue();
           
            drop.value = savec.resolution;
            resolution = drop.value;
            Slider.value = savec.volume;

        }
        else
        {
            savec = new SaveChapter(0, 0, ChooseE.none, resolution, AudioSource.volume);
        }
        ChangeRe();
    }
    public void ChangeRe()
    {
       
      
            
            if (drop.value == 0) { Screen.SetResolution(1920, 1080, FullScreenMode.MaximizedWindow); resolution = 0; }
        else if (drop.value == 1)
        { Screen.SetResolution(1920, 1080,false);resolution = 1; }
           

        else if (drop.value == 2){ Screen.SetResolution(1280, 720, false); resolution = 2; }


    }
    public void startGame() {
       
        StartCoroutine(StartE(savec.chapter, savec. dong, savec.state, resolution,AudioSource.volume));
   
       
    }
    public IEnumerator StartE(int chapter,int dong,ChooseE state,int reso,float volume)
    {
        var e = new SaveChapter(chapter, dong, state , reso, volume);
        save.saveValue(e);
        RandomInon.ButtonSound(AudioSource, AudioClip);
        yield return new WaitUntil(() => AudioSource.isPlaying == false);

        RandomInon.FadeOutAndLoad(fadeAll, 1);
    }    
    public void StartNewGame()
    {
       
       StartCoroutine(StartE(0, 0,ChooseE.none , resolution, AudioSource.volume));
      
    }
    private void OnApplicationQuit()
    {
        var e = new SaveChapter(savec.chapter, savec.dong, savec.state, resolution,AudioSource.volume);
        save.saveValue(e);
    }
    // Update is called once per frame
    private void FixedUpdate()
    {
        AudioSource.volume = Slider.value;
        SongVolume.volume = Slider.value;
    }
}
public class chang
{
   
}
