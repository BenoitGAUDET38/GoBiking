﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Clientcsharp.GoBikeService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="GoBikeService.IGoBikeService")]
    public interface IGoBikeService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGoBikeService/GetItinary", ReplyAction="http://tempuri.org/IGoBikeService/GetItinaryResponse")]
        string GetItinary(string origin, string destination);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGoBikeService/GetItinary", ReplyAction="http://tempuri.org/IGoBikeService/GetItinaryResponse")]
        System.Threading.Tasks.Task<string> GetItinaryAsync(string origin, string destination);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IGoBikeServiceChannel : Clientcsharp.GoBikeService.IGoBikeService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class GoBikeServiceClient : System.ServiceModel.ClientBase<Clientcsharp.GoBikeService.IGoBikeService>, Clientcsharp.GoBikeService.IGoBikeService {
        
        public GoBikeServiceClient() {
        }
        
        public GoBikeServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public GoBikeServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public GoBikeServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public GoBikeServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string GetItinary(string origin, string destination) {
            return base.Channel.GetItinary(origin, destination);
        }
        
        public System.Threading.Tasks.Task<string> GetItinaryAsync(string origin, string destination) {
            return base.Channel.GetItinaryAsync(origin, destination);
        }
    }
}
