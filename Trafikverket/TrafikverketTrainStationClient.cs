using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Trafikverket.Response;
using Trafikverket.Request;
using System.Xml;
using System.Xml.Serialization;
using System.Collections.ObjectModel;
using System.IO;

namespace Trafikverket
{
    public class TrafikverketTrainStationClient : TrafikverketBase
    {

        public TrafikverketTrainStationClient(string apiKey) : base(apiKey)
        {
        }

        public async Task<TrainStation> GetAsync(string name, CancellationToken cancellationToken)
        {
            var request = new Request.Request()
            {
                Login = new Login()
                {
                    Authenticationkey = apiKey
                },
                Query = new Query()
                {
                    ObjectType = "TrainStation",
                    Include = new Collection<string>(),
                    Filter = new BaseGroup()
                    {
                        Equals = new Collection<BaseCondition>()
                        {
                            new BaseCondition("AdvertisedLocationName", name)
                        }
                    }
                }
            };

            var response = await MakeRequestAsync<TrainStationResponse>(request, cancellationToken);

            if (response.Result.Count > 0)
                return response.Result[0];
            else
                return null;
        }

        public async Task<Collection<TrainStation>> Search(string name, CancellationToken cancellationToken)
        {
            var request = new Request.Request()
            {
                Login = new Login()
                {
                    Authenticationkey = apiKey
                },
                Query = new Query()
                {
                    ObjectType = "TrainStation",
                    Include = new Collection<string>(),
                    Filter = new BaseGroup()
                    {
                        Like = new Collection<BaseCondition>()
                        {
                            new BaseCondition("AdvertisedLocationName", name)
                        }
                    }
                }
            };

            return (await MakeRequestAsync<TrainStationResponse>(request, cancellationToken)).Result;
        }
    }
}
