public class Combo : MyDisplay
{
    public static Combo instance;
    private int combo;
    private int comboIndex;
    void Awake()
    {
     //   base.Awake();
        instance = this;
        SetCombo(1);
    }
    public void StopCombo()
    {
        comboIndex = 0;
        SetCombo(1);
        ScreenShake.instance.Shake(0.1f, 0.1f);
    }
    public int GetComboLevel()
    {
        comboIndex++;
        if (comboIndex > 20)
        {
            SetCombo(3);
        }else if (comboIndex > 8)
        {
            SetCombo(2);
        }

        return combo;
    }
    
    private void SetCombo(int newCombo)
    {
        combo = newCombo;
        

        if (combo > 1)
        {
            if (combo >= 3)
            {
                ScreenShake.instance.Shake(0.1f, 0.1f);
            }
            
            Display(combo+"x");
        }
        else
        {
            Display("");
        }
        
    }
}
