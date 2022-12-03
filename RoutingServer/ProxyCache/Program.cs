﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Description;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ProxyCache
{
	internal class Program
	{
		static void Main(string[] args)
		{
			//Create a URI to serve as the base address
			//Be careful to run Visual Studio as Admistrator or to allow VS to open new port netsh command. 
			// Example : netsh http add urlacl url=http://+:80/MyUri user=DOMAIN\user
			Uri httpUrl = new Uri("http://localhost:8733/Design_Time_Addresses/ProxyCache/JCDecauxService/");

			//Create ServiceHost
			ServiceHost host = new ServiceHost(typeof(JCDecauxService), httpUrl);

			// Multiple end points can be added to the Service using AddServiceEndpoint() method.
			// Host.Open() will run the service, so that it can be used by any client.

			// Example adding :
			// Uri tcpUrl = new Uri("net.tcp://localhost:8090/MyService/SimpleCalculator");
			// ServiceHost host = new ServiceHost(typeof(MyCalculatorService.SimpleCalculator), httpUrl, tcpUrl);

			// Modify binding parameters
			BasicHttpBinding binding = new BasicHttpBinding();
			binding.MaxReceivedMessageSize = 1000000;
            binding.MaxBufferPoolSize = 1000000;
            binding.MaxBufferSize = 1000000;

			//Add a service endpoint
			host.AddServiceEndpoint(typeof(IJCDecauxService), binding, "");

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
        <binding name="proxyCacheBinding"
                 maxBufferSize ="1000000"
                 maxReceivedMessageSize = "1000000">
        </binding>
      </basicHttpBinding>
    </bindings>
    <services>
      <service name="ProxyCache.JCDecauxService">
        <host>
          <baseAddresses>
            <add baseAddress="http://localhost:8733/Design_Time_Addresses/ProxyCache/JCDecauxService/" />
          </baseAddresses>
        </host>
        <!-- Service Endpoints -->
        <!-- Unless fully qualified, address is relative to base address supplied above -->
        <endpoint address="" binding="basicHttpBinding" contract="ProxyCache.IJCDecauxService">
          <!-- 
              Upon deployment, the following identity element should be removed or replaced to reflect the 
              identity under which the deployed service runs.  If removed, WCF will infer an appropriate identity 
              automatically.
          -->
          <identity>
            <dns value="localhost" />
          </identity>
        </endpoint>
        <!-- Metadata Endpoints -->
        <!-- The Metadata Exchange endpoint is used by the service to describe itself to clients. --> 
        <!-- This endpoint does not use a secure binding and should be secured or removed before deployment -->
        <endpoint address="mex" binding="mexHttpBinding" contract="IMetadataExchange" />
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
