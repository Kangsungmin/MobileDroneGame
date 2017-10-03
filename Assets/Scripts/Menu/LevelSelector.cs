using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelector : MonoBehaviour {
    //static SceneData sceneData = new SceneData();
    public SceneFader fader;

    public void Select(string Name)//버튼 클릭시.
    {
        if (PlayerDataManager.spanner > 0)
        {
            string MapName, Level;
            int index = Name.IndexOf(".");
            MapName = Name.Substring(0, index);
            Level = Name.Substring(index + 1, 2);
            //씬데이터에 저장
            SceneData.sceneData.LoadStage(MapName, Level);
            PlayerDataManager.spanner--;//스패너 감소
            PlayerPrefs.SetInt("spanner", PlayerDataManager.spanner);
            fader.FadeTo(MapName);
        }
       
    }
}
