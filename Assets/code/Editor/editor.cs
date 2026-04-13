
using UnityEditor;
using UnityEngine;


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
            e.state = (state)EditorGUILayout.EnumPopup("Loại State ", e.state);

            switch (e.state)
            {
                case state.None:
                    {
                        e.TextStory = EditorGUILayout.TextField("Text :", e.TextStory);

                        break;
                    }

                case state.Sound:
                    {
                        e.TextStory = EditorGUILayout.TextField("Text :", e.TextStory);
                        e.clip = (AudioClip)EditorGUILayout.ObjectField(e.clip, typeof(AudioClip), false);
                        break;
                    }
                case state.stopSound:
                    {
                        e.TextStory = EditorGUILayout.TextField("Text :", e.TextStory);
                        break;
                    }
                case state.changeBackGround:
                    {
                        e.TextStory = EditorGUILayout.TextField("Text :", e.TextStory);
                        e.Background = (Sprite)EditorGUILayout.ObjectField(e.Background, typeof(Sprite), true);
                        break;
                    }
                case state.changeNV:
                    {
                        e.NameStory = EditorGUILayout.TextField("Name :", e.NameStory);
                        e.TextStory = EditorGUILayout.TextField("Text :", e.TextStory);
                        e.imgNV = (Sprite)EditorGUILayout.ObjectField(e.imgNV, typeof(Sprite), true);
                        break;
                    }
                case state.choose:
                    {
                        e.TextStory = EditorGUILayout.TextField("Text :", e.TextStory);
                        foreach (var f in e.textChoose)
                        {
                            f.choose = (ChooseE)EditorGUILayout.EnumPopup(f.choose);
                            f.textCho = EditorGUILayout.TextField("Text :", f.textCho);
                        }
                        break;
                    }
                case state.onlySeeText:
                    {
                        e.TextStory = EditorGUILayout.TextField("Text :", e.TextStory);

                        break;
                    }





            }
            e.soundType = (CheckTypeSound)EditorGUILayout.EnumPopup("Loại âm ", e.soundType);
            if (e.soundType == CheckTypeSound.BackGroundMusic) e.backGroundMusic = (AudioClip)EditorGUILayout.ObjectField(e.backGroundMusic, typeof(AudioClip), false);
            else if (e.soundType == CheckTypeSound.SFX) e.clip = (AudioClip)EditorGUILayout.ObjectField(e.clip, typeof(AudioClip), false);
            if (GUI.changed)
            {
                EditorUtility.SetDirty(story);
            }

        }
    }

}
