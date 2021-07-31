using System;
using System.Threading.Tasks;
using CitizenFX.Core;

namespace vorpcharacter_cl
{
    public class vorpcharacter_cl : BaseScript
    {
        public static dynamic CORE;
        public static int MaxCharacters;

        public vorpcharacter_cl()
        {
            GetCore();
        }

        public async Task GetCore()
        {
            while (CORE == null)
            {
                TriggerEvent("getCore", new Action<dynamic>(dic => { CORE = dic; }));
                await Delay(100);
            }

            Debug.WriteLine(CORE.ToString());
            CORE.RpcCall("vorp_characters:getMaxCharacters", new Action<int>(max => { MaxCharacters = max; }), "none");
        }
    }
}
