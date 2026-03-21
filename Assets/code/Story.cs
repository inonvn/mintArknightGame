using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName ="Story",menuName ="Story")]
public class Story : ScriptableObject
{
    public int StoryChap;
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
    public ChooseE textChoose;
}

public enum ChooseE
{
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
