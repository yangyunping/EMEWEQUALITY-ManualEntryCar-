﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

// 
// 此源代码是由 Microsoft.VSDesigner 4.0.30319.42000 版自动生成。
// 
#pragma warning disable 1591

namespace EMEWEQUALITY.LWU9WebReference {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1099.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="LooseWastePaperManagementHTTPEndpointBinding", Namespace="http://www.leemanpaper.com/wsdl/wastePaperManagement/looseWastePaper/LooseWastePa" +
        "perManagement/v001/concrete")]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(LoosePaperContainerShareType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(LoosePaperTruckLoadType))]
    public partial class LooseWastePaperManagement_v001 : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private RequestHeaderType requestHeaderField;
        
        private System.Threading.SendOrPostCallback RequestForCheckOutOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetRemainingLoosePaperQuantitiesOperationCompleted;
        
        private System.Threading.SendOrPostCallback RegisterLoosePaperDistributionOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public LooseWastePaperManagement_v001() {
            this.Url = global::EMEWEQUALITY.Properties.Settings.Default.EMEWEQUALITY_LWU9WebReference_LooseWastePaperManagement_v001;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public RequestHeaderType RequestHeader {
            get {
                return this.requestHeaderField;
            }
            set {
                this.requestHeaderField = value;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event RequestForCheckOutCompletedEventHandler RequestForCheckOutCompleted;
        
        /// <remarks/>
        public event GetRemainingLoosePaperQuantitiesCompletedEventHandler GetRemainingLoosePaperQuantitiesCompleted;
        
        /// <remarks/>
        public event RegisterLoosePaperDistributionCompletedEventHandler RegisterLoosePaperDistributionCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("/WastePaperManagement/LooseWastePaper/LooseWastePaperManagement/v001/RequestForCh" +
            "eckOut", RequestNamespace="http://www.leemanpaper.com/wsdl/wastePaperManagement/looseWastePaper/LooseWastePa" +
            "perManagement/v001", ResponseNamespace="http://www.leemanpaper.com/wsdl/wastePaperManagement/looseWastePaper/LooseWastePa" +
            "perManagement/v001", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("ReplyMessage", Namespace="http://www.leemanpaper.com/schema/technology/framework/Common/v001")]
        public ReplyMessageType RequestForCheckOut([System.Xml.Serialization.XmlElementAttribute(Namespace="http://www.leemanpaper.com/schema/common/LeeManCommon/v001")] string OrganizationID, string WeightTicketID) {
            object[] results = this.Invoke("RequestForCheckOut", new object[] {
                        OrganizationID,
                        WeightTicketID});
            return ((ReplyMessageType)(results[0]));
        }
        
        /// <remarks/>
        public void RequestForCheckOutAsync(string OrganizationID, string WeightTicketID) {
            this.RequestForCheckOutAsync(OrganizationID, WeightTicketID, null);
        }
        
        /// <remarks/>
        public void RequestForCheckOutAsync(string OrganizationID, string WeightTicketID, object userState) {
            if ((this.RequestForCheckOutOperationCompleted == null)) {
                this.RequestForCheckOutOperationCompleted = new System.Threading.SendOrPostCallback(this.OnRequestForCheckOutOperationCompleted);
            }
            this.InvokeAsync("RequestForCheckOut", new object[] {
                        OrganizationID,
                        WeightTicketID}, this.RequestForCheckOutOperationCompleted, userState);
        }
        
        private void OnRequestForCheckOutOperationCompleted(object arg) {
            if ((this.RequestForCheckOutCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.RequestForCheckOutCompleted(this, new RequestForCheckOutCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("/WastePaperManagement/LooseWastePaper/LooseWastePaperManagement/v001/GetRemaining" +
            "LoosePaperQuantities", RequestNamespace="http://www.leemanpaper.com/wsdl/wastePaperManagement/looseWastePaper/LooseWastePa" +
            "perManagement/v001", ResponseNamespace="http://www.leemanpaper.com/wsdl/wastePaperManagement/looseWastePaper/LooseWastePa" +
            "perManagement/v001", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("Batch")]
        public GetRemainingLoosePaperQuantitiesResponseBatch[] GetRemainingLoosePaperQuantities([System.Xml.Serialization.XmlElementAttribute(Namespace="http://www.leemanpaper.com/schema/common/LeeManCommon/v001")] string OrganizationID, string ConsumingDepartmentID) {
            object[] results = this.Invoke("GetRemainingLoosePaperQuantities", new object[] {
                        OrganizationID,
                        ConsumingDepartmentID});
            return ((GetRemainingLoosePaperQuantitiesResponseBatch[])(results[0]));
        }
        
        /// <remarks/>
        public void GetRemainingLoosePaperQuantitiesAsync(string OrganizationID, string ConsumingDepartmentID) {
            this.GetRemainingLoosePaperQuantitiesAsync(OrganizationID, ConsumingDepartmentID, null);
        }
        
        /// <remarks/>
        public void GetRemainingLoosePaperQuantitiesAsync(string OrganizationID, string ConsumingDepartmentID, object userState) {
            if ((this.GetRemainingLoosePaperQuantitiesOperationCompleted == null)) {
                this.GetRemainingLoosePaperQuantitiesOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetRemainingLoosePaperQuantitiesOperationCompleted);
            }
            this.InvokeAsync("GetRemainingLoosePaperQuantities", new object[] {
                        OrganizationID,
                        ConsumingDepartmentID}, this.GetRemainingLoosePaperQuantitiesOperationCompleted, userState);
        }
        
        private void OnGetRemainingLoosePaperQuantitiesOperationCompleted(object arg) {
            if ((this.GetRemainingLoosePaperQuantitiesCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetRemainingLoosePaperQuantitiesCompleted(this, new GetRemainingLoosePaperQuantitiesCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapHeaderAttribute("RequestHeader")]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("/WastePaperManagement/LooseWastePaper/LooseWastePaperManagement/v001/RegisterLoos" +
            "ePaperDistribution", RequestNamespace="http://www.leemanpaper.com/wsdl/wastePaperManagement/looseWastePaper/LooseWastePa" +
            "perManagement/v001", ResponseNamespace="http://www.leemanpaper.com/wsdl/wastePaperManagement/looseWastePaper/LooseWastePa" +
            "perManagement/v001", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("ReplyMessage", Namespace="http://www.leemanpaper.com/schema/technology/framework/Common/v001")]
        public ReplyMessageType RegisterLoosePaperDistribution(string OrganizationID, [System.Xml.Serialization.XmlElementAttribute(Namespace="http://www.leemanpaper.com/schema/wastePaperManagement/looseWastePaper/LoosePaper" +
            "TruckLoad/v002")] LoosePaperTruckLoadU9Type TruckLoad, string DistributionMemo, [System.Xml.Serialization.XmlArrayItemAttribute("ContainerShare", Namespace="http://www.leemanpaper.com/schema/wastePaperManagement/looseWastePaper/LoosePaper" +
            "TruckLoad/v002", IsNullable=false)] LoosePaperContainerShareU9Type[] Distribution, [System.Xml.Serialization.XmlAnyAttributeAttribute()] System.Xml.XmlAttribute[] AnyAttr) {
            object[] results = this.Invoke("RegisterLoosePaperDistribution", new object[] {
                        OrganizationID,
                        TruckLoad,
                        DistributionMemo,
                        Distribution,
                        AnyAttr});
            return ((ReplyMessageType)(results[0]));
        }
        
        /// <remarks/>
        public void RegisterLoosePaperDistributionAsync(string OrganizationID, LoosePaperTruckLoadU9Type TruckLoad, string DistributionMemo, LoosePaperContainerShareU9Type[] Distribution, System.Xml.XmlAttribute[] AnyAttr) {
            this.RegisterLoosePaperDistributionAsync(OrganizationID, TruckLoad, DistributionMemo, Distribution, AnyAttr, null);
        }
        
        /// <remarks/>
        public void RegisterLoosePaperDistributionAsync(string OrganizationID, LoosePaperTruckLoadU9Type TruckLoad, string DistributionMemo, LoosePaperContainerShareU9Type[] Distribution, System.Xml.XmlAttribute[] AnyAttr, object userState) {
            if ((this.RegisterLoosePaperDistributionOperationCompleted == null)) {
                this.RegisterLoosePaperDistributionOperationCompleted = new System.Threading.SendOrPostCallback(this.OnRegisterLoosePaperDistributionOperationCompleted);
            }
            this.InvokeAsync("RegisterLoosePaperDistribution", new object[] {
                        OrganizationID,
                        TruckLoad,
                        DistributionMemo,
                        Distribution,
                        AnyAttr}, this.RegisterLoosePaperDistributionOperationCompleted, userState);
        }
        
        private void OnRegisterLoosePaperDistributionOperationCompleted(object arg) {
            if ((this.RegisterLoosePaperDistributionCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.RegisterLoosePaperDistributionCompleted(this, new RegisterLoosePaperDistributionCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1099.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.leemanpaper.com/schema/technology/framework/Common/v001")]
    [System.Xml.Serialization.XmlRootAttribute("RequestHeader", Namespace="http://www.leemanpaper.com/schema/technology/framework/Common/v001", IsNullable=false)]
    public partial class RequestHeaderType : System.Web.Services.Protocols.SoapHeader {
        
        private string transactionIDField;
        
        private string correlationIDField;
        
        private string sourceIDField;
        
        private string sourceIDTrailField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        public string TransactionID {
            get {
                return this.transactionIDField;
            }
            set {
                this.transactionIDField = value;
            }
        }
        
        /// <remarks/>
        public string CorrelationID {
            get {
                return this.correlationIDField;
            }
            set {
                this.correlationIDField = value;
            }
        }
        
        /// <remarks/>
        public string SourceID {
            get {
                return this.sourceIDField;
            }
            set {
                this.sourceIDField = value;
            }
        }
        
        /// <remarks/>
        public string SourceIDTrail {
            get {
                return this.sourceIDTrailField;
            }
            set {
                this.sourceIDTrailField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(LoosePaperContainerShareU9Type))]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1099.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.leemanpaper.com/schema/wastePaperManagement/looseWastePaper/LoosePaper" +
        "TruckLoad/v002")]
    public partial class LoosePaperContainerShareType {
        
        private string containerIDField;
        
        private bool isGettingShareField;
        
        private string pONumberField;
        
        private string shipmentNumberField;
        
        private string unloadPlatformField;
        
        private string memoField;
        
        private TypeOfWeighedWastePaperType typeOfWeighedWastePaperField;
        
        private bool typeOfWeighedWastePaperFieldSpecified;
        
        /// <remarks/>
        public string ContainerID {
            get {
                return this.containerIDField;
            }
            set {
                this.containerIDField = value;
            }
        }
        
        /// <remarks/>
        public bool IsGettingShare {
            get {
                return this.isGettingShareField;
            }
            set {
                this.isGettingShareField = value;
            }
        }
        
        /// <remarks/>
        public string PONumber {
            get {
                return this.pONumberField;
            }
            set {
                this.pONumberField = value;
            }
        }
        
        /// <remarks/>
        public string ShipmentNumber {
            get {
                return this.shipmentNumberField;
            }
            set {
                this.shipmentNumberField = value;
            }
        }
        
        /// <remarks/>
        public string UnloadPlatform {
            get {
                return this.unloadPlatformField;
            }
            set {
                this.unloadPlatformField = value;
            }
        }
        
        /// <remarks/>
        public string Memo {
            get {
                return this.memoField;
            }
            set {
                this.memoField = value;
            }
        }
        
        /// <remarks/>
        public TypeOfWeighedWastePaperType TypeOfWeighedWastePaper {
            get {
                return this.typeOfWeighedWastePaperField;
            }
            set {
                this.typeOfWeighedWastePaperField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool TypeOfWeighedWastePaperSpecified {
            get {
                return this.typeOfWeighedWastePaperFieldSpecified;
            }
            set {
                this.typeOfWeighedWastePaperFieldSpecified = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1099.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.leemanpaper.com/schema/common/LeeManCommon/v001")]
    public enum TypeOfWeighedWastePaperType {
        
        /// <remarks/>
        BaleForeign,
        
        /// <remarks/>
        BaleLocal,
        
        /// <remarks/>
        LoosenedAtContainer,
        
        /// <remarks/>
        LoosenedAtBin,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1099.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.leemanpaper.com/schema/wastePaperManagement/looseWastePaper/LoosePaper" +
        "TruckLoad/v002")]
    public partial class LoosePaperContainerShareU9Type : LoosePaperContainerShareType {
        
        private string extensionField1Field;
        
        private string extensionField2Field;
        
        private string extensionField3Field;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        public string ExtensionField1 {
            get {
                return this.extensionField1Field;
            }
            set {
                this.extensionField1Field = value;
            }
        }
        
        /// <remarks/>
        public string ExtensionField2 {
            get {
                return this.extensionField2Field;
            }
            set {
                this.extensionField2Field = value;
            }
        }
        
        /// <remarks/>
        public string ExtensionField3 {
            get {
                return this.extensionField3Field;
            }
            set {
                this.extensionField3Field = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(LoosePaperTruckLoadU9Type))]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1099.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.leemanpaper.com/schema/wastePaperManagement/looseWastePaper/LoosePaper" +
        "TruckLoad/v002")]
    public partial class LoosePaperTruckLoadType {
        
        private string workshopMachineIDField;
        
        private string departmentCodeField;
        
        private string truckLoadDocumentIDField;
        
        private System.DateTime businessDateField;
        
        private int countField;
        
        private double weightField;
        
        /// <remarks/>
        public string WorkshopMachineID {
            get {
                return this.workshopMachineIDField;
            }
            set {
                this.workshopMachineIDField = value;
            }
        }
        
        /// <remarks/>
        public string DepartmentCode {
            get {
                return this.departmentCodeField;
            }
            set {
                this.departmentCodeField = value;
            }
        }
        
        /// <remarks/>
        public string TruckLoadDocumentID {
            get {
                return this.truckLoadDocumentIDField;
            }
            set {
                this.truckLoadDocumentIDField = value;
            }
        }
        
        /// <remarks/>
        public System.DateTime BusinessDate {
            get {
                return this.businessDateField;
            }
            set {
                this.businessDateField = value;
            }
        }
        
        /// <remarks/>
        public int Count {
            get {
                return this.countField;
            }
            set {
                this.countField = value;
            }
        }
        
        /// <remarks/>
        public double Weight {
            get {
                return this.weightField;
            }
            set {
                this.weightField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1099.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.leemanpaper.com/schema/wastePaperManagement/looseWastePaper/LoosePaper" +
        "TruckLoad/v002")]
    public partial class LoosePaperTruckLoadU9Type : LoosePaperTruckLoadType {
        
        private string extensionField2Field;
        
        private string extensionField3Field;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        public string ExtensionField2 {
            get {
                return this.extensionField2Field;
            }
            set {
                this.extensionField2Field = value;
            }
        }
        
        /// <remarks/>
        public string ExtensionField3 {
            get {
                return this.extensionField3Field;
            }
            set {
                this.extensionField3Field = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1099.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.leemanpaper.com/schema/common/LeeManCommon/v001")]
    public partial class WeightType {
        
        private WeightUnitType unitField;
        
        private decimal valueField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public WeightUnitType Unit {
            get {
                return this.unitField;
            }
            set {
                this.unitField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public decimal Value {
            get {
                return this.valueField;
            }
            set {
                this.valueField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1099.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.leemanpaper.com/schema/common/LeeManCommon/v001")]
    public enum WeightUnitType {
        
        /// <remarks/>
        kg,
        
        /// <remarks/>
        g,
        
        /// <remarks/>
        mg,
        
        /// <remarks/>
        lb,
        
        /// <remarks/>
        mt,
        
        /// <remarks/>
        t,
        
        /// <remarks/>
        oz,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1099.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.leemanpaper.com/schema/technology/framework/Common/v001")]
    public partial class ReplyMessageType {
        
        private string resultCodeField;
        
        private string replyMessageField;
        
        /// <remarks/>
        public string ResultCode {
            get {
                return this.resultCodeField;
            }
            set {
                this.resultCodeField = value;
            }
        }
        
        /// <remarks/>
        public string ReplyMessage {
            get {
                return this.replyMessageField;
            }
            set {
                this.replyMessageField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1099.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://www.leemanpaper.com/wsdl/wastePaperManagement/looseWastePaper/LooseWastePa" +
        "perManagement/v001")]
    public partial class GetRemainingLoosePaperQuantitiesResponseBatch {
        
        private string productCategoryField;
        
        private string binIDField;
        
        private string weightTicketIDField;
        
        private WeightType weightField;
        
        private TypeOfWeighedWastePaperType typeOfWeighedWastePaperField;
        
        /// <remarks/>
        public string ProductCategory {
            get {
                return this.productCategoryField;
            }
            set {
                this.productCategoryField = value;
            }
        }
        
        /// <remarks/>
        public string BinID {
            get {
                return this.binIDField;
            }
            set {
                this.binIDField = value;
            }
        }
        
        /// <remarks/>
        public string WeightTicketID {
            get {
                return this.weightTicketIDField;
            }
            set {
                this.weightTicketIDField = value;
            }
        }
        
        /// <remarks/>
        public WeightType Weight {
            get {
                return this.weightField;
            }
            set {
                this.weightField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace="http://www.leemanpaper.com/schema/common/LeeManCommon/v001")]
        public TypeOfWeighedWastePaperType TypeOfWeighedWastePaper {
            get {
                return this.typeOfWeighedWastePaperField;
            }
            set {
                this.typeOfWeighedWastePaperField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1099.0")]
    public delegate void RequestForCheckOutCompletedEventHandler(object sender, RequestForCheckOutCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1099.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class RequestForCheckOutCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal RequestForCheckOutCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public ReplyMessageType Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((ReplyMessageType)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1099.0")]
    public delegate void GetRemainingLoosePaperQuantitiesCompletedEventHandler(object sender, GetRemainingLoosePaperQuantitiesCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1099.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetRemainingLoosePaperQuantitiesCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetRemainingLoosePaperQuantitiesCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public GetRemainingLoosePaperQuantitiesResponseBatch[] Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((GetRemainingLoosePaperQuantitiesResponseBatch[])(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1099.0")]
    public delegate void RegisterLoosePaperDistributionCompletedEventHandler(object sender, RegisterLoosePaperDistributionCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1099.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class RegisterLoosePaperDistributionCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal RegisterLoosePaperDistributionCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public ReplyMessageType Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((ReplyMessageType)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591