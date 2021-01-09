using System.Linq;
using System.Reflection;
using BepInEx;
using HarmonyLib;
using UnityEngine;

namespace SebaFixes
{
    [BepInPlugin(PluginGuid, PluginName, PluginVersion)]
    internal class SebaFixes : BaseUnityPlugin
    {
        public const string PluginGuid = "com.sebag.pcbs.fixes";
        public const string PluginName = "Seba Fixes";
        public const string PluginVersion = "1.0.0";

        internal void Awake()
        {
            Debug.Log("Seba Fixes loaded!");
        }

        public SebaFixes()
        {
            // Harmony.CreateAndPatchAll(typeof(Patches), PluginGuid);
            // var harmony = new Harmony(PluginGuid);
            // harmony.PatchAll();
            // harmony.PatchAll(typeof(ReplacePartObjectiveTestSatisfiedByPatch));
            // harmony.PatchAll(typeof(Patches));

            // var originalMethod = typeof(ReplacePartObjective).GetMethod("TestSatisfiedBy");
            // var prefixMethod = typeof(ReplacePartObjectiveTestSatisfiedByPatch).GetMethod("Prefix");
            // var postfixMethod = typeof(ReplacePartObjectiveTestSatisfiedByPatch).GetMethod("Postfix");
            //
            // Debug.Log(originalMethod);
            // Debug.Log(prefixMethod);
            // Debug.Log(postfixMethod);
            //
            // harmony.Patch(
            //     originalMethod,
            //     new HarmonyMethod(prefixMethod),
            //     new HarmonyMethod(postfixMethod)
            // );
            Debug.Log("[SF] Patching Methods...");
            var harmony = new Harmony(PluginGuid);
            harmony.PatchAll(Assembly.GetExecutingAssembly());
            Debug.Log("[SF] Patched methods: " + harmony.GetPatchedMethods().Count().ToString());
            foreach (var method in harmony.GetPatchedMethods())
            {
                Debug.Log("[SF] Patched: " + method.Name);
            }
        }
    }
}