using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField]

   private TMP_Text timelefttext;

   [SerializeField]

   private TMP_Text Roundtext;

   private double numofsec;

   public double Numsec 
   { 
       
       get
       { 
           return numofsec;
       }

       set
       {
           numofsec=value;
       }
  }
    

    // Update is called once per frame
    void Update()
    {
        Roundtext.text=GManager.Instance.current_round.ToString(); 
        numofsec-=Time.deltaTime;
       
         if((int)numofsec==1||(int)numofsec==2||(int)numofsec==3||(int)numofsec==4||(int)numofsec==5||(int)numofsec==6||(int)numofsec==7||(int)numofsec==8||(int)numofsec==9||(int)numofsec==10||(int)numofsec==11||(int)numofsec==12||(int)numofsec==13)
         {
             timelefttext.text=((int)numofsec).ToString();
         }
         if(numofsec<0)
         {
             timelefttext.text="0 - Time Out";
             
             Debug.Log("Time Out");
             return;
         }

    }
}
