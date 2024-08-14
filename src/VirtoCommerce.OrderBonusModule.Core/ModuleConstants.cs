using System.Collections.Generic;
using System.Linq;
using VirtoCommerce.Platform.Core.Settings;

namespace VirtoCommerce.OrderBonusModule.Core;

public static class ModuleConstants
{
    public static class CustomerOrder
    {
        public static class Status
        {
            public const string Completed = "Completed";
            public const string New = "New";
        }
    }

    public static class Customer
    {
        public static class Type
        {
            public const string Vip = "vip";
            public const string SuperVip = "supervip";
        }
    }

    public static class Security
    {
        public static class Permissions
        {
            public const string Access = "OrderBonusModule:access";
            public const string Create = "OrderBonusModule:create";
            public const string Read = "OrderBonusModule:read";
            public const string Update = "OrderBonusModule:update";
            public const string Delete = "OrderBonusModule:delete";

            public static string[] AllPermissions { get; } =
            {
                Access,
                Create,
                Read,
                Update,
                Delete,
            };
        }
    }

    public static class Settings
    {
        public static class General
        {
            public static SettingDescriptor OrderBonusModuleEnabled { get; } = new()
            {
                Name = "OrderBonusModule.OrderBonusModuleEnabled",
                GroupName = "OrderBonusModule|General",
                ValueType = SettingValueType.Boolean,
                DefaultValue = false,
            };

            public static SettingDescriptor OrderBonusModulePassword { get; } = new()
            {
                Name = "OrderBonusModule.OrderBonusModulePassword",
                GroupName = "OrderBonusModule|Advanced",
                ValueType = SettingValueType.SecureString,
                DefaultValue = "qwerty",
            };

            public static IEnumerable<SettingDescriptor> AllGeneralSettings
            {
                get
                {
                    yield return OrderBonusModuleEnabled;
                    yield return OrderBonusModulePassword;
                }
            }
        }

        public static class Bonus
        {
            public static readonly SettingDescriptor BonusPercentNormal = new SettingDescriptor
            {
                DisplayName = "Normal Customer bonus %",
                Name = "Stores.BonusPercent.Normal",
                GroupName = "Store|Bonus",
                ValueType = SettingValueType.Integer,
                DefaultValue = 1
            };

            public static readonly SettingDescriptor BonusPercentVip = new SettingDescriptor
            {
                DisplayName = "VIP Customer bonus %",
                Name = "Stores.BonusPercent.Vip",
                GroupName = "Store|Bonus",
                ValueType = SettingValueType.Integer,
                DefaultValue = 5
            };

            public static readonly SettingDescriptor BonusPercentSuperVip = new SettingDescriptor
            {
                DisplayName = "Super VIP Customer bonus %",
                Name = "Stores.BonusPercent.SuperVip",
                GroupName = "Store|Bonus",
                ValueType = SettingValueType.Integer,
                DefaultValue = 10
            };

            public static IEnumerable<SettingDescriptor> AllSettings
            {
                get
                {
                    yield return BonusPercentNormal;
                    yield return BonusPercentVip;
                    yield return BonusPercentSuperVip;
                }
            }
        }

        public static IEnumerable<SettingDescriptor> AllSettings
        {
            get
            {
                return General.AllGeneralSettings.Concat(Bonus.AllSettings);
            }
        }
    }
}
