using System;

public class Combo : MyDisplay
{
    public static Combo instance;
    private int combo;
    private int comboIndex;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        SetCombo(1);
    }

    public void StopCombo()
    {
        comboIndex = 0;
        SetCombo(1);
        //ScreenShake.instance.Shake(0.1f, 0.1f);
    }

    public void StartSliding(){
        comboIndex = 0;
        SetCombo(0);
    }


public int GetComboLevel()
    {
        comboIndex++;
        if (comboIndex > 80)
        {
            SetCombo(8);
        }else
        if (comboIndex > 70)
        {
            SetCombo(7);
        }else
        if (comboIndex > 60)
        {
            SetCombo(6);
        }else
        if (comboIndex > 45)
        {
            SetCombo(5);
        }else
        if (comboIndex > 20)
        {
            SetCombo(4);
        }else
        if (comboIndex > 12)
        {
            SetCombo(3);
        }else if (comboIndex > 5)
        {
            SetCombo(2);
        }
        return combo;
    }
    
    private void SetCombo(int newCombo)
    {
        if (newCombo == 0)
        {
            Spawner.instance.SetSpeed(8);
        }

        else
        {
            Spawner.instance.SetSpeed(Bigbrain.Map(newCombo,1,8,4,8));
        }
       
        
        combo = newCombo;
        if (combo > 1)
        {
            // if (combo >= 8)
            // {
            //     ScreenShake.instance.Shake(0.1f, 0.1f);
            // }
            
            Display(combo+"x");
        }
        else
        {
            Display("");
        }
        
    }
}
