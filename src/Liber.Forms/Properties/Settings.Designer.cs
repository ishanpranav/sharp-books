﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Liber.Forms.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "17.9.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("{}")]
        public string RecentPaths {
            get {
                return ((string)(this["RecentPaths"]));
            }
            set {
                this["RecentPaths"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string Culture {
            get {
                return ((string)(this["Culture"]));
            }
            set {
                this["Culture"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("[]")]
        public string CustomColors {
            get {
                return ((string)(this["CustomColors"]));
            }
            set {
                this["CustomColors"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute(@"[
  {
    ""filter"": ""^7"",
    ""type"": ""incomeTaxExpense"",
    ""strict"": true
  },
  {
    ""filter"": ""^[46]9"",
    ""type"": ""otherIncomeExpense"",
    ""strict"": true
  },
  {
    ""filter"": ""^1[689]"",
    ""type"": ""otherAsset""
  },
  {
    ""filter"": ""^17"",
    ""type"": ""fixedAsset""
  },
  {
    ""filter"": ""^2[5-9]"",
    ""type"": ""longTermLiability""
  }
]")]
        public string ImportRules {
            get {
                return ((string)(this["ImportRules"]));
            }
            set {
                this["ImportRules"] = value;
            }
        }
    }
}
