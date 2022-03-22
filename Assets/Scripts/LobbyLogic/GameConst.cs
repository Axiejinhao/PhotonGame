using UnityEngine;

public static class GameConst
{
    #region Player Custom Properties

    public const string READY_PROPERTY = "Ready";

    #endregion
    
    #region Room Custom Properties
    

    
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
    }

    #endregion
}