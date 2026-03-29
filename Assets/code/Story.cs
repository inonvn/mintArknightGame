using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName ="Story",menuName ="Story")]
public class Story : ScriptableObject
{
    public int StoryChap;
    public ChooseE chooseE;
    public List<Dia> Diabl;
}

[System.Serializable]
public class Dia
{
    public state state;
    public string TextStory;
    public string NameStory;
    public AudioClip clip;
    public Sprite imgNV;
    public Sprite Background;
    public List<ChooseLim> textChoose;
}

[System.Serializable]
public class ChooseLim
{
    public ChooseE choose;
    public string textCho;
}

public enum ChooseE
{
    none,
    choose1,
    choose2,
    choose3,
}
public enum state
{
    None,
    Sound,
    stopSound,
    changeBackGround,
    changeNV,
    choose,
    onlySeeText,
    
}
