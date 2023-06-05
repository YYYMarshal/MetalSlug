using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AnimatorParameters
{
    private static readonly string walk = "walk";
    private static readonly string shoot = "shoot";
    private static readonly string shootUp = "shootup";
    private static readonly string jump = "jump";
    private static readonly string attack = "attack";
    private static readonly string throwGrenade = "throw";
    private static readonly string die = "die";
    private static readonly string idle = "idle";
    private static readonly string kill = "kill";

    public static string Walk => walk;
    public static string Shoot => shoot;
    public static string ShootUp => shootUp;
    public static string Jump => jump;
    public static string Attack => attack;
    public static string Throw => throwGrenade;
    public static string Die => die;
    public static string Idle => idle;
    public static string Kill => kill;
}
