﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace EMEWEQUALITY.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "14.0.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.ConnectionString)]
        [global::System.Configuration.DefaultSettingValueAttribute("Data Source=192.168.1.118;Initial Catalog=CQEMEWEQC;Persist Security Info=True;Us" +
            "er ID=sa;Password=123456")]
        public string EMEWEQCConnectionString {
            get {
                return ((string)(this["EMEWEQCConnectionString"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.WebServiceUrl)]
        [global::System.Configuration.DefaultSettingValueAttribute("http://localhost:9966/WastePaperManagement/QualityPaper/QualityPaperManagement/v0" +
            "01")]
        public string EMEWEQUALITY_LWGETWebReference_QualityPaperManagement_v001 {
            get {
                return ((string)(this["EMEWEQUALITY_LWGETWebReference_QualityPaperManagement_v001"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.WebServiceUrl)]
        [global::System.Configuration.DefaultSettingValueAttribute("http://localhost:9966/WastePaperManagement/LooseWastePaper/LooseWastePaperManagem" +
            "ent/v001")]
        public string EMEWEQUALITY_LWU9WebReference_LooseWastePaperManagement_v001 {
            get {
                return ((string)(this["EMEWEQUALITY_LWU9WebReference_LooseWastePaperManagement_v001"]));
            }
        }
    }
}