using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Indico;
using Indico.Entity;
using Indico.Request;
using Newtonsoft.Json.Linq;

namespace IndicoTest
{
    class Program
    {
        static void Main(string[] args)
        {


            IndicoConfig config = new IndicoConfig(host: "app.indico.io");
            IndicoClient client = new IndicoClient(config);

            getDatasets(client);
            getModelGroup(client, 4415);
            
        }

        static void getModelGroup(IndicoClient client, int mgId)
        {
            ModelGroup mg = client.ModelGroupQuery(mgId).Exec();
            Console.WriteLine(mg.ToString());
        }

        static void getDatasets(IndicoClient client)
        {
            string query = @"
                query GetDatasets {
                    datasets {
                        id
                        name
                        status
                        rowCount
                        numModelGroups
                        modelGroups {
                            id
                        }
                    }
                  }
                ";

            GraphQLRequest request = client.GraphQLRequest(query, "GetDatasets");
            JObject response = request.Call();
            Console.WriteLine(response);
        }
    }
}
