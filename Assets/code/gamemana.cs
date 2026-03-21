using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class gamemana : MonoBehaviour
{
    public List<Story> stories;
    public state state;
    public GameObject Choose;
    public GameObject textBox;
    public TextMeshProUGUI text;
    public TextMeshProUGUI Name;
    public Image imgNV;
    public Image background;
    public GameObject setting;
    public GameObject HistoryLine;
    public GameObject Map;
    public AudioSource source;
    public int chapter;
    bool ittyping;
    int dong;
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
    public void SkipStory()
    {
        
      

    }
    Story st;
  
    IEnumerator texte()
    {



       st  = stories.Where(o => o.StoryChap == chapter).FirstOrDefault();
        if (dong < st.Diabl.Count())
        {


            foreach (var t in st.Diabl)
            {
                switch (t.state)
                {

                    case state.None:
                        {
                           
                            foreach (var e in t.TextStory)
                            {
                                text.text += e;
                                yield return new WaitForSeconds(0.01f);
                                if (ittyping == true)
                                {
                                    SkipLine();
                                    break;
                                }
                     
                        ittyping = false;
                            }
                        }
                        break;
                        
                    case state.Sound:
                        {
                                source.PlayOneShot(t.clip);
                            foreach (var e in t.TextStory)
                            {
                                text.text += e;
                                yield return new WaitForSeconds(0.01f);
                                if (ittyping == true)
                                {
                                    SkipLine();
                                    break;
                                }
                                
                                ittyping = false;
                            }
                            break;
                        }
                    case state.stopSound:
                        {
                            foreach (var e in t.TextStory)
                            {
                                text.text += e;
                                yield return new WaitForSeconds(0.01f);
                                if (ittyping == true)
                                {
                                    SkipLine();
                                    break;
                                }
                                source.Stop();
                                ittyping = false;
                            }
                            break;
                        }
                    case state.changeBackGround:
                        {
                            background.sprite = t.Background;
                            foreach (var e in t.TextStory)
                            {
                                text.text += e;
                                yield return new WaitForSeconds(0.01f);
                                if (ittyping == true)
                                {
                                    SkipLine();
                                    break;
                                }
                                source.Stop();
                                ittyping = false;
                            }
                            break;
                        }
                    case state.changeNV:
                        {
                            Name.SetText(t.NameStory);
                            imgNV.sprite = t.imgNV;
                            foreach (var e in t.TextStory)
                            {
                                text.text += e;
                                yield return new WaitForSeconds(0.01f);
                                if (ittyping == true)
                                {
                                    SkipLine();
                                    break;
                                }
                                
                                ittyping = false;
                            }
                            break;
                        }
                    case state.choose:
                        {

                            break;
                        }
                    case state.onlySeeText:
                        {
                            Name.SetText("");
                            foreach (var e in t.TextStory)
                            {
                                text.text += e;
                                yield return new WaitForSeconds(0.01f);
                                if (ittyping == true)
                                {
                                    SkipLine();
                                    break;
                                }
                                source.Stop();
                                ittyping = false;
                            }
                            break;
                        }
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
                        e.textChoose =(ChooseE) EditorGUILayout.EnumPopup(e.textChoose);
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
