using System.Collections;
using System.Collections.Generic;
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
 
    public GameObject Choose;
    public GameObject textBox;
    public TextMeshProUGUI text;
    public TextMeshProUGUI Name;
    public Image imgNV;
    public Image background;
    public AudioSource source;
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
    bool ittyping;
    int dong;
  public   ChooseE choose;
    public void Awake()
    {
        gamemna = this;
    }
    public void storyTime ()
    {
       if (ittyping == false)
        {
            StartCoroutine(texte());

        dong++;
        }    
       else
        {
            SkipLine();
        }
    }
    public void onSetting ()
    {
        UIsetting.gameObject.SetActive(true);
        RandomInon.FadeOut(UIsetting);
        LogButton.SetActive(false);
        MapButton.SetActive(false);
      

    }    
    public void offSsetting ()
    {
       
        RandomInon.FadeIn(UIsetting);
      
        LogButton.SetActive(true);
        MapButton.SetActive(true);
    }
    int nu = 0;
    public void onLogE()
    {
        UILog.gameObject.SetActive(true);
        RandomInon.FadeOut(UILog);
        MapButton.SetActive(false);
        settingButton.SetActive(false);
        while (nu < dong)
        
        {
          var e =  Instantiate(HistoryLine, UILog.transform);
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
        RandomInon.FadeIn(UILog);
        UILog.gameObject.SetActive(false);
        MapButton.SetActive(true);
        settingButton.SetActive(true);

    }
    
    public void SkipStory()
    {
        
      

    }
    public void logE ()
    {
        
    }
    Story st;
  
    IEnumerator texte()
    {



       st  = stories.Where(o => o.StoryChap == chapter && o.chooseE == choose).FirstOrDefault();
        if (dong < st.Diabl.Count())
        {
            

           
                text.SetText("");
                switch (st.Diabl[dong].state)
                {
                    case state.None:
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
                     
                        ittyping = false;
                            }
                        }
                        break;
                        
                    case state.Sound:
                        {
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
                                
                                ittyping = false;
                            }
                            break;
                        }
                    case state.stopSound:
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
                                source.Stop();
                                ittyping = false;
                            }
                            break;
                        }
                    case state.changeBackGround:
                        {
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
                                source.Stop();
                                ittyping = false;
                            }
                            break;
                        }
                    case state.changeNV:
                        {
                            Name.SetText(st.Diabl[dong].NameStory);
                            imgNV.sprite = st.Diabl[dong].imgNV;
                            foreach (var e in st.Diabl[dong].TextStory)
                            {
                                text.text += e;
                                yield return new WaitForSeconds(0.01f);
                                //if (ittyping == true)
                                //{
                                //    SkipLine();
                                //    break;
                                //}
                                
                                ittyping = false;
                            }
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
                            source.Stop();
                            ittyping = false;
                        }
                        foreach (var f in st.Diabl[dong].textChoose)
                        {
                            var e1 = Instantiate(choo, UIchoose.transform);
                            e1.cho(f.choose, f.textCho);
                        }
                        break;
                        }
                    case state.onlySeeText:
                        {
                            Name.SetText("");
                            foreach (var e in st.Diabl[dong].TextStory)
                            {
                                text.text += e;
                                yield return new WaitForSeconds(0.01f);
                                //if (ittyping == true)
                                //{
                                //    SkipLine();
                                //    break;
                                //}
                                source.Stop();
                                ittyping = false;
                            }
                            break;
                        }
                }

            

        }
        else
        {
            endLine();
        }
    }
    public void SkipLine()
    {
        if (ittyping == true)
        {
            StopAllCoroutines();
            text.SetText(st.Diabl[dong].TextStory);
            ittyping = false;
            source.Stop();
        }
    }
    public void endLine()
    {
        source.Stop();
       

        StopAllCoroutines();
       
        }
   
public void Start()
    {
        storyTime();
    }
    public void Update()
    {
        source.volume = Music.value;
    }

}
    
    [CustomEditor(typeof(Story))]
public class StoryEditor : Editor
{
    public override void OnInspectorGUI()
    {
        //DrawDefaultInspector();

        base.OnInspectorGUI();
        Story story = (Story)target;
        foreach (var e in story.Diabl)
        {
            e.state = (state)EditorGUILayout.EnumPopup(e.state);
            switch (e.state)
            {
                case state.None:
                    {
                        e.TextStory = EditorGUILayout.TextField("Text :",e.TextStory);

                        break;
                    }
                case state.Sound:
                    {
                        e.TextStory = EditorGUILayout.TextField("Text :",e.TextStory);
                        e.clip = (AudioClip)EditorGUILayout.ObjectField(e.clip,typeof(AudioClip),false);
                        break;
                    }
                case state.stopSound:
                    {
                        e.TextStory = EditorGUILayout.TextField("Text :", e.TextStory);
                        break;
                    }
                case state.changeBackGround:
                    {
                        e.TextStory= EditorGUILayout.TextField("Text :", e.TextStory);
                        e.Background =(Sprite) EditorGUILayout.ObjectField(e.Background,typeof(Sprite),true);
                        break;
                    }
                case state.changeNV:
                    {
                        e.NameStory = EditorGUILayout.TextField("Name :",e.NameStory);
                        e.TextStory = EditorGUILayout.TextField("Text :", e.TextStory);
                        e.imgNV = (Sprite)EditorGUILayout.ObjectField(e.imgNV, typeof(Sprite), true);
                        break;
                    }
                case state.choose:
                    {
                        e.TextStory = EditorGUILayout.TextField("Text :", e.TextStory);
                        foreach(var f in e.textChoose)
                        {
                            f.choose = (ChooseE)EditorGUILayout.EnumPopup(f.choose);
                            f.textCho = EditorGUILayout.TextField("Text :", f.textCho);
                        }    
                        break;
                    }
                case state.onlySeeText:
                    {
                        e.TextStory=EditorGUILayout.TextField("Text :", e.TextStory);
                        
                        break;
                    }





            }
            if (GUI.changed)
            {
                EditorUtility.SetDirty(story);
            }    

        }
    }
}
