using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillColumn : SkillMenu
{
    int nSkillBars;
    List<Image> skillBars;
    GameObject BG_Panel;
    Image BG_Image;
    GameObject Vert_Panel;

    // Start is called before the first frame update
    void Start()
    {
        BG_Panel = transform.GetChild(0).gameObject;
        BG_Image = BG_Panel.transform.GetChild(0).GetComponent<Image>();

        Vert_Panel = transform.GetChild(1).gameObject;

        nSkillBars = Vert_Panel.transform.childCount;

        skillBars = new List<Image>();

        for (int i = 0; i < nSkillBars; i++) {
            skillBars.Add(Vert_Panel.transform.GetChild(i).GetComponent<Image>());
        }
    }

    // Update is called once per frame
    void Update()
    {
         GetComponent<CanvasRenderer>().SetColor(Color.yellow);
 

        if(Input.GetKey(KeyCode.Space)) {
            skillBars[0].color = Color.green;
            BG_Image.color = Color.yellow;
        }
    }
}
