﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UziTrainer {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "15.9.0.0")]
    internal sealed partial class SwapDoll : global::System.Configuration.ApplicationSettingsBase {
        
        private static SwapDoll defaultInstance = ((SwapDoll)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new SwapDoll())));
        
        public static SwapDoll Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool Active {
            get {
                return ((bool)(this["Active"]));
            }
            set {
                this["Active"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("SOPMOD")]
        public string ExhaustedDoll {
            get {
                return ((string)(this["ExhaustedDoll"]));
            }
            set {
                this["ExhaustedDoll"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("G11")]
        public string LoadedDoll {
            get {
                return ((string)(this["LoadedDoll"]));
            }
            set {
                this["LoadedDoll"] = value;
            }
        }
    }
}
