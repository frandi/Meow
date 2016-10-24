using System.Data.Entity.Infrastructure;

namespace Meow.Shared.DataAccess
{
    public class MeowContextFactory : IDbContextFactory<MeowContext>
    {
        public MeowContext Create()
        {
            return new MeowContext("Server=.\\SqlExp2014;Database=MeowDB;User ID=sa;Password=samprod;");
        }
    }
}
