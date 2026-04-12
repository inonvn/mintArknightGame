using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChooseO : MonoBehaviour
{
    public ChooseE chooseE;
   
    public TextMeshProUGUI textMesh;
    public ChooseE cho (ChooseE e,string t)
    {
        chooseE = e;
        textMesh.SetText(t);
        return chooseE;
    }    
    public void getChoose()
    {
        gamemana.gamemna.choose = chooseE;
        gamemana.gamemna.chapter++;
        gamemana.gamemna.ItCanClick= true;
        
        gamemana.gamemna.storyTime();
        foreach(Transform e in gamemana.gamemna.UIchoose.transform)
        { 
        Destroy(e.gameObject);
        }    
    }
}
