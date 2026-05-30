using Volo.Abp.Settings;

namespace TSTOP.Settings;

public class TSTOPSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(TSTOPSettings.MySetting1));
    }
}
