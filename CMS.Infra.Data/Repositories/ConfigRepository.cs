using CMS.Domain.Entities;
using CMS.Infra.Data.Repositories;

namespace CMS.Domain.Interfaces.Repositories
{
    public class ConfigRepository : RepositoryBase<Config>, IConfigRepository
    {
    }
}