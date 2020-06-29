using GDS.Gateway.Constants;
using GDS.Gateway.Models.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace GDS.Gateway.Adapters.DataAccess
{
    public interface IGatewayAdapterRouter
    {
        public IGatewayAdapter GetGatewayAdapter(string gatewayType);
    }

    public class GatewayAdapterRouter : IGatewayAdapterRouter
    {
        private static readonly Dictionary<GatewayTypes, IGatewayAdapter> _adapterKeyTypeMap = new Dictionary<GatewayTypes, IGatewayAdapter>() {
            { GatewayTypes.Blob, new BlobDataAdapter()},
            { GatewayTypes.SQL, new SqlDataAdapter()}
        };

        public IGatewayAdapter GetGatewayAdapter(string gatewayType) {
            var gatewayTypeInst = Enum.Parse<GatewayTypes>(gatewayType);
            var gAdapter = default(IGatewayAdapter);
            if (_adapterKeyTypeMap.TryGetValue(gatewayTypeInst, out gAdapter))
            {
                return gAdapter;
            }
            throw new ArgumentException($"{nameof(gatewayType)} cannot be empty and must be registered", nameof(gatewayType));
        }
    }
}
