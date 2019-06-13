using System.Web.Http;

namespace CMS.Presentation.Controllers.Api
{
    //[EnableCors(origins: "https://CMS.vn,https://localhost:44345", headers: "*", methods: "*")]
    //[AutoInvalidateCacheOutput]
    //[CacheOutput(ServerTimeSpan = 84000, ExcludeQueryStringFromCacheKey = true)]
    public class ApiControllerBase : ApiController
    {
    }
}
