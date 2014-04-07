using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace Burgerama.Services.Users.Api.Results
{
    public class ChallengeResult : IHttpActionResult
    {
        public string LoginProvider { get; set; }
        public HttpRequestMessage Request { get; set; }

        public ChallengeResult(string loginProvider, ApiController controller)
        {
            LoginProvider = loginProvider;
            Request = controller.Request;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            Request.GetOwinContext().Authentication.Challenge(LoginProvider);

            return Task.FromResult(new HttpResponseMessage(HttpStatusCode.Unauthorized)
            {
                RequestMessage = Request
            });
        }
    }
}
