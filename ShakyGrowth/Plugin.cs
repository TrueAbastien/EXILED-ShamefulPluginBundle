using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EXILED;
using EXILED.Extensions;

namespace ShakyGrowth
{
    public class Plugin : EXILED.Plugin
    {
        private EventHandlers eventHandlers;
        public Random gen = new Random();

        public bool enabled { get; private set; }

        public bool targetWhiteList { get; private set; }
        public List<RoleType> targetList { get; private set; }

        public bool originWhiteList { get; private set; }
        public List<string> originList { get; private set; }

        public float minScale { get; private set; }
        public float maxScale { get; private set; }

        public override void OnEnable()
        {
            ReloadConfig();
            if (!enabled) return;

            eventHandlers = new EventHandlers(this);

            Events.PlayerHurtEvent += eventHandlers.OnPlayerHurt;
        }

        public override void OnDisable()
        {
            Events.PlayerHurtEvent -= eventHandlers.OnPlayerHurt;

            eventHandlers = null;
        }

        public override void OnReload() { }

        private void ReloadConfig()
        {
            enabled = Config.GetBool("sg_enabled", true);
            
            List<string> targetStrList = Config.GetStringList("sg_targetWL") ?? Config.GetStringList("sg_targetBL") ?? new List<string>();
            RoleType output;
            targetList = new List<RoleType>();
            foreach (string targetStr in targetStrList)
                if (Enum.TryParse(targetStr, out output))
                    targetList.Add(output);
            targetWhiteList = Config.GetStringList("sg_targetWL") != null;

            originList = Config.GetStringList("sg_originWL") ?? Config.GetStringList("sg_originBL") ?? new List<string>();
            originWhiteList = Config.GetStringList("sg_originWL") != null;

            minScale = Config.GetFloat("sg_minScale", .25f);
            maxScale = Config.GetFloat("sg_maxScale", 2f);
        }

        public override string getName { get; } = "ShakyGrowth";
    }
}
