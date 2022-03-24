using UnityEngine;

public static class GameConst
{
    #region Player Custom Properties

    public const string READY_PROPERTY = "Ready";
    public const string LOADED_PROPERTY = "Loaded";
    
    #endregion
    
    #region Room Custom Properties
    
    public const string INITHERO_PROPERTY = "Inithero";
    
    #endregion

    #region Visual Axis & Button

    public const string HEROMOVE = "HeroMove";

    #endregion

    #region Animator Parameters

    public static int SPEED_PARAM;
    public static int PLAYERATTACK_PARAM;

    #endregion
    
    #region PlayerColors

    public static Color[] PLAYER_COLORS;

    #endregion

    #region Static Constructor

    static GameConst()
    {
        PLAYER_COLORS = new Color[]
        {
            Color.red,
            Color.blue, 
            Color.gray,
            Color.black, 
            Color.green, 
            Color.magenta,
            Color.yellow,
            Color.cyan
        };

        SPEED_PARAM = Animator.StringToHash("Speed");
        PLAYERATTACK_PARAM = Animator.StringToHash("PlayerAttack");
    }

    #endregion
}