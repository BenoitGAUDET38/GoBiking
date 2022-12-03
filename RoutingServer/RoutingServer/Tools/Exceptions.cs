using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoutingServer.Tools
{
	[Serializable]
	class ContractNotCoveredException : Exception { }

	[Serializable]
	class CityNotFoundException : Exception { }

	[Serializable]
	class IncorrectAdressException : Exception { }

	[Serializable]
	class MultipleCitiesItinaryException : Exception { }
}
