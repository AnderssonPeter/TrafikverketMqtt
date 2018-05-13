using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Trafikverket.Request;
using Trafikverket.Response;

namespace Trafikverket
{
    public class TrafikverketTrainAnnouncemenClient : TrafikverketBase
    {
        public TrafikverketTrainAnnouncemenClient(string apiKey) : base(apiKey)
        {
        }

        public async Task<TrainAnnouncement> GetTrainStopAsync(string departureTrainStationSignature, string destinationTrainStationSignature, DateTime time, CancellationToken cancellationToken)
        {
            var request = new Request.Request()
            {
                Login = new Login()
                {
                    Authenticationkey = apiKey
                },
                Query = new Query()
                {
                    ObjectType = "TrainAnnouncement",
                    Include = new Collection<string>(),
                    Filter = new BaseGroup()
                    {
                        And = new Collection<BaseGroup>()
                        {
                            new BaseGroup()
                            {
                                Equals = new Collection<BaseCondition>()
                                {
                                    new BaseCondition("ActivityType", "Avgang"),
                                    new BaseCondition("LocationSignature", departureTrainStationSignature),
                                    new BaseCondition("AdvertisedTimeAtLocation", time.ToString("yyyy-MM-dd HH:mm:ss"))
                                },
                                Or = new Collection<BaseGroup>()
                                {
                                    new BaseGroup()
                                    {
                                        Equals = new Collection<BaseCondition>()
                                        {
                                            new BaseCondition("ViaToLocation.LocationName", destinationTrainStationSignature),
                                            new BaseCondition("ToLocation.LocationName", destinationTrainStationSignature)
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            };

            var response = await MakeRequestAsync<TrainAnnouncementResponse>(request, cancellationToken);

            if (response.Result.Count > 0)
                return response.Result[0];
            else
                return null;
        }

        public async Task<TrainAnnouncement> GetTrainNextStopAsync(string departureTrainStationSignature, string destinationTrainStationSignature, DateTime time, CancellationToken cancellationToken)
        {
            var request = new Request.Request()
            {
                Login = new Login()
                {
                    Authenticationkey = apiKey
                },
                Query = new Query()
                {
                    ObjectType = "TrainAnnouncement",
                    Include = new Collection<string>(),
                    Filter = new BaseGroup()
                    {
                        And = new Collection<BaseGroup>()
                        {
                            new BaseGroup()
                            {
                                Equals = new Collection<BaseCondition>()
                                {
                                    new BaseCondition("ActivityType", "Avgang"),
                                    new BaseCondition("LocationSignature", departureTrainStationSignature)
                                },
                                GreaterThanEqual = new Collection<BaseCondition>()
                                {
                                    new BaseCondition("AdvertisedTimeAtLocation", time.ToString("yyyy-MM-dd HH:mm:ss"))
                                },
                                Or = new Collection<BaseGroup>()
                                {
                                    new BaseGroup()
                                    {
                                        Equals = new Collection<BaseCondition>()
                                        {
                                            new BaseCondition("ViaToLocation.LocationName", destinationTrainStationSignature),
                                            new BaseCondition("ToLocation.LocationName", destinationTrainStationSignature)
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            };

            var response = await MakeRequestAsync<TrainAnnouncementResponse>(request, cancellationToken);

            if (response.Result.Count > 0)
                return response.Result[0];
            else
                return null;
        }
    }
}
