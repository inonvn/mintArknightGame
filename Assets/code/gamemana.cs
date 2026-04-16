using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.WebSockets;
using System.Xml.Linq;

using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class gamemana : MonoBehaviour
{
    public static gamemana gamemna;
    public List<Story> stories;
    public AudioClip PressButton;
    public GameObject Choose;
    public GameObject textBox;
    public TextMeshProUGUI text;
    public TextMeshProUGUI Name;
    public Image imgNV;
    public Image background;
    public AudioSource source;
    public AudioSource BackGroundMusic;
    public int chapter;
    public CanvasGroup UIsetting;

    public GameObject settingButton;
    public Slider Music;
    public CanvasGroup UILog;
    public GameObject HistoryLine;
    public GameObject LogButton;
    public GameObject MapButton;
    public GameObject Map;
    public ChooseO choo;
    public GameObject UIchoose;
    public bool ItCanClick;
    bool ittyping;
   int dong;
    int resolution;
  public   ChooseE choose;

    public CanvasGroup FadeBlack;
    public CanvasGroup ToBeComtrinue;
    public void Awake()
    {
        BackGroundMusic = GameObject.Find("BackGroundMusic").GetComponent<AudioSource>();
        gamemna = this;
        if (save.readValue() != null)
        {
            var e = save.readValue();
            print(e.volume);
            dong = e.dong;
            chapter = e.chapter;
            choose = e.state;
            resolution = e.resolution;
            Music.value = e.volume;
            
        }
        else
        {
            dong = 0;
            chapter = 0;
            choose = ChooseE.none;
            resolution = 0;
        }
        ChangeRe(resolution);
    }
    public void storyTime ()
    {
        var e = stories.Max(o => o.StoryChap);
        print(e);
        if (chapter <= e)
        {
            if (ittyping == false && ItCanClick == true)
            {
                StartCoroutine(texte());

            }
            else
            {
                SkipLine();
            }
        }
        else
        {
            showEndTamThoi();
        }

    }
    public void onSetting ()
    {
        RandomInon.ButtonSound(source, PressButton);
        UIsetting.gameObject.SetActive(true);
        RandomInon.FadeOut(UIsetting);
        LogButton.SetActive(false);
        MapButton.SetActive(false);
      

    }    
    public void ChangeRe (int i)
    {
        if (i == 0) Screen.SetResolution(1920, 1080, FullScreenMode.MaximizedWindow);
       else if (i == 1)
      
            Screen.SetResolution(1920, 1080, FullScreenMode.Windowed);
       
        else if (i==2) Screen.SetResolution(1280, 720, FullScreenMode.Windowed);


    }    
    public void offSsetting ()
    {
        RandomInon.ButtonSound(source, PressButton);
        RandomInon.FadeIn(UIsetting);
      
        LogButton.SetActive(true);
        MapButton.SetActive(true);
    }
    public void GotoMenu ()
    {
        RandomInon.ButtonSound(source, PressButton);
        SaveOrDelete();
        RandomInon.FadeOutAndLoad(FadeBlack,0);
       
    }    
    int nu = 0;
    public void onLogE()
    {
        if (UILog.gameObject.activeSelf != true) onlogE1();
        else { offLogE(); }

       

    }
    public void onlogE1()
    {
        RandomInon.ButtonSound(source, PressButton);
        UILog.gameObject.SetActive(true);
        RandomInon.FadeOut(UILog);
        MapButton.SetActive(false);
        settingButton.SetActive(false);
        var r = UILog.transform.Find("ShowLogE").GetComponent<Transform>();
        while (nu < dong)

        {
            var e = Instantiate(HistoryLine, r);
            var f1 = e.transform.Find("name").GetComponent<TextMeshProUGUI>();
            var f2 = e.transform.Find("Dia").GetComponent<TextMeshProUGUI>();
            if (st.Diabl[nu].state == state.changeNV)
            {
                f1.SetText(st.Diabl[nu].NameStory);
            }
            else { f1.SetText(""); }
            f2.SetText(st.Diabl[nu].TextStory);
            nu++;
        }
    }

    public void offLogE()
    {
        RandomInon.ButtonSound(source, PressButton);
        RandomInon.FadeIn(UILog);
        UILog.gameObject.SetActive(false);
        MapButton.SetActive(true);
        settingButton.SetActive(true);

    }
    
    public void SkipStory()
    {
        
      

    }
   
    Story st;

    bool OnNV;

    public void checkSound()
    {
        
        switch (st.Diabl[dong].soundType)
        {
            case CheckTypeSound.none:
                return;
                case CheckTypeSound.BackGroundMusic:
                {
                    BackGroundMusic.Stop();
                    BackGroundMusic.PlayOneShot(st.Diabl[dong].backGroundMusic);
                    break;
                }
            case CheckTypeSound.SFX:
                {
                    source.PlayOneShot(st.Diabl[dong].clip);
                    break;
                }
        }
    }
    IEnumerator texte()
    {


      
            st = stories.Where(o => o.StoryChap == chapter && o.chooseE == choose).FirstOrDefault();
            if (dong < st.Diabl.Count())
            {



                text.SetText("");
                switch (st.Diabl[dong].state)
                {
                    case state.None:
                        {
                        if (OnNV) imgNV.gameObject.SetActive(true);
                        else imgNV.gameObject.SetActive(false);
                        checkSound();
                      
                            foreach (var e in st.Diabl[dong].TextStory)
                            {
                                text.text += e;
                                yield return new WaitForSeconds(0.01f);
                                //if (ittyping == true)
                                //{
                                //    SkipLine();
                                //    break;
                                //}
                                ittyping = true;
                            }
                            ittyping = false;
                            dong++;
                        }
                        break;
             
                    case state.Sound:
                        {
                        OnNV = false;
                        imgNV.gameObject.SetActive(false);
                        Name.SetText("");
                        source.PlayOneShot(st.Diabl[dong].clip);
                        
                        foreach (var e in st.Diabl[dong].TextStory)
                            {
                                text.text += e;
                                yield return new WaitForSeconds(0.01f);
                                //if (ittyping == true)
                                //{
                                //    SkipLine();
                                //    break;
                                //}
                                ittyping = true;
                            }
                            ittyping = false;
                            dong++;
                      
                            break;
                        }
                    case state.stopSound:
                        {
                        if (OnNV) imgNV.gameObject.SetActive(true);
                        else imgNV.gameObject.SetActive(false);
                       
                                BackGroundMusic.Stop();
                        foreach (var e in st.Diabl[dong].TextStory)
                            {
                                text.text += e;
                                yield return new WaitForSeconds(0.01f);
                                //if (ittyping == true)
                                //{
                                //    SkipLine();
                                //    break;
                                //}
                                ittyping = true;
                            }
                            ittyping = false;
                            dong++;
                            break;
                        }
                    case state.changeBackGround:
                        {
                        imgNV.gameObject.SetActive(false);
                        Name.SetText("");
                        checkSound();
                        background.sprite = st.Diabl[dong].Background;
                            foreach (var e in st.Diabl[dong].TextStory)
                            {
                                text.text += e;
                                yield return new WaitForSeconds(0.01f);
                                //if (ittyping == true)
                                //{
                                //    SkipLine();
                                //    break;
                                //}
                                BackGroundMusic.Stop();
                                ittyping = true;
                            }
                            ittyping = false;
                            dong++;

                            break;
                        }
                    case state.changeNV:
                        {
                        imgNV.gameObject.SetActive(true);
                        OnNV = true;
                        checkSound();
                            Name.SetText(st.Diabl[dong].NameStory);
                        if (st.Diabl[dong].imgNV!=null) imgNV.sprite = st.Diabl[dong].imgNV;
                        else { imgNV.gameObject.SetActive(false); }

                        foreach (var e in st.Diabl[dong].TextStory)
                            {
                                text.text += e;
                                yield return new WaitForSeconds(0.01f);
                                //if (ittyping == true)
                                //{
                                //    SkipLine();
                                //    break;
                                //}
                                ittyping = true;
                            }
                            ittyping = false;
                            dong++;
                            break;
                        }
                    case state.choose:
                        {
                            foreach (var e in st.Diabl[dong].TextStory)
                            {
                                text.text += e;
                                yield return new WaitForSeconds(0.01f);
                                //if (ittyping == true)
                                //{
                                //    SkipLine();
                                //    break;
                                //}
                                
                                ittyping = true;
                            }
                            ittyping = false;
                            SpawnChoose();
                            break;
                        }
                    case state.onlySeeText:
                        {

                            Name.SetText("");
                        checkSound();
                        OnNV=false;
                            imgNV.gameObject.SetActive(false);
                            foreach (var e in st.Diabl[dong].TextStory)
                            {
                                text.text += e;
                                yield return new WaitForSeconds(0.01f);
                                //if (ittyping == true)
                                //{
                                //    SkipLine();
                                //    break;
                                //}
                                
                                ittyping = true;
                            }
                            ittyping = false;
                            dong++;
                            break;
                        }
                }



            }
            else
            {
                endLine();
            }
        
       
    }
    public void showEndTamThoi()
    {
       
        RandomInon.FadeOut(ToBeComtrinue);

    }    
    
    public void SpawnChoose()
    {
        ItCanClick = false;
        foreach (var f in st.Diabl[dong].textChoose)
        {
            var e1 = Instantiate(choo, UIchoose.transform);
            RandomInon.FadeOut(e1.GetComponent<CanvasGroup>());
            e1.cho(f.choose, f.textCho);
        }
        dong = 0;
    }    
    public void SkipLine()
    {
        if (ittyping == true)
        {
            StopAllCoroutines();
            text.SetText(st.Diabl[dong].TextStory);
            ittyping = false;
            source.Stop();
          if (  st.Diabl[dong].state != state.choose) dong++;
          else { SpawnChoose(); }

        }
    }
    public void endLine()
    {
        source.Stop();
       

        StopAllCoroutines();
       
        }
   
   
public void Start()
    {
        StartCoroutine(StartAw());
    }
    IEnumerator StartAw()
    {
        FadeBlack.gameObject.SetActive(true);
        RandomInon.FadeIn(FadeBlack);
        ItCanClick = false;
        yield return new WaitUntil(()=> FadeBlack.alpha == 0);
        ItCanClick = true;
        BackGroundMusic.Stop();


        storyTime();
    }    
    public void SaveOrDelete()
    {
        var e = stories.Max(o => o.StoryChap);
        if (chapter <= e)
        {
            var e1 = new SaveChapter(chapter, dong, choose,resolution,source.volume);
            save.saveValue(e1);
        }
        else
        {
            var path = Path.Combine(Application.persistentDataPath, "valuee.json");
            if (File.Exists(path) == true)
            {
                File.Delete(path);
            }
        }
    }
    public void Update()
    {
        source.volume = Music.value;
        BackGroundMusic.volume = Music.value-0.1f;
    }
    public void OnApplicationQuit()
    {
        SaveOrDelete();
    }

}
    

 
public static class save
{


    public static string returnpart(string path1)
    {
        return path1;
    }
    public static void saveValue(SaveChapter p)
    {
        var path = Path.Combine(Application.persistentDataPath, "valuee.json");
      var  PlayerValue = p;
        

        string e = JsonUtility.ToJson(PlayerValue);
       

        File.WriteAllText(path, e);
    }
    public static SaveChapter readValue()
    {
      var  path = Path.Combine(Application.persistentDataPath, "valuee.json");
        if (File.Exists(path) == true)
        {
            var str = File.ReadAllText(path);
          var  bXH1 = JsonUtility.FromJson<SaveChapter>(str);
            return bXH1;
        }
        else
        {
            return null;
        }
    }
}
public class SaveChapter
{
    public int chapter;
    public int dong;
    public ChooseE state;
    public int resolution;
    public float volume;
    public SaveChapter ( int chapter, int dong , ChooseE chooseE,int resolution,float volume)
    {
        this.chapter = chapter;
        this.dong = dong;
        this.state = chooseE;
        this.resolution = resolution;
        this.volume = volume;
    }
}

