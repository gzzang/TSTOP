using TSTOP.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace TSTOP.Permissions;

public class TSTOPPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(TSTOPPermissions.GroupName);

        var booksPermission = myGroup.AddPermission(TSTOPPermissions.Books.Default, L("Permission:Books"));
        booksPermission.AddChild(TSTOPPermissions.Books.Create, L("Permission:Books.Create"));
        booksPermission.AddChild(TSTOPPermissions.Books.Edit, L("Permission:Books.Edit"));
        booksPermission.AddChild(TSTOPPermissions.Books.Delete, L("Permission:Books.Delete"));
        //Define your own permissions here. Example:
        //myGroup.AddPermission(TSTOPPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<TSTOPResource>(name);
    }
}
