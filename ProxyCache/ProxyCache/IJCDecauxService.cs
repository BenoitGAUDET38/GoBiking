using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ProxyCache
{
	[ServiceContract]
	public interface IJCDecauxService
	{
		[OperationContract]
		string GetContracts();

		[OperationContract]
		string GetStations(string contract);
	}
}
