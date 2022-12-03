using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Description;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using RoutingServer.Tools.JCDecaux;
using RoutingServer.Tools;
using System.Text.Json;

namespace RoutingServer
{
	internal class Program
	{
		static void Main(string[] args)
		{
			//Create a URI to serve as the base address
			Uri httpUrl = new Uri("http://localhost:8733/Design_Time_Addresses/RoutingServer/GoBikeService/");

			//Create ServiceHost
			ServiceHost host = new ServiceHost(typeof(GoBikeService), httpUrl);

			// Modify binding parameters
			BasicHttpBinding binding = new BasicHttpBinding();
			binding.MaxReceivedMessageSize = 1000000;
            binding.MaxBufferPoolSize = 1000000;
            binding.MaxBufferSize = 1000000;

			//Add a service endpoint
			host.AddServiceEndpoint(typeof(IGoBikeService), binding, "");

			//Enable metadata exchange
			ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
			smb.HttpGetEnabled = true;
			host.Description.Behaviors.Add(smb);

			//Start the Service
			host.Open();

			Console.WriteLine("Service is host at " + DateTime.Now.ToString());
			Console.WriteLine("Host is running... Press <Enter> key to stop");
			Console.ReadLine();
			/*
			<?xml version="1.0" encoding="utf-8"?>
<configuration>

  <appSettings>
    <add key="aspnet:UseTaskFriendlySynchronizationContext" value="true" />
  </appSettings>
  <system.web>
    <compilation debug="true" />
  </system.web>
  <!-- When deploying the service library project, the content of the config file must be added to the host's 
  app.config file. System.Configuration does not support config files for libraries. -->
  <system.serviceModel>
    <bindings>
      <basicHttpBinding>
        <binding name="BasicHttpBinding_IJCDecauxService" maxReceivedMessageSize = "1000000" maxBufferSize="1000000"/>
      </basicHttpBinding>
    </bindings>
    <client>
      <endpoint address="http://localhost:8733/Design_Time_Addresses/ProxyCache/JCDecauxService/"
        binding="basicHttpBinding" bindingConfiguration="BasicHttpBinding_IJCDecauxService"
        contract="JCDecauxProxyCacheService.IJCDecauxService" name="BasicHttpBinding_IJCDecauxService" />
    </client>
    <services>
      <service name="RoutingServer.GoBikeService">
        <endpoint address="" binding="basicHttpBinding" contract="RoutingServer.IGoBikeService">
          <identity>
            <dns value="localhost" />
          </identity>
        </endpoint>
        <endpoint address="mex" binding="mexHttpBinding" contract="IMetadataExchange" />
        <host>
          <baseAddresses>
            <add baseAddress="http://localhost:8733/Design_Time_Addresses/RoutingServer/GoBikeService/" />
          </baseAddresses>
        </host>
      </service>
    </services>
    <behaviors>
      <serviceBehaviors>
        <behavior>
          <!-- To avoid disclosing metadata information, 
          set the values below to false before deployment -->
          <serviceMetadata httpGetEnabled="True" httpsGetEnabled="True" />
          <!-- To receive exception details in faults for debugging purposes, 
          set the value below to true.  Set to false before deployment 
          to avoid disclosing exception information -->
          <serviceDebug includeExceptionDetailInFaults="False" />
        </behavior>
      </serviceBehaviors>
    </behaviors>
  </system.serviceModel>

  <runtime>

    <assemblyBinding xmlns="urn:schemas-microsoft-com:asm.v1">

      <dependentAssembly>

        <assemblyIdentity name="System.Runtime.CompilerServices.Unsafe" publicKeyToken="b03f5f7f11d50a3a" culture="neutral" />

        <bindingRedirect oldVersion="0.0.0.0-6.0.0.0" newVersion="6.0.0.0" />

      </dependentAssembly>

    </assemblyBinding>

  </runtime>
</configuration>

			
			 */
		}
	}
}
