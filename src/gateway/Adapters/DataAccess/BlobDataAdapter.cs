using GDS.Gateway.Models;
using GDS.Gateway.Models.Io;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GDS.Gateway.Adapters.DataAccess
{
    public class BlobDataAdapter : IGatewayAdapter
    {

        public async Task<SchemaResponse> GetSchemaAsync(SchemaRequest req)
        {
            await Task.Delay(1000);

            return default(SchemaResponse);
        }

        public async Task<DataResponse> GetDataAsync(DataRequest req)
        {
            await Task.Delay(1000);
            return default(DataResponse);
        }

    }
}
