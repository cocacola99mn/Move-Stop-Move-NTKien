using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameConstant
{
    public static string[] names =  { "Aron", "Abdul", "Abe", "Abel", "Abraham", "Adam", "Adan", "Adolfo", "Adolph", "Adrian", "Hafsa", "Lindsay", "Sierra", "Yusef", "Johns", "Ward", "Sarah" , "Khalil", "Payne", "Henri", "Hunter" , "Tomi", "Lam", "Bryant", "Diego" };

    public const string IDLE_ANIM = "IsIdle";
    public const string DEAD_ANIM = "IsDead";
    public const string ATTACK_ANIM = "IsAttack";
    public const string WIN_ANIM = "IsWin";
    public const string DANCE_ANIM = "IsDance";
    public const string ARROW_ANIM = "ArrowSwingAnim 2";

    public const string DAMAGEABLE_TAG = "Damageable";
    public const string OBSTACLE_TAG = "Obstacle";
    public const string GIFT_TAG = "Gift";
    public const string PROJECTILE_TAG = "Projectile";

    public const string HAT_DESCRIPTION = "+5 Range";
    public const string PANT_DESCRIPTION = "+5 Speed";

    public const string DEFAULT_NAME = "Player";
    public const string PREF_WEAPONEQUIP = "WeaponID";
    public const string PREF_HATEQUIP = "HatID";
    public const string PREF_PANTEQUIP = "PantID";
    public const string PREF_PLAYERNAME = "PlayerName";
    public const string PREF_COLOR = "Color";
    public const string PREF_GOLD = "Gold";
    public const string PREF_RANK = "Rank";
    public const string PREF_ZONE = "Zone";
    public const string PREF_INITDATA = "InitData";
    public const string PREF_SOUND = "SoundPref";
    public const string PREF_VIBRATE = "Vibrate";

    public const string MIXER_MASTER = "MasterVolume";

    public const string EQUIPPED_TEXT = "EQUIPPED";
    public const string EQUIP_TEXT = "EQUIP";
}
