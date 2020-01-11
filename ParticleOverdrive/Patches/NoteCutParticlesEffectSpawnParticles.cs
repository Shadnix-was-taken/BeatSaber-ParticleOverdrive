﻿using System;
using System.Linq;
using Harmony;
using UnityEngine;
using ParticleOverdrive.Misc;
using BS_Utils.Utilities;

namespace ParticleOverdrive.Patches
{
    [HarmonyPatch(typeof(NoteCutParticlesEffect))]
    [HarmonyPatch("SpawnParticles")]
    class NoteCutParticlesEffectSpawnParticles
    {
        internal static void Prefix(ref NoteCutParticlesEffect __instance, ref Color32 color, ref int sparkleParticlesCount, ref int explosionParticlesCount, ref float lifetimeMultiplier,
            ref ParticleSystem[] ____sparklesPS, ref ParticleSystem ____explosionPS)
        {
            sparkleParticlesCount = Mathf.FloorToInt(sparkleParticlesCount * Plugin.SlashParticleMultiplier);
            explosionParticlesCount = Mathf.FloorToInt(explosionParticlesCount * Plugin.ExplosionParticleMultiplier);
            lifetimeMultiplier *= Plugin.SlashParticleLifetimeMultiplier;
            if (Plugin.RainbowParticles)
                color = UnityEngine.Random.ColorHSV();
            ParticleSystem.MainModule slashMain = ____sparklesPS[0].main;
            slashMain.maxParticles = int.MaxValue;
            ParticleSystem.MainModule explosionMain = ____explosionPS.main;
            explosionMain.maxParticles = int.MaxValue;
            explosionMain.startLifetimeMultiplier = Plugin.ExplosionParticleLifetimeMultiplier;
        }
    }
}
