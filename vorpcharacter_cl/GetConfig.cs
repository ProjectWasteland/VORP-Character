using System;
using System.Collections.Generic;
using System.Dynamic;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using Newtonsoft.Json.Linq;
using vorpcharacter_cl.Utils;

namespace vorpcharacter_cl
{
    public class GetConfig : BaseScript
    {
        public static JObject Config = new JObject();
        public static JArray CharactArray = new JArray();
        public static Dictionary<string, string> Langs = new Dictionary<string, string>();
        public static bool IsLoaded;

        public GetConfig()
        {
            EventHandlers[$"{API.GetCurrentResourceName()}:SendConfig"] +=
                    new Action<string, ExpandoObject>(LoadDefaultConfig);
            TriggerServerEvent($"{API.GetCurrentResourceName()}:getConfig");
        }

        private void LoadDefaultConfig(string dc, ExpandoObject dl)
        {
            Config = JObject.Parse(dc);

            foreach (var l in dl)
            {
                Langs[l.Key] = l.Value.ToString();
            }

            IsLoaded = true;

            Commands.InitCommands();
        }
    }
}
